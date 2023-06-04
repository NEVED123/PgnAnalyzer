using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

/*
    Data class used in ComplexAnalyzer.cs. 
    Aggregates similar Data classes - see those for more examples.

    Note that all values are initialized, so we have a parameterless constructor
    by default that will appease the serializers.
*/

public class OpeningData
{
    public string ecoCode {get;set;} = "A00";
    public string ecoName {get;set;} = "Unknown";
    public string ecoMoves {get;set;} = "";
    public int numGames {get; set;} = 0;
    public List<RatingData> ratingDataList {get; set;} = new List<RatingData>();

    //ToString method is not required
    public override string ToString()
    {
        ecoMoves = ecoMoves.Trim(' ');

        string output = "";

        output += $"ECO: {ecoCode} - {ecoName}\n";
        output += $"Moves: {ecoMoves}";

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
