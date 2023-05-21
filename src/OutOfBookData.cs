namespace PgnAnalyzer;

public class OutOfBookData
{
    public OutOfBookData(string san, int moveNum)
    {
        count = 0;
        this.san = san;
        this.moveNum = moveNum;
    }
    public string san {get; init;}
    public int moveNum {get; init;}
    public int count {get; set;}

    public override string ToString()
    {
        string output = "";

        output += $"Move: {san}\n";
        output += $" moveNum: {moveNum}\n";
        output += $" count: {count}\n";

        return output;
    }
}