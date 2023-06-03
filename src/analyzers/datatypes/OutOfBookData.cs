namespace PgnAnalyzer.Analyzer;

public class OutOfBookData
{
    public string? san {get; set;}
    public int count {get; set;} = 0;

    public override string ToString()
    {
        return $"Move: {san}\ncount: {count}\n";
    }
}