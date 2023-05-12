using System.Globalization;

namespace PgnAnalyzer;

public class Pgn : Dictionary<String, String>
{
    public void addGame(string game)
    {
        this.Add("game", game);
    }

}

