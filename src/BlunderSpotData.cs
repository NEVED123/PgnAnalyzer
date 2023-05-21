namespace PgnAnalyzer;

public class BlunderSpotData
{
    public BlunderSpotData(int moveNum)
    {
        count = 0;
        this.moveNum = moveNum;
    }
    
    public int moveNum {get; init;}
    public int count {get; set;}

    public override string ToString()
    {
        string output = "";

        output += $" Move Number: {moveNum}\n";
        output += $" Count: {count}\n";

        return output;
    }
}