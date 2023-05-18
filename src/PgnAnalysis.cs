using System.Text.RegularExpressions;
using CsvHelper;
using System.Globalization;

namespace PgnAnalyzer;

class PgnAnalysis
{
    public List<OpeningData> analyze(string pathToPgn)
    {
        PgnEnumerator pgns = new PgnEnumerator(pathToPgn);

        List<OpeningData> openings = new List<OpeningData>();

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
                currOpening = new OpeningData();
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
            string afterBookMoveText = symmetricMoveDifference(EcoMoveText, moveText);
            string firstAfterBookMoveSan = getFirstMoveSan(afterBookMoveText);
            int firstAfterBookMoveNum = getFirstMoveNum(afterBookMoveText);

            OutOfBookData currOutOfBookData;
            int outOfBookDataIndex = currOutOfBookDataList.FindIndex(x => x.san == firstAfterBookMoveSan && x.moveNum == firstAfterBookMoveNum);

            if(openingIndex >= 0)
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
                //IMPLEMENT
                int blunderMoveNum = 0;

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
        }

        return openings;
    }

    //ALL OF THESE METHODS HAVE BEEN MADE PUBLIC FOR TESTING. THIS IS BAD AND I SHOULDNT BE DOING IT, HOWEVER
    //THE ALTERNATIVE IS GETTING GOOD AND I'M FAR TOO LAZY FOR THAT.

    public string getFirstBlunder(string moveText)
    {
        if(hasAnalysis(moveText))
        {
            List<string> splitMoveText = moveText.Split(" ").ToList();

            string? firstBlunder = splitMoveText.Find(move => Regex.Match(move, @"^\s]*\?\?").Success);

            if(firstBlunder != null)
            {
                return firstBlunder.TrimEnd('?');
            }
        }

        return "";
    }

    public bool hasAnalysis(string moveText)
    {
        string[] annoations = new string[]{"??", "?", "!", "?!", "!?", "!!"};

        if(annoations.Any(annotation => moveText.Contains(annotation)))
        {
            return true;
        }

        return false;
    }

    public string symmetricMoveDifference(string moveText, string ecoMoveText)
    {

        for(int i = 0; i<moveText.Length;i++)
        {
            if(moveText.ElementAt(i) != ecoMoveText.ElementAt(i))
            {
                return moveText.Substring(i);
            }
        }

        return "";

    }

    public string getFirstMoveSan(string moveText)
    {
        string[] splitMoveText = moveText.Split(" ");
        
        foreach(string item in splitMoveText)
        {
            if(!Regex.Match(item, @"\d\.").Success)
            {
                return item;
            }
        }

        throw new InvalidOperationException();
    }

    public int getFirstMoveNum(string moveText)
    {
        string[] splitMoveText = moveText.Split(" ");

        //does not start with a number, find the next number and return that-1
        for(int i = 1;i<splitMoveText.Length;i++)
        {
            if(Regex.Match(splitMoveText[0], @"\d\.").Success)
            {
                if(i == 0)
                {
                    return Int32.Parse(splitMoveText[i]);
                }
                else
                {
                    return Int32.Parse(splitMoveText[i])-1;
                }
            }
        }

        return -1;
    }

    public string stripCommentsFromGame(string moveText)
    {
        string output = Regex.Replace(moveText, @"\{[^{}]*\}", "");

        Console.WriteLine("removed comments: " + output);

        output = Regex.Replace(output, @"\d\.\.\.", "");

        return output;
    }

    public string getEcoFromString(string moveText)
    {
        StreamReader sr = new StreamReader("eco.tsv");

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        string largestSubstring = "";

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string ecoMoveText = splitLine[2];

            if(moveText.Contains(ecoMoveText))
            {
                if(ecoMoveText.Length > largestSubstring.Length)
                {
                    largestSubstring = ecoMoveText;
                }
            }
        }

        return largestSubstring;
    }

    public string getStringFromEco(string eco)
    {
        StreamReader sr = new StreamReader("eco.tsv");

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string ecoFromFile = splitLine[0];

            if(ecoFromFile == eco)
            {
                return splitLine[2];
            }
        }

        return "";
    }

    public string getResultFromString(string moveText)
    {
       string[] splitMoveText = moveText.Split(" ");

       string lastItem = splitMoveText[splitMoveText.Length-1];

       if(lastItem == "1-0" || lastItem == "0-1" || lastItem == "1/2-1/2")
       {
            return lastItem;
       }

       return "";
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

        if(pgn.ContainsKey("WhiteElo"))
        {
            totalElo += int.Parse(pgn["WhiteElo"]);
            numOfElos++;
        }
        if(pgn.ContainsKey("BlackElo"))
        {
            totalElo += int.Parse(pgn["BlackElo"]);
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