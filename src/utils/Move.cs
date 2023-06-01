using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

public class Move{

    public Ply? whitePly {get; set;}
    public Ply? blackPly {get; set;}
    public int? moveNum {get; set;}

    public Move(Ply? whitePly, Ply? blackPly, int? moveNum){

        if(whitePly == null && blackPly == null)
        {
            throw new InvalidDataException("White Ply and Black Ply cannot both be null");
        }

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

       public override bool Equals(object? obj)
    {
        return Equals(obj as Move);
    }

    public bool Equals(Move? obj)
    {
        return obj != null 
        && this.whitePly == obj.whitePly
        && this.blackPly == obj.blackPly
        && this.moveNum == obj.moveNum;
    }

    public override int GetHashCode()
    {
        return (whitePly, blackPly, moveNum).GetHashCode();
    }
    
    public override string ToString(){

        string output = "";

        if(moveNum != null && whitePly != null)
        {
            output += $"{moveNum}. ";
        }

        if(whitePly != null)
        {
            output += $"{whitePly}";
        }

        if(blackPly != null)
        {
            output += " ";
        }

        if(moveNum != null && blackPly != null && (whitePly == null || whitePly.annotation != null))
        {
            output += $"{moveNum}... ";
        }

        if(blackPly != null)
        {
            output += $"{blackPly}";
        }

        return output;
    }

    public static Move Parse(string moveString)
    {
        if(!Regex.Match(moveString, ChessRegex.Move).Success)
        {
            throw new InvalidDataException($"Move \"{moveString}\" is not in a valid PGN format");
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
    


 
}