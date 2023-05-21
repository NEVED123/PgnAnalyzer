namespace PgnAnalyzer;

public class OpeningData
{
    public OpeningData(string eco){
        numGames = 0;
        ratingDataList = new List<RatingData>();
        this.eco = eco;
    }
    public string eco {get; init;}
    public int numGames {get; set;}
    public List<RatingData> ratingDataList {get; set;}

    public override string ToString()
    {
        string output = "";

        output += $"ECO: {eco}\n";
        output += $" Number of Games: {numGames}\n";
        output += $"Rating Pools\n";

        foreach(RatingData ratingData in ratingDataList)
        {
            output += $"{ratingData}";
        }

        output += "\n";

        return output;
    }  
}

/*
    private void printOpening(OpeningData data)
    {
        Console.WriteLine("ECO: " + data.eco);
        Console.WriteLine("Number of games with this opening: " + data.numGames);
        Console.WriteLine("Rating Pools:");
        foreach(RatingData ratingData in data.ratingDataList)
        {
            Console.WriteLine(" Rating: " + ratingData.eloMin);
            Console.WriteLine(" White Win Number: " + ratingData.whiteWinNum);
            Console.WriteLine(" Black Win Number: " + ratingData.blackWinNum);
            Console.WriteLine(" Draw Number: " + ratingData.drawNum);
            Console.WriteLine(" Out Of Book Moves");
            foreach(OutOfBookData outOfBookData in ratingData.outOfBookDataList)
            {
                Console.WriteLine("     Move: " + outOfBookData.san);
                Console.WriteLine("     Move Number: " + outOfBookData.moveNum);
                Console.WriteLine("     Count: " + outOfBookData.count);
            }
            Console.WriteLine(" Blunders");
            foreach(BlunderSpotData blunderSpotData in ratingData.blunderSpotDataList)
            {
                Console.WriteLine("     Move: " + blunderSpotData.moveNum);
                Console.WriteLine("     Count: " + blunderSpotData.count);
            }
        }
    }  */