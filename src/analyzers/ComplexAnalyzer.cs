using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

//TODO: ADD ANNOTATIONS
//TODO: ADD README
public class ComplexAnalyzer : IAnalyzer
{
    private List<OpeningData> openings = new List<OpeningData>();

    private Eco eco = new Eco("eco.tsv");

    public void addGame(Pgn pgn)
    {
        //opening info
        Game? game = pgn.game;

        if(game == null)
        {
            return;
        }

        string eco = pgn.ContainsKey("eco") ? (string)pgn["eco"] : this.eco.getEcoFromMoves(game.moves);

        //get appropiate openingData class

        OpeningData? opening = openings.Find(opening => opening.eco == eco);

        if(opening == null)
        {
            opening = new OpeningData();
            opening.eco = eco;
            openings.Add(opening);
        }

        opening.numGames++;

        //rating data

        int basket = getEloBasket(pgn); 

        List<RatingData> ratingDatas = opening.ratingDataList;

        RatingData? ratingData = ratingDatas.Find(ratingData => ratingData.eloMin == basket);

        if(ratingData == null)
        {
            ratingData = new RatingData();
            ratingData.eloMin = basket;
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

        // //out of book data

        List<OutOfBookData> currOutOfBookDataList = ratingData.outOfBookDataList;

        //if EcoMoveText = null, something has gone wrong with getEcoFromMoves() or the PGN itself
        List<Move>? ecoMoves = this.eco.getMovesFromEco(eco);
  
        if(ecoMoves == null)
        {
            ecoMoves = new List<Move>();
        }

        //get first ply out of book

        int gameLength = game.moves.Count;
        int ecoLength = ecoMoves.Count;

        if(gameLength <= ecoLength)
        {
            return;
        }
        
        Ply newPly;

        List<Move> simplifiedGame = Eco.SimplifyMoves(game.moves);

        Move newMove = simplifiedGame[ecoLength-1];

        Move lastMoveOfEco = ecoMoves[ecoLength-1];

        if(newMove.Equals(lastMoveOfEco))
        {
            newPly = simplifiedGame[ecoLength].whitePly!;
        }
        else
        {
            newPly = newMove.blackPly!;
        }

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
            try{
                totalElo += int.Parse((string)pgn["WhiteElo"]);
                numOfElos++;
            }
            catch
            {
                //Console.WriteLine("Invalid Elo: " + pgn["WhiteElo"]);
            }
        }
        if(pgn.ContainsKey("BlackElo") && int.TryParse((string)pgn["BlackElo"], out int m))
        {
            try{
                totalElo += int.Parse((string)pgn["BlackElo"]);
                numOfElos++;
            }
            catch
            {
                //Console.WriteLine("Invalid Elo: " + pgn["BlackElo"]);
            }
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