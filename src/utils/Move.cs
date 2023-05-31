using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

public class Move{

    public Move(Ply whitePly, Ply? blackPly, int moveNum){
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

    private Move Parse(string moveString)
    {
        if(!Regex.Match(moveString, ChessRegex.Move).Success)
        {
            throw new InvalidDataException("Move is not in a valid PGN format");
        }

        string moveNum = Regex.Match(moveString, ChessRegex.MoveNum).Value;

        moveNum = moveNum.Trim(new char[]{' ', '.'});

        //if the move is valid, plys.count == 2
        MatchCollection plys = Regex.Matches(moveString, ChessRegex.Ply);

        Ply? blackPly = null;
        Ply whitePly;

        whitePly = new Ply(plys[0].Value.Trim(' '));

        if(plys.Count == 2)
        {
            blackPly = new Ply(plys[1].Value.Trim(' '));
        }

        return new Move(whitePly, blackPly, int.Parse(moveNum));
    }
    
    public Ply whitePly {get; set;}
    public Ply? blackPly {get; set;}
    public int? moveNum {get; set;}

    public override string ToString(){

        string firstPly = $"{moveNum}. {whitePly}";

        string secondPly = "";

        if(blackPly != null)
        {
            secondPly = $" {blackPly}";

            if(whitePly.annotation != null)
            {
                secondPly = $" {moveNum}..." + secondPly; 
            }
        }

        return firstPly + secondPly; 
    }

}