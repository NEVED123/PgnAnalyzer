using System.Globalization;

namespace PgnAnalyzer;

public class Pgn : Dictionary<String, String>
{
    public string game {get; set;} = "";
}

