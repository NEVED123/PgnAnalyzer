using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

//TODO: Implement IEquatable or IEqualityComparer
public class Game
{
    public Game(string moveText)
    {
        var game = Parse(moveText);

        this.moves = game.moves;
        this.result = game.result;
    }

    public Game(List<Move>? moves, string? result)
    {
        if(moves != null)
        {
            this.moves = moves;
        }
        
        this.result = result;
    }

    public Game(){}

    public string? result {get; set;}
    public List<Move> moves {get;} = new List<Move>();

    public override string ToString()
    {
        string output = "";

        if(moves != null)
        {
            foreach(Move move in moves)
            {
                output += $"{move} ";
            }
        }
        if(result != null)
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

        if(this.moves == null && obj.moves == null)
        {
            return true;
        }

        if(this.moves != null && obj.moves != null)
        {
            return this.moves.SequenceEqual(obj.moves);
        }

        return false;        
    }

    public override int GetHashCode()
    {
        return (moves, result).GetHashCode();
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
}