using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

public class Move : IComparable<Move>
{

    public Ply? whitePly {get; set;}
    public Ply? blackPly {get; set;}
    public int? moveNum {get; set;}

    public Move(Ply? whitePly, Ply? blackPly, int? moveNum){
        this.whitePly = whitePly;
        this.blackPly = blackPly;
        this.moveNum = moveNum;
    }

    public Move(string moveString)
    {
        Move move = Parse(moveString);

        this.whitePly = move.whitePly;
        this.blackPly = move.blackPly;
        this.moveNum = move.moveNum; 
    }

    public Move(){}

    public override bool Equals(object? obj)
    {
        return Equals(obj as Move);
    }

    public bool Equals(Move? obj)
    {
        if(obj == null)
        {
            return false;
        }

        if(this.whitePly == null)
        {
            return obj.whitePly == null && 
            this.blackPly!.Equals(obj.blackPly);
        }
        if(this.blackPly == null)
        {
            return obj.blackPly == null &&
            this.whitePly.Equals(obj.whitePly);
        }

        return this.blackPly.Equals(obj.blackPly)
        && this.whitePly.Equals(obj.whitePly);
    }

    public override int GetHashCode()
    {
        return (whitePly, blackPly, moveNum).GetHashCode();
    }
    
    public override string ToString()
    {
        return ToString(ChessPrintOptions.Default);
    }

    public string ToString(ChessPrintOptions options)
    {
        string output = "";

        if(moveNum != null && whitePly != null)
        {
            output += $"{moveNum}. ";
        }

        if(whitePly != null)
        {
            output += $"{whitePly.ToString(options)}";
        }

        if(blackPly != null)
        {
            output += " ";
        }

        if(moveNum != null && blackPly != null 
        && (whitePly == null || whitePly.annotation != null)
        && (options & ChessPrintOptions.NoAnnotations) != ChessPrintOptions.NoAnnotations)
        {
            output += $"{moveNum}... ";
        }

        if(blackPly != null)
        {
            output += $"{blackPly.ToString(options)}";
        }

        return output;
    }

    
    public int CompareTo(Move? other)
    {
        if(other == null || other.moveNum == null || this.moveNum > other.moveNum)
        {
            return 1;
        }

        if(other.moveNum > this.moveNum)
        {
            return -1;
        }

        Console.WriteLine("WARNING: Adding a move with a duplicate move number.");
        return 0;
    }

    public static Move Parse(string moveString)
    {
        if(!Regex.Match(moveString, ChessRegex.Move).Success)
        {
            throw new InvalidDataException($"Move \"{moveString}\" is not in a valid PGN format.");
        }

        int? moveNum = null;

        Match moveNumMatch = Regex.Match(moveString, ChessRegex.MoveNum);

        if(moveNumMatch.Success)
        {
            string temp = moveNumMatch.Value.Trim(new char[]{' ', '.'});
            moveNum = int.Parse(temp);
        }
        
        //if the move is valid, plys.count == 2
        MatchCollection plys = Regex.Matches(moveString, ChessRegex.Ply);

        Ply? blackPly = null;
        Ply? whitePly = null;

        whitePly = new Ply(plys[0].Value.Trim(' '));

        if(plys.Count == 2)
        {
            blackPly = new Ply(plys[1].Value.Trim(' '));
        }

        return new Move(whitePly, blackPly, moveNum);
    }

    public static string ListToString(IList<Move> moves)
    {
        string result = "";

        foreach(Move move in moves)
        {
            result += $"{move} ";
        }

        return result.Trim(' ');
    }

    public bool HasAnalysis()
    {
        if(blackPly != null && whitePly != null)
        {
            return blackPly.HasAnalysis() && whitePly.HasAnalysis();
        }

        return false;
    }

    public bool HasAnnotations()
    {
        if(blackPly != null && whitePly != null)
        {
            return blackPly.HasAnnotations() && whitePly.HasAnnotations();
        }

        return false;
    }

}