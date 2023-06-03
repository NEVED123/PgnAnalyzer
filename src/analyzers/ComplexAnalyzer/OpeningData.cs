using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public class OpeningData
{
    public Eco eco {get;set;} = new Eco();
    public int numGames {get; set;} = 0;
    public List<RatingData> ratingDataList {get; set;} = new List<RatingData>();

    public override string ToString()
    {
        string ecoMoves = "";

        if(eco.moves != null)
        {
            foreach(Move move in eco.moves)
            {
                ecoMoves += $"{move} ";
            } 
        }

        ecoMoves = ecoMoves.Trim(' ');

        string output = "";

        output += $"ECO: {eco.code} - {eco.name}\n";
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
