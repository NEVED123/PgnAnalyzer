using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

public class Game
{
    public Game(string gameString)
    {
        var game = Parse(gameString);

        this.movelist = game.movelist;
        this.result = game.result;

        SortMoves();
    }

    public Game(List<Move>? movelist, string? result)
    {
        if(movelist != null)
        {
            this.AddMoves(movelist);
        }
        
        this.result = result;
        SortMoves();
    }

    public Game(){}

    public Move? this[int moveNum]
    {
        get{
            return movelist.Find(move => move.moveNum == moveNum);
        }
    }

    public string? result {get; set;}
    private List<Move> movelist = new List<Move>();

    public IList<Move> readonlyMoves
    {
        get
        {
            return movelist.AsReadOnly();
        }
    }

    public override string ToString()
    {
        return ToString(ChessPrintOptions.Default);
    }

    public string ToString(ChessPrintOptions options)
    {
        string output = "";

        if(movelist != null)
        {
            foreach(Move move in movelist)
            {
                output += $"{move.ToString(options)} ";
            }
        }
        if(result != null && (options & ChessPrintOptions.NoResult) != ChessPrintOptions.NoResult)
        {
            output += $"{result}";
        }

        return output.Trim(' '); //Incase of no result

    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Game);
    }

    public bool Equals(Game? obj)
    {
        if(obj == null || this.result != obj.result)
        {
            return false; 
        }

        if(this.movelist == null && obj.movelist == null)
        {
            return true;
        }

        if(this.movelist != null && obj.movelist != null)
        {
            return this.movelist.SequenceEqual(obj.movelist);
        }

        return false;        
    }

    public override int GetHashCode()
    {
        return (movelist, result).GetHashCode();
    }

    public static Game Parse(string game)
    {
        if(!Regex.Match(game, ChessRegex.Game).Success)
        {
            throw new InvalidDataException("Game \"{game}\" is not in a valid PGN format");
        }

        string? gameResult = null;

        var resultMatch = Regex.Match(game, ChessRegex.Result);

        if(resultMatch.Success)
        {
            gameResult = resultMatch.Value.Trim(' ');
        }

        List<Move> moveList = new List<Move>();

        var moveStrings = Regex.Matches(game, ChessRegex.Move);

        if(moveStrings != null)
        {   
            foreach(Match move in moveStrings)
            {
                string moveString = move.Value;

                moveList.Add(new Move(moveString));
            }
        }

       return new Game(moveList, gameResult);
    }

    public void AddMove(Move move)
    {
        AddMoveWithoutSorting(move);
        SortMoves();
    }

    public void AddMoves(IList<Move> moves)
    {
        foreach(Move move in moves)
        {
            AddMoveWithoutSorting(move);
        }
        SortMoves();
    }

    private void AddMoveWithoutSorting(Move move)
    {   
        int? moveNum = move.moveNum;
        Ply? whitePly = move.whitePly;
        Ply? blackPly = move.blackPly;
        
        if(moveNum == null)
        {
            if(movelist.Count == 0)
            {
                moveNum = 1;
            }
            else
            {
                moveNum = movelist.Last().moveNum + 1;
            }
        }
        else if(whitePly == null ^ blackPly == null) //Fancy XOR operator 😎😎
        {
            if(whitePly == null)
            {
               Move? moveToReplace = movelist.Find(moveToReplace => 
                moveToReplace.moveNum == moveNum 
                && moveToReplace.whitePly != null 
                && moveToReplace.blackPly == null); 

                if(moveToReplace != null)
                {
                    movelist.Remove(moveToReplace);
                    whitePly = moveToReplace.whitePly;
                }
            }
            else //blackply is null
            {
               Move? moveToReplace = movelist.Find(moveToReplace => 
                moveToReplace.moveNum == moveNum 
                && moveToReplace.whitePly == null 
                && moveToReplace.blackPly != null); 

                if(moveToReplace != null)
                {
                    movelist.Remove(moveToReplace);
                    blackPly = moveToReplace.whitePly;
                }                
            }
            
        }

        movelist.Add(new Move(whitePly, blackPly, moveNum));
    }

    public void RemoveMove(int moveNum)
    {
        movelist.RemoveAll(move => move.moveNum == moveNum);
    }

    public void ClearMoves()
    {
        movelist.Clear();
    }
    
    private void SortMoves()
    {
        movelist.Sort();
    }

    public bool HasAnalysis()
    {
        if(movelist != null)
        {
            return movelist.Any(move => move.HasAnalysis());
        }

        return false;
    }

    public bool HasAnnotations()
    {
        if(movelist != null)
        {
            return movelist.Any(move => move.HasAnnotations());
        }

        return false;
    }    
}