using PgnAnalyzer.Utils;
using PgnAnalyzer.IO;

namespace PgnAnalyzer.Analyzer;

/*
    Example of a more involved analyzer class. If you have not yet, checkout SimpleAnalyzer.cs first.
    The data will look like this:

    Opening:
        ECO:
            Code: @##
            Name: "Opening name"
            Moves: Move list
        Rating range: 0-1000
            Number of games: #
            White wins: #
            Black wins: #
            Draws: #
            Unknown (No result information given in pgn): #
            First moves out of book:
                San: SAN
                    Count: #
                San: SAN
                    Count: #
                ...
        Rating range: 1000-1500
            Same as above
        Rating range: 1500-2000
            Same as above
        Rating range: 2000+
            Same as above

    [For each unique opening]
*/

public class ComplexAnalyzer : IAnalyzer
{
    //Data object to be passed to serializer
    private List<OpeningData> openings = new List<OpeningData>();

    //EcoReader aids in parsing and searching the eco file
    private EcoReader ecoReader = new EcoReader("eco.tsv");

    public void addGame(Pgn pgn)
    {
        //opening info
        Game? game = pgn.game;

        if(game == null)
        {
            return;
        }

        //Util for Eco. 
        Eco? eco;

        if(pgn.ContainsKey("eco"))
        {
            eco = ecoReader.GetEcoFromCode((string)pgn["eco"]);
        }
        else
        {
            eco = ecoReader.getEcoFromMoves(game.moves);
        }

        //get appropiate openingData class if it exists
        OpeningData? opening = openings.Find(opening => opening.eco.Equals(eco));

        if(opening == null)
        {
            //We have not seen this opening yet, so add a new one to the list
            opening = new OpeningData();
            opening.eco = eco;
            openings.Add(opening);
        }

        opening.numGames++;

        //Now that we have the OpeningData object we want, we can start collecting rating data

        int basket = getEloBasket(pgn);

        List<RatingData> ratingDatas = opening.ratingDataList;

        RatingData? ratingData = ratingDatas.Find(ratingData => ratingData.eloMin == basket);

        if(ratingData == null)
        {
            ratingData = new RatingData();
            ratingData.eloMin = basket; //eloMin represents the bottom of the basket
            ratingDatas.Add(ratingData);
        }

        string? result = game.result;

        switch(result)
        {
            case "1-0":
                ratingData.whiteWinNum++;
                break;
            case "0-1":
                ratingData.blackWinNum++;
                break;
            case "1/2-1/2":
                ratingData.drawNum++;
                break;
            default:
                ratingData.noResultDataNum++;
                break;
        }

        //Out Of Book Data

        List<OutOfBookData> currOutOfBookDataList = ratingData.outOfBookDataList;

        //get First Ply out of book

        int gameLength = game.moves.Count;
        int ecoLength = eco.moves!.Count;

        if(gameLength <= ecoLength || eco.moves.Count == 0)
        {
            return;
        }

        Move lastMoveOfEco = eco.moves[ecoLength-1];

        //Util for Ply (Ply = One Turn, as opposed to a Move = White Turn and Black Turn)
        Ply newPly;

        if(lastMoveOfEco.blackPly == null)
        {
            newPly = game.moves[ecoLength-1].blackPly!; //Black made the first out of book move
        }
        else
        {
            newPly = game.moves[ecoLength].whitePly!; //White made the first out of book move
        }

        //SANitize ply
        newPly.analysis = null;
        newPly.annotation = null;

        List<OutOfBookData> outOfBookDatas = ratingData.outOfBookDataList;
        
        OutOfBookData? outOfBookData = outOfBookDatas.Find(outOfbookMove => outOfbookMove.san == newPly.san);
        
        if(outOfBookData == null)
        {
            outOfBookData = new OutOfBookData();
            outOfBookData.san = newPly.san;
            outOfBookDatas.Add(outOfBookData);
        }

        outOfBookData.count++;  
    }
  
    public object getResults()
    {
        return openings;
    }

    //Private helper methods
    private int getEloBasket(Pgn pgn)
    {
        int avgElo = getAverageRating(pgn);

        //basket minimums in descending order
        int[] baskets = new int[]{2000,1500,1000,0};

        foreach(int basketMin in baskets)
        {
            if(avgElo > basketMin)
            {
                return basketMin;
            }
        }

        return -1;
    }

    private int getAverageRating(Pgn pgn)
    {
        int numOfElos = 0;
        int totalElo = 0;

        if(pgn.ContainsKey("WhiteElo") && int.TryParse((string)pgn["WhiteElo"], out int n))
        {
            totalElo += int.Parse((string)pgn["WhiteElo"]);
            numOfElos++;
        }
        if(pgn.ContainsKey("BlackElo") && int.TryParse((string)pgn["BlackElo"], out int m))
        {
            totalElo += int.Parse((string)pgn["BlackElo"]);
            numOfElos++;
        }

        if(numOfElos != 0)
        {
            return totalElo/numOfElos;
        }
        else{
            return -1;
        }
    }

}