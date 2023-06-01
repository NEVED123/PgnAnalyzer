using PgnAnalyzer.Utils;
using System.Text.RegularExpressions;

public class EcoReader
{
    public EcoReader(string filepath)
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
        string movesString = "";

        foreach(Move move in moves)
        {
            movesString += move.ToString() + " ";
        }

        movesString = Regex.Replace(movesString, ChessRegex.Annotation, String.Empty);
        movesString = Regex.Replace(movesString, ChessRegex.Analysis, String.Empty);
        movesString = Regex.Replace(movesString, ChessRegex.BlackMoveNum, String.Empty);
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
}