namespace PgnAnalyzer;

public struct OutOfBookData
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
}