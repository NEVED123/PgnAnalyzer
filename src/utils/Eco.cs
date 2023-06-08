namespace PgnAnalyzer.Utils;

public class Eco
{
    public Eco(string? code, string? name, IList<Move>? moves) 
    {
        this.code = code;
        this.name = name;

        if(moves != null)
        {
            this.moves = moves;
        }
    }

    public Eco(){}
    public string? code {get; set;}
    public string? name {get;set;}
    public IList<Move>? moves {get;set;} = new List<Move>();

    public override string ToString()
    {
        string output = $"Code: {code}\nName: {name}\nMoves: ";

        if(moves != null)
        {
            output += Move.ListToString(moves);
        }
        else
        {
            output += "No information";
        }

        return output;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Eco);
    }

    public bool Equals(Eco? obj)
    {
        if(obj == null || this.code != obj.code || this.name != obj.name)
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
        return (code, name, moves).GetHashCode();
    }
}