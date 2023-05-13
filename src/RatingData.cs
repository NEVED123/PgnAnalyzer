namespace PgnAnalyzer;

struct RatingData
{
    public RatingData()
    {
        whiteWinNum = 0;
        blackWinNum = 0;
        drawNum = 0;
        outOfBookDataList = new List<OutOfBookData>();
        blunderSpotDataList = new List<BlunderSpotData>();
    }

    public List<OutOfBookData> outOfBookDataList {get; set;} = new List<OutOfBookData>();
    public List<BlunderSpotData>? blunderSpotDataList {get; set;} = new List<BlunderSpotData>();
    public int? eloMin {get; set;}
    public int whiteWinNum {get; set;}
    public int blackWinNum {get; set;}
    public int drawNum {get; set;}
}