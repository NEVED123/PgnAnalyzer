namespace PgnAnalyzer.Utils;

public class Game : List<Move>
{
    public Game(string result)
    {
        this.result = result;
    }

    public string result {get; set;}

    public override string ToString()
    {
        string output = "";
        foreach(Move move in this)
        {
            output += move;
        }

        output += $" {result}";
        
        return output;
    }
}