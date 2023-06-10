using PgnAnalyzer.Utils;

namespace PgnAnalyzer.IO;

public class EcoReader
{
    private List<Eco> ecoList;
    public EcoReader(string filepath)
    {
        ecoList = new List<Eco>();

        StreamReader sr = new StreamReader(filepath);

        sr.ReadLine(); //burn header

        string? line;

        using(sr)
        {
            while((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split('\t');

                string code = splitLine[0];
                string name = splitLine[1];
                IList<Move> moves = Game.Parse(splitLine[2]).readOnlyMoves;

                ecoList.Add(new Eco(code, name, moves));                
                //Console.WriteLine($"{code}, {name}, {Move.ListToString(moves)}");
            }
        }
    }

    public Eco? this[string code]
    {
        get
        {
            return ecoList.Find(eco => eco.code == code);
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