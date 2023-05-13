namespace PgnAnalyzer;

struct RatingData
{
    public RatingData()
    {
        whiteWinNum = 0;
        blackWinNum = 0;
        drawNum = 0;
        outOfBookData = new List<OutOfBookData>();
        blunderSpotData = new List<BlunderSpotData>();
    }

    public List<OutOfBookData> outOfBookData {get; set;} = new List<OutOfBookData>();
    public List<BlunderSpotData>? blunderSpotData {get; set;} = new List<BlunderSpotData>();
    public int? eloMin {get; set;}
    public int whiteWinNum {get; set;}
    public int blackWinNum {get; set;}
    public int drawNum {get; set;}
}