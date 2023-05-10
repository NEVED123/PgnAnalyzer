using System.IO;

namespace PgnAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        parse();
    }

    static void parse(){
        StreamReader sr = new StreamReader("games.pgn");

        string line = sr.ReadLine()!;

        while(line != null){
            Console.WriteLine(line);
            line = sr.ReadLine()!;
        }

    }
}
