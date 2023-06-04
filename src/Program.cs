using System.IO;
using System.Reflection;
using PgnAnalyzer.Analyzer;
using PgnAnalyzer.Serializer;
using PgnAnalyzer.IO;

namespace PgnAnalyzer;

//TODO: ADD README
class Program
{
    static void Main(string[] args)
    {
        //arg handling

        if(args.Length == 0 || args.Length == 2)
        {
            Console.WriteLine("\nNot enough arguments. Cannot perform analysis.\n");
            Console.WriteLine("Run 'analyzer --help' or 'bash analyzer --help' for more information.");
            return;
        }

        //Extraneous options
        if(args.Any(arg => arg.ToLower() == "-h" || arg.ToLower() == "--help"))
        {
            showHelpMenu();
            return;
        }

        if(args[0].ToLower() == "--boop" || args[0].ToLower() == "-b")
        {
            Console.Beep();
            return;
        }

        string analyzer = args[0]; 
        string format = args[1];
        string pgnPath = args[2];

        //Configure args
        if(analyzer.Substring(analyzer.Length-3) == ".cs")
        {
            analyzer = analyzer.Replace(".cs", String.Empty);
        }
        
        format = format[0].ToString().ToUpper() + format.Substring(1).ToLower(); //capital case

        pgnPath = Path.GetFullPath(pgnPath);

        //Options
        string exportPath = Path.GetFullPath("results");

        if(args.Contains("--export")|| args.Contains("-x"))
        {
            string flag = args.Contains("--export") ? "--export" : "-x";
            try
            {
                string input = args[args.ToList().IndexOf(flag)+1];
                exportPath = Path.GetFullPath(input);
            }
            catch
            {
                Console.WriteLine("WARNING: File path not found. Exporting to current directory.");
            }
        }

        int progressCount = 10000;

        if(args.Contains("--count")|| args.Contains("-c"))
        {
            string flag = args.Contains("--count") ? "--count" : "-c";
            try
            {
                progressCount = int.Parse(args[args.ToList().IndexOf(flag)+1]); 
            }
            catch(FormatException)
            {
                Console.WriteLine("WARNING: Counter number is not in the proper format. Defaulting to 10000.");
            }
            catch(OverflowException)
            {
                Console.WriteLine("WARNING: Counter number must be in the range of an integer. Defaulting to 10000.");
            }
            finally
            {
                Console.WriteLine("WARNING: Counter number not found. Defaulting to 10000.");
            }
        }

        //Application logic

        IAnalyzer analyzerClass;

        Type? analyzerType = Type.GetType($"PgnAnalyzer.Analyzer.{analyzer}");

        if(analyzerType == null)
        {
            Console.WriteLine("\nAnalyzer Class not found. Make sure the class is in the PgnAnalyzer.Analyzer namespace, and note that class names are case sensitive. Aborting analysis.\n");
            return;
        }

        try
        {
           analyzerClass = (IAnalyzer)Activator.CreateInstance(analyzerType)!;
        }
        catch
        {
            Console.WriteLine("Class could not be instantiated. Make sure the class implements the IAnalyzer interface. Aborting Analysis.");
            return;
        }

        ISerializerWrapper serializer;

        Type? serializerType = Type.GetType($"PgnAnalyzer.Serializer.{format}SerializerWrapper");
        
        if(serializerType == null)
        {
            Console.WriteLine("\nFormatter Class not found. Make sure the class is in the PgnAnalyzer.Serializer namespace, and be sure that the class name should be in the format '[YOUR FILE TYPE]SerializerWrapper. Aborting analysis.\n");
            return;
        }

        try
        {
           serializer = (ISerializerWrapper)Activator.CreateInstance(serializerType)!;
        }
        catch
        {
            Console.WriteLine("Class could not be instantiated. Make sure the class implements ISerializerWrapper. Aborting Analysis.");
            return;
        }

        PgnReader reader;

        try
        {
            reader = new PgnReader(pgnPath);
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine($"Could not find the PGN file at path {pgnPath} - Aborting analysis.");
            Console.WriteLine(@"Make sure the file and path exist. run 'analyzer --help' or 'bash analyzer --help' for more information.");
            return;
        }

        Console.WriteLine("Beginning Analysis...");

        int numGames = 0;

        while(reader.MoveNext())
        {
            analyzerClass.addGame(reader.Current);
            numGames++;
            if(numGames % progressCount == 0)
            {
                Console.WriteLine($"Number of games analyzed: {numGames}");
            }
        }

        serializer.Serialize(exportPath, analyzerClass.getResults());
        Console.WriteLine("Analysis successful.");
        Console.WriteLine($"Analyzed a total of {numGames} games.");
        Console.WriteLine($"Exported at {exportPath}.{format.ToLower()}");   
    }

    private static void showHelpMenu()
    {
        Console.WriteLine("\nExecute PgnAnalyzer.\n");
        Console.WriteLine("Usage: <[bash (Unix only)] analyzer analyzer_class file_format path_to_pgn [options]> | <[-h|--help]> | <[-b|--boop]> \n");
        Console.WriteLine("Arguments:\n");
        Console.WriteLine(" analyzer_class    Name of the class to use for analysis. Class must be in the PgnAnalyzer.Analyzer namespace.\n");
        Console.WriteLine(" file_format    Format of the exported file. Must have a corresponding class in PgnAnalyzer.Serializer with the name '[FILE_TYPE]SerializerWrapper'.\n");
        Console.WriteLine(" path_to_pgn    Path and name of pgn file to analyze\n");
        Console.WriteLine(" -b, --boop    Make a booping sound and abort. No real purpose, but kinda fun sometimes.\n");
        Console.WriteLine(" -h, --help    Show command line help.\n");
        Console.WriteLine("Options:\n");
        Console.WriteLine(" -x, --export <FILE_PATH>    Location and name of the exported file.\n");
        Console.WriteLine(" -c, --count <INTEGER>    Indicate analysis progress after this many games.\n");
    }
}
