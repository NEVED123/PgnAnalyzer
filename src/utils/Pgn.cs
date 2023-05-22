using System.Globalization;

namespace PgnAnalyzer.Utils;

public class Pgn : Dictionary<String, Object>
{
    public override string ToString()
    {
        string output = ""; 

        foreach(KeyValuePair<String, Object> tag in this)
        {
            output += $"[{tag.Key} {tag.Value}]\n";
        }

        return output;
    }
}

