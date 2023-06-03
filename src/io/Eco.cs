using PgnAnalyzer.Utils;
using System.Text.RegularExpressions;


//TODO: RENAME THIS CLASS TO ECOREADER, AND CREATE AN ECO DATATYPE IN UTILS.
public class Eco
{
    public Eco(string filepath)
    {
        this.filepath = filepath;
    }

    private string filepath;
    private StreamReader? sr;

    public List<Move>? getMovesFromEco(string eco)
    {
        sr = new StreamReader(filepath);

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string ecoFromFile = splitLine[0];

            if(ecoFromFile == eco)
            {
                sr.Close();
                return Game.Parse(splitLine[2]).moves;
            }

            line = sr.ReadLine();
        }

        return null;
    }

    public string getEcoFromMoves(List<Move>? moves)
    {
        if(moves == null)
        {   
            //Unknown opening
            return "A00";
        }

        sr = new StreamReader(filepath);

        //strip down list moves to a string that is equal to the eco file
        List<Move> simplifiedMoves = SimplifyMoves(moves);

        string movesString = "";

        foreach(Move move in simplifiedMoves)
        {
            movesString += $"{move} ";
        }

        movesString = movesString.Replace(".",String.Empty);

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        //Eco for unknown opening
        string bestFitEco = "A00";

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string eco = splitLine[0];
            string ecoMoveText = splitLine[2];

            if(movesString.Contains(ecoMoveText))
            {
                if(ecoMoveText.Length > bestFitEco.Length)
                {
                    bestFitEco = eco;
                }
            }

            line = sr.ReadLine();
        }

        sr.Close();

        return bestFitEco;
    }

    public static List<Move> SimplifyMoves(List<Move> moves)
    {
        List<Move> result = new List<Move>();

        foreach(Move move in moves)
        {   
            Ply? whiteSan = null;

            if(move.whitePly != null)
            {
                whiteSan = new Ply(move.whitePly.san, null, null);
            }
           
            Ply? blackPly = null;

            if(move.blackPly != null)
            {
                blackPly = new Ply(move.blackPly.san, null, null);
            }

            Move simpleMove = new Move(whiteSan, blackPly, move.moveNum);

            result.Add(simpleMove);
        }

        return result;
    }
}