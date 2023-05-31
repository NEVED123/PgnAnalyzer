using System.Globalization;

namespace PgnAnalyzer.Utils;

public class Pgn : Dictionary<String, Object>
{
    public Game? game {get; set;}

    public override string ToString()
    {
        string output = ""; 

        foreach(KeyValuePair<String, Object> tag in this)
        {
            output += $"[{tag.Key} {tag.Value}]\n";
        }

        if(game != null)
        {
            output += $"\n{game}\n";
        }
        
        return output;
    }
}

