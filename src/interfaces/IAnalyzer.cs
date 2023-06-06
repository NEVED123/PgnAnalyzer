using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

public interface IAnalyzer
{
    void AddGame(Pgn pgn);

    object Export();
}