using System.IO;
using System.Reflection;
using PgnAnalyzer.Analyzer;
using PgnAnalyzer.Serializer;
using PgnAnalyzer.IO;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        //arg handling
        if(args.Any(arg => arg.ToLower() == "help"))
        {
            showHelpMenu();
            return;
        }

        string analyzer = "";
        string format = "";
        string pgnPath = "";

        try
        {
            analyzer = args[0]; 
            format = args[1];
            pgnPath = args[2];
        }
        catch
        {
            Console.WriteLine("\nArguments not understood. Aborting analysis, commencing boop.\n");
            Console.WriteLine("Run '{dotnet run} help' for more information.");
            Console.Beep();
            return;
        }

        //Configure args
        if(analyzer.Substring(analyzer.Length-3) == ".cs")
        {
            analyzer = analyzer.Replace(".cs", String.Empty);
        }

        format = format[0].ToString().ToUpper() + format.Substring(1).ToLower(); //capital case
        pgnPath = Path.GetFullPath(pgnPath);

        //Options

        string exportPath = Directory.GetCurrentDirectory() + @"\results";

        if(args.Contains("--export")|| args.Contains("-e"))
        {
            string flag = args.Contains("--export") ? "--export" : "-e";
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
            Console.WriteLine("Class could not be instantiated. Make sure the class implements IAnalyzer. Aborting Analysis.");
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

        PgnReader reader = new PgnReader(pgnPath);

        while(reader.MoveNext())
        {
            analyzerClass.addGame(reader.Current);
        }

        try
        {
            serializer.Serialize(exportPath, analyzerClass.getResults());
            Console.WriteLine($"Analysis successful. Exported at {exportPath}.{format.ToLower()}");
        }
        catch
        {
            Console.WriteLine("Failed to export results. Aborting Analysis");
        }
        
    }

    private static void showHelpMenu()
    {
        Console.WriteLine("\nExecute PgnAnalyzer.\n");
        Console.WriteLine("Usage: {dotnet run} <analyzer_class file_format path_to_pgn [options]> | help\n");
        Console.WriteLine("Arguments:\n");
        Console.WriteLine(" analyzer_class    Name of the class to use for analysis.\n");
        Console.WriteLine(" file_format    Format of the exported file.\n");
        Console.WriteLine(" path_to_pgn    Path and name of pgn file to analyze");
        Console.WriteLine("Options:\n");
        Console.WriteLine(" -e, --export <FILE_PATH>    Location and name of the exported file.\n");
        Console.WriteLine(" help    Show command line help.\n");
    }
}
