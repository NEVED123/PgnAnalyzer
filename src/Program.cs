using System.IO;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        if(args[0] == "help")
        {
            Console.WriteLine("Usage: {pathToPgn} [pathToExport] [exportName]");
            Console.WriteLine("Target File must be a .pgn file");
            return;
        }

        string pathToPgn = "";
        string pathToExport = "";
        string exportName = "results";

        switch(args.Length)
        {
            case 0:
                throw new ArgumentException("pgn file must be specified.");
            case 1:
                pathToPgn = args[0];
                break;
            case 2:
                pathToPgn = args[0];
                pathToExport = args[1];
                break;
            case 3:
                pathToPgn = args[0];
                pathToExport = args[1];
                exportName = args[2];
                break;
        }

        PgnAnalysis pgn = new PgnAnalysis(); 

        var results = pgn.analyze(pathToPgn);

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(pathToExport, $"{exportName}.txt")))
        {
            outputFile.WriteLine("---RESULTS---");
   
            foreach (var result in results)
                outputFile.WriteLine(result);
        }

    }
}
