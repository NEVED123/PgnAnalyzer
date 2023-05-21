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

    public void toString()
    {
        Console.WriteLine("ECO: " + eco);
        Console.WriteLine("Number of games with this opening: " + numGames);
        Console.WriteLine("Rating Pools:");
        foreach(RatingData ratingData in ratingDataList)
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
    }  
}