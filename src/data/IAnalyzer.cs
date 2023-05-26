using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Data;

public interface IAnalyzer
{
    void addGame(Pgn pgn);

    object getResults();
}