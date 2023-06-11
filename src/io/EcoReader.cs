using PgnAnalyzer.Utils;
using System.Text.RegularExpressions;

namespace PgnAnalyzer.IO;

public class EcoReader
{
    private List<Eco> ecoList;
    public EcoReader(string filepath)
    {
        //TODO: Add file type verification
        
        ecoList = new List<Eco>();

        StreamReader sr = new StreamReader(filepath);

        sr.ReadLine(); //burn header

        int ecoFileLine = 1; //for warning/debugging

        string? line;

        using(sr)
        {
            while((line = sr.ReadLine()) != null)
            {
                ecoFileLine++; 
                if(line == String.Empty) continue;

                string[] splitLine = line.Split('\t');

                if(splitLine.Length != 3)
                {
                    Console.WriteLine($"WARNING: Eco at line {ecoFileLine} could not be resolved.");
                    continue;
                }
                if(!Regex.Match(splitLine[2], ChessRegex.GameWithAtLeastOneMove).Success)
                {
                    Console.WriteLine($"WARNING: The following eco movetext could at line {ecoFileLine} not be resolved: {splitLine[2]}");
                }

                string code = splitLine[0];
                string name = splitLine[1];
                IList<Move> moves = Game.Parse(splitLine[2]).readOnlyMoves;
                ecoList.Add(new Eco(code, name, moves));                  
            }
        }
    }

    public Eco? this[string code]
    {
        get
        {
            return ecoList.Find(eco => eco.code == code.ToUpper());
        }
    }

    public Eco? GetEcoFromCode(string code)
    {
        return this[code];
    }

    public Eco getEcoFromMoves(IList<Move>? moves)
    {
        if(moves == null)
        {   
            //Unknown opening
            return new Eco("A00", "Unknown Opening", null);
        }

        //Eco for unknown opening
        Eco? bestFitEco = null;
        int bestFitLength = 0;

        List<Ply> gamePlys = Ply.ToPlyList(moves);
        
        foreach(Eco eco in ecoList)
        {
            List<Ply> ecoPlys = Ply.ToPlyList(eco.moves!);

            if(DiffersAtIndex(gamePlys, ecoPlys) > bestFitLength)
            {
                bestFitEco = eco;
                bestFitLength = ecoPlys.Count;
            }
        }

        if(bestFitEco == null)
        {
            return new Eco("A00", "Unknown Opening", null);
        }

        return bestFitEco;
    }

    private int DiffersAtIndex(List<Ply> p1, List<Ply> p2)
    {
        int index = 0;
        int min = Math.Min(p1.Count, p2.Count);

        while (index < min && p1[index].san == p2[index].san) 
            index++;

        return (index == min && p1.Count == p2.Count) ? -1 : index;
    }
}