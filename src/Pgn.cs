using System.Globalization;

namespace PgnAnalyzer;

public class Pgn : Dictionary<String, String>
{
    public override string ToString()
    {
        string output = ""; 

        foreach(KeyValuePair<String, String> tag in this)
        {
            output += $"[{tag.Key} {tag.Value}]\n";
        }

        return output;
    }
}

