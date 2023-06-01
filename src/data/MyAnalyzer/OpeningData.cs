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
