using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public class SimpleAnalyzer : IAnalyzer
{
    private int totalRatingPoints;
    private int numGames;
    private double result;

    public void addGame(Pgn pgn)
    {
        if(!pgn.ContainsKey("WhiteElo") || !pgn.ContainsKey("BlackElo"))
        {
            return;
        }

        int whiteElo = 0;
        int blackElo = 0;

        int.TryParse((string)pgn["WhiteElo"], out whiteElo);
        int.TryParse((string)pgn["BlackElo"], out blackElo);

        totalRatingPoints += whiteElo + blackElo;
        numGames++;
    }

    public object getResults()
    {
        result = totalRatingPoints/(numGames * 2);

        return result;
    }
}