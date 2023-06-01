/*using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using PgnAnalyzer.Utils; 

namespace PgnAnalyzer;

public class PgnAnalysis
{
    public List<OpeningData> analyze(string pathToPgn)
    {
        PgnEnumerator pgns = new PgnEnumerator(pathToPgn);

        Console.WriteLine($"Beginning analysis of {pathToPgn}");

        List<OpeningData> openings = new List<OpeningData>();

        int totalNumGames = 0;

        while(pgns.MoveNext())
        {
            //opening info

            Pgn currPgn = pgns.Current;

            string moveText = currPgn["game"];

            string eco = currPgn.ContainsKey ("eco") ? currPgn["eco"] : getEcoFromString(moveText);
            
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

            int basket = getEloBasket(currPgn); 

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

            string result = currPgn.ContainsKey("result") ? currPgn["result"] : getResultFromString(moveText);

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
            }

            //out of book data

            List<OutOfBookData> currOutOfBookDataList = currRatingData.outOfBookDataList;
            
            string EcoMoveText = getStringFromEco(eco);
            string afterBookMoveText = symmetricMoveDifference(moveText, EcoMoveText);
            string firstAfterBookMoveSan = getFirstMoveSan(afterBookMoveText);
            int firstAfterBookMoveNum = getFirstMoveNum(afterBookMoveText);

            OutOfBookData currOutOfBookData;
            int outOfBookDataIndex = currOutOfBookDataList.FindIndex(x => (x.san == firstAfterBookMoveSan && x.moveNum == firstAfterBookMoveNum));

            if(outOfBookDataIndex >= 0)
            {   
                currOutOfBookData = currOutOfBookDataList[outOfBookDataIndex];
            }
            else{
                currOutOfBookData = new OutOfBookData(firstAfterBookMoveSan, firstAfterBookMoveNum);
                currOutOfBookDataList.Add(currOutOfBookData);
            }

            currOutOfBookData.count++;  

            //Blunder spots

            List<BlunderSpotData> currBlunderSpotDataList = currRatingData.blunderSpotDataList;

            if(hasAnalysis(moveText))
            {
                //TODO: FIGURE OUT WHETHER IT WAS A BLACK OR WHITE BLUNDER
                string blunder = getFirstBlunder(moveText);

                int blunderMoveNum = getFirstBlunderNum(moveText);

                int blunderSpotIndex = currBlunderSpotDataList.FindIndex(x => x.moveNum == blunderMoveNum);
                BlunderSpotData currBlunderSpotData;

                if(blunderSpotIndex >= 0)
                {
                    currBlunderSpotData = currBlunderSpotDataList[blunderSpotIndex];
                }
                else{
                    currBlunderSpotData = new BlunderSpotData(blunderMoveNum);
                    currBlunderSpotDataList.Add(currBlunderSpotData);
                } 

                currBlunderSpotData.count++;
            }

            totalNumGames++;

            if(totalNumGames % 10000 == 0)
            {
                Console.WriteLine($"Total games analyzed: {totalNumGames}");
            }

        }

        pgns.Dispose();

        return openings;
    }

    //ALL OF THESE METHODS HAVE BEEN MADE PUBLIC FOR TESTING. THIS IS BAD AND I SHOULDNT BE DOING IT, HOWEVER
    //THE ALTERNATIVE IS GETTING GOOD AND I'M FAR TOO LAZY FOR THAT.



    public string symmetricMoveDifference(string moveText, string ecoMoveText)
    {

        string modifiedMoveText = moveText;

        if(hasAnalysis(moveText))
        {
            modifiedMoveText = stripCommentsFromGame(moveText);
        }

        List<string> splitMoveText = modifiedMoveText.Split(' ').ToList();

        int splitEcoTextLength = ecoMoveText.Split(' ').Length;
    
        if(splitEcoTextLength < splitMoveText.Count())
        {
            splitMoveText.RemoveRange(0, splitEcoTextLength);
        }
        else
        {
            return "";
        }
        
        string result = string.Join(' ', splitMoveText);

        return result;

    }

    public int getEloBasket(Pgn pgn)
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

    public int getAverageRating(Pgn pgn)
    {
        int numOfElos = 0;
        int totalElo = 0;

        if(pgn.ContainsKey("WhiteElo") && int.TryParse(pgn["WhiteElo"], out int n))
        {
            try{
                totalElo += int.Parse(pgn["WhiteElo"]);
                numOfElos++;
            }
            catch
            {
                Console.WriteLine("Invalid Elo: " + pgn["WhiteElo"]);
            }
        }
        if(pgn.ContainsKey("BlackElo") && int.TryParse(pgn["BlackElo"], out int m))
        {
            try{
                totalElo += int.Parse(pgn["BlackElo"]);
                numOfElos++;
            }
            catch
            {
                Console.WriteLine("Invalid Elo: " + pgn["BlackElo"]);
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

}*/