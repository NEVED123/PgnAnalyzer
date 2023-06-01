using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public interface IAnalyzer
{
    void addGame(Pgn pgn);

    object getResults();
}