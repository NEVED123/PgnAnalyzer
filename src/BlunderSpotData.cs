namespace PgnAnalyzer;

public struct BlunderSpotData
{
    public BlunderSpotData(int moveNum)
    {
        count = 0;
        this.moveNum = moveNum;
    }
    
    public int moveNum {get; init;}
    public int count {get; set;}
}