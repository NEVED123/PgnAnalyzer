namespace PgnAnalyzer.IO;
//TODO: Add validation that file is a proper eco file. (correct number of columns, moves can be parsed, etc.)
//TODO: Add a static list of ECOs that gets filled with imported ECO list upon being initialized.
public class EcoReader
{
    public EcoReader(string filepath)
    {
        this.filepath = filepath;
    }

    private string filepath;
    private StreamReader? sr;

    public Eco GetEcoFromCode(string code)
    {
        sr = new StreamReader(filepath);

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string codeFromFile = splitLine[0];

            if(codeFromFile == code)
            {
                sr.Close();
                string name = splitLine[1];
                List<Move> moves = Game.Parse(splitLine[2]).moves;
                return new Eco(code,name,moves);
            }

            line = sr.ReadLine();
        }

        return new Eco("A00", "Unknown", null);
    }

    public Eco getEcoFromMoves(IEnumerable<Move>? moves)
    {
        if(moves == null)
        {   
            //Unknown opening
            return new Eco("A00", "Unknown", null);
        }

        sr = new StreamReader(filepath);

        //strip down list moves to a string that is equal to the eco file
        List<Move> simplifiedMoves = SanitizeMoves(moves);

        string movesString = "";

        foreach(Move move in simplifiedMoves)
        {
            movesString += $"{move} ";
        }

        movesString = movesString.Replace(".",String.Empty);

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        //Eco for unknown opening
        string bestFitCode = "A00";
        string bestFitName = "Unknown";
        List<Move>? bestFitMoves = null;


        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string eco = splitLine[0];
            string name = splitLine[1];
            string ecoMoveText = splitLine[2];

            if(movesString.Contains(ecoMoveText))
            {
                if(ecoMoveText.Length > bestFitCode.Length)
                {
                    bestFitCode = eco;
                    bestFitName = name;
                    bestFitMoves = Game.Parse(ecoMoveText).moves;
                }
            }

            line = sr.ReadLine();
        }

        sr.Close();

        return new Eco(bestFitCode, bestFitName, bestFitMoves);
    }

    private List<Move> SanitizeMoves(IEnumerable<Move> moves)
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