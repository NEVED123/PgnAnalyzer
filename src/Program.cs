using System.IO;
using System.Reflection;
using PgnAnalyzer.Analyzer;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        string[] argsLower = new string[args.Length];

        for(int i = 0;i<args.Length;i++)
        {
            argsLower[i] = args[i].ToLower();
        }

        if(argsLower.Contains("help"))
        {
            showHelpMenu();
            return;
        }

        string analyzer = "";
        string format = "";

        try
        {
            analyzer = args[0]; 
            format = argsLower[1];
        }
        catch
        {
            Console.WriteLine("\nArguments not understood. Aborting analysis, commencing boop.\n");
            Console.WriteLine("Run '{dotnet run} help' for more information.");
            Console.Beep();
            return;
        }

        string name = "results";

        if(args.Contains("--name") || args.Contains("-n"))
        {
            try
            {
                name = args[args.ToList().IndexOf("--name")+1];
            }
            catch
            {
                Console.WriteLine("WARNING: File name not found. File name set to results");
            }
        }

        string path = Directory.GetCurrentDirectory() + "";

        if(args.Contains("--path")|| args.Contains("-p"))
        {
            try
            {
                name = args[args.ToList().IndexOf("--name")+1];
            }
            catch
            {
                Console.WriteLine("WARNING: File path not found. Exporting to current directory.");
            }
        }

        IAnalyzer? analyzerClass;

        Type? type = Type.GetType(analyzer);

        if(type == null)
        {
            Console.WriteLine("\nAnalyzer Class not found. Aborting analysis.\n");
            return;
        }

        // analyzerClass = (IAnalyzer?)Activator.CreateInstance(type);
        
        // Console.WriteLine(analyzerClass);

        //find file, make instance of that


        //foreach(game in pgn)
        //  dataclass.addGame(game)

        //Initialize proper serializer

        //dataclass.getresults

        //make file
    }

    private static void showHelpMenu()
    {
        Console.WriteLine("\nExecute PgnAnalyzer.\n");
        Console.WriteLine("Usage: {dotnet run} analyzer_class file_format [options] OR help\n");
        Console.WriteLine("Arguments:\n");
        Console.WriteLine(" analyzer_class    Name of the class to use for analysis.\n");
        Console.WriteLine(" file_format    Format of the exported file.\n");
        Console.WriteLine("Options:\n");
        Console.WriteLine(" help    Show command line help.\n");
        Console.WriteLine(" -n, --name <FILE_NAME>    Name of the exported file.\n");
        Console.WriteLine(" -p, --path <FILE_PATH>    Location of the exported file.\n");
    }
}
