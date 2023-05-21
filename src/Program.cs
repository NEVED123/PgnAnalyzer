using System.IO;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        PgnAnalysis pgn = new PgnAnalysis(); 

        var results = pgn.analyze("games.pgn");

        using (StreamWriter outputFile = new StreamWriter("results.txt"))
        {
            outputFile.WriteLine("---RESULTS---");
   
            foreach (var result in results)
                outputFile.WriteLine(result);
        }

    }
}
