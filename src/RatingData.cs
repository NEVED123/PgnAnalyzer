namespace PgnAnalyzer;

struct RatingData
{
    public RatingData()
    {
        whiteWinNum = 0;
        blackWinNum = 0;
        drawNum = 0;
    }

    public List<OutOfBookData>? outOfBookData {get; set;}
    public List<BlunderSpotData>? blunderSpotData {get; set;}
    public int? eloMin {get; set;}
    public int whiteWinNum {get; set;}
    public int blackWinNum {get; set;}
    public int drawNum {get; set;}
}