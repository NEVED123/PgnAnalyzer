using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer;

//TODO: ADD CUSTOM TEXT SERIALIZER
public interface IAnalyzer
{
    void addGame(Pgn pgn);

    object getResults();
}