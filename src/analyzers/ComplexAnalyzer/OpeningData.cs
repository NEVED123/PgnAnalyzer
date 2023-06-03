using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public class OpeningData
{
    public string? eco {get; set;}
    public int numGames {get; set;} = 0;
    public List<RatingData> ratingDataList {get; set;} = new List<RatingData>();

    public override string ToString()
    {
        string output = "";

        output += $"ECO: {eco}\n";
        output += $" Number of Games: {numGames}\n";


        if(ratingDataList == null || ratingDataList.Count == 0)
        {
            return output;
        }

        output += $"Rating Pools\n";
        foreach(RatingData ratingData in ratingDataList)
        {
            output += $"{ratingData}";
        }

        output += "\n";

        return output;
    }  
}
