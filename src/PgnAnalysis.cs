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

    public int getFirstBlunderNum(string moveText)
    {
        if(hasAnalysis(moveText))
        {
            List<string> splitMoveText = moveText.Split(" ").ToList();

            // foreach(string eco in splitMoveText)
            // {
            //     Console.WriteLine(eco);
            // }

            int firstBlunderIndex = splitMoveText.FindIndex(move => Regex.Match(move, @"(.)*\?\?").Success);

            if(firstBlunderIndex != -1)
            {
                string blunderNum = splitMoveText[firstBlunderIndex-1];

                blunderNum = blunderNum.Trim('.');

                int blunderNumInt = Int32.Parse(blunderNum);

                return blunderNumInt;
            }
        }

        return -1;
    }

    public string getFirstBlunder(string moveText)
    {
        if(hasAnalysis(moveText))
        {
            List<string> splitMoveText = moveText.Split(" ").ToList();

            string? firstBlunder = splitMoveText.Find(move => Regex.Match(move, @"(.)*\?\?").Success);

            if(firstBlunder != null)
            {
                return firstBlunder.TrimEnd('?');
            }
        }

        return "";
    }

    public bool hasAnalysis(string moveText)
    {
        string[] analyses = new string[]{"??", "?", "!", "?!", "!?", "!!"};

        if(analyses.Any(analysis => moveText.Contains(analysis)))
        {
            return true;
        }

        return false;
    }

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

    public string getFirstMoveSan(string moveText)
    {
        string modifiedMoveText = moveText;

        if(hasAnalysis(modifiedMoveText))
        {
            modifiedMoveText = stripCommentsFromGame(moveText);
        }

        string[] splitMoveText = modifiedMoveText.Split(" ");
        
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

        string stripped = stripCommentsFromGame(moveText);

        string[] splitMoveText = stripped.Split(" ");

        //does not start with a number, find the next number and return that-1
        for(int i = 0;i<splitMoveText.Length;i++)
        {
            if(Regex.Match(splitMoveText[i], @"\d\.").Success)
            {
                string trimmed = splitMoveText[i].Trim('.');
                
                if(i == 0)
                {            
                    return Int32.Parse(trimmed);
                }
                else
                {
                    return Int32.Parse(trimmed)-1;
                }
            }
        }

        return -1;
    }

    public string stripCommentsFromGame(string moveText)
    {
        string output = Regex.Replace(moveText, @"\{[^{}]*\}", "");

        //Console.WriteLine("removed comments: " + output);

        output = Regex.Replace(output, @"\d\.\.\.", String.Empty);
        
        output = output.Replace("   ", " ");
        output = output.Replace("  ", " ");
        output = output.Trim(' ');

        return output;
    }

    public string getEcoFromString(string moveText)
    {

        string moveTextNoPeriod = moveText.Replace(".", String.Empty);
        StreamReader sr = new StreamReader("eco.tsv");

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        string bestFitEco = "A00";

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string eco = splitLine[0];
            string ecoMoveText = splitLine[2];

            if(moveTextNoPeriod.Contains(ecoMoveText))
            {
                if(ecoMoveText.Length > bestFitEco.Length)
                {
                    bestFitEco = eco;
                }
            }

            line = sr.ReadLine();
        }

        sr.Close();

        return bestFitEco;
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
                sr.Close();
                return splitLine[2];
            }

            line = sr.ReadLine();
        }

        throw new InvalidOperationException();
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