using System.IO;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        PgnAnalysis pgn = new PgnAnalysis(); 

        var results = pgn.analyze("games100s.pgn");

        foreach(var opening in results)
        {
            Console.WriteLine(opening);
        }
    }
}
