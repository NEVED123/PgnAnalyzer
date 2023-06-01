using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public class MyAnalyzer : IAnalyzer
{
    private List<OpeningData> openings = new List<OpeningData>();

    private EcoReader ecoReader = new EcoReader("../eco.tsv");

    int totalNumGames = 0;

    public void addGame(Pgn pgn)
    {
        //opening info
        Game? game = pgn.game;

        if(game == null)
        {
            return;
        }

        string eco = pgn.ContainsKey("eco") ? (string)pgn["eco"] : ecoReader.getEcoFromMoves(game.moves);

        //Console.WriteLine(eco);
    
        OpeningData currOpening;
        int openingIndex = openings.FindIndex(x => x.eco == eco);

        if(openingIndex >= 0)
        {  
            currOpening = openings[openingIndex];
        }
        else{
            currOpening = new OpeningData(eco);
            openings.Add(currOpening);
        }

        currOpening.numGames++;

        //rating data

        int basket = getEloBasket(pgn); 

        List<RatingData> currRatingDataList = currOpening.ratingDataList;

        int ratingIndex = currRatingDataList.FindIndex(x => x.eloMin == basket);
        RatingData currRatingData;

        if(ratingIndex >= 0)
        {
            currRatingData = currRatingDataList[ratingIndex];
        }
        else{
            currRatingData = new RatingData(basket);
            currRatingDataList.Add(currRatingData);
        } 

        string? result = game.result;

        switch(result)
        {
            case "1-0":
                currRatingData.whiteWinNum++;
                break;
            case "0-1":
                currRatingData.blackWinNum++;
                break;
            case "1/2-1/2":
                currRatingData.drawNum++;
                break;
            default:
                currRatingData.noResultDataNum++;
                break;
        }

        //out of book data

        List<OutOfBookData> currOutOfBookDataList = currRatingData.outOfBookDataList;
        
        //if EcoMoveText = null, something has gone wrone with getEcoFromMoves() or the PGN itself
        List<Move>? EcoMoveText = ecoReader.getMovesFromEco(eco);

        if(EcoMoveText == null)
        {
            EcoMoveText = new List<Move>();
        }

        //get first move

        List<Move> afterBookMoves = moveDifference(game.moves, EcoMoveText);
        
        Ply firstOutOfBookPly;
        Move afterBookMove = afterBookMoves[0];
        Ply? whiteFirstPly = afterBookMove.whitePly;
        Ply? blackFirstPly = afterBookMove.blackPly;
        
        if(whiteFirstPly != null)
        {
            firstOutOfBookPly = whiteFirstPly;
        }
        else
        {
            firstOutOfBookPly = blackFirstPly!;
        }

        //get first move num

        int? outOfBookMoveNum;

        if(afterBookMove.moveNum != null)
        {
            outOfBookMoveNum = afterBookMove.moveNum;
        }
        else
        {
            outOfBookMoveNum = afterBookMoves[1].moveNum;
        }

        OutOfBookData currOutOfBookData;
        int outOfBookDataIndex = currOutOfBookDataList.FindIndex(
            x => (x.san == firstOutOfBookPly.san && x.moveNum == outOfBookMoveNum)
        );

        if(outOfBookDataIndex >= 0)
        {   
            currOutOfBookData = currOutOfBookDataList[outOfBookDataIndex];
        }
        else{
            currOutOfBookData = new OutOfBookData(firstOutOfBookPly.san, outOfBookMoveNum);
            currOutOfBookDataList.Add(currOutOfBookData);
        }

        currOutOfBookData.count++;  

        // //Blunder spots

        // List<BlunderSpotData> currBlunderSpotDataList = currRatingData.blunderSpotDataList;

        // if(hasAnalysis(moveText))
        // {
        //     //TODO: FIGURE OUT WHETHER IT WAS A BLACK OR WHITE BLUNDER
        //     string blunder = getFirstBlunder(moveText);

        //     int blunderMoveNum = getFirstBlunderNum(moveText);

        //     int blunderSpotIndex = currBlunderSpotDataList.FindIndex(x => x.moveNum == blunderMoveNum);
        //     BlunderSpotData currBlunderSpotData;

        //     if(blunderSpotIndex >= 0)
        //     {
        //         currBlunderSpotData = currBlunderSpotDataList[blunderSpotIndex];
        //     }
        //     else{
        //         currBlunderSpotData = new BlunderSpotData(blunderMoveNum);
        //         currBlunderSpotDataList.Add(currBlunderSpotData);
        //     } 

        //     currBlunderSpotData.count++;
        // }

        totalNumGames++;

        if(totalNumGames % 10000 == 0)
        {
            //.WriteLine($"Total games analyzed: {totalNumGames}");
        }
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

    private List<Move> moveDifference(List<Move> bigger, List<Move> smaller)
    {
        List<Move> difference = new List<Move>();

        if(bigger.Count <= smaller.Count)
        {
            return difference;
        }

        //find the move object where they differ
        
        List<Move> biggerSimple = EcoReader.simplifyMoves(bigger);
        List<Move> smallerSimple = EcoReader.simplifyMoves(smaller);

        //janky as hell
        string differenceStr = biggerSimple.ToString()!.Replace(smallerSimple.ToString()!, String.Empty);

        //Console.WriteLine("Difference: " + differenceStr);

        return Game.Parse(differenceStr).moves;
    }

}