namespace PgnAnalyzer;

public class RatingData
{
    public RatingData(int eloMin)
    {
        whiteWinNum = 0;
        blackWinNum = 0;
        drawNum = 0;
        outOfBookDataList = new List<OutOfBookData>();
        blunderSpotDataList = new List<BlunderSpotData>();
        this.eloMin = eloMin;
    }

    public List<OutOfBookData> outOfBookDataList {get; set;} = new List<OutOfBookData>();
    public List<BlunderSpotData> blunderSpotDataList {get; set;} = new List<BlunderSpotData>();
    public int eloMin {get; init;}
    public int whiteWinNum {get; set;}
    public int blackWinNum {get; set;}
    public int drawNum {get; set;}

    public override string ToString()
    {
        string output = "";

        output += $" Elo: >{eloMin}\n";
        output += $" White wins: {whiteWinNum}\n";
        output += $" Black wins: {blackWinNum}\n";
        output += $" Draws: {drawNum}\n";

        output += "Out Of Book Moves:\n"
;
        foreach(var data in outOfBookDataList)
        {
            output += $" {data}";
        }

        output += "Blunders: ";

        if(blunderSpotDataList.Count == 0)
        {
            output += "No Data";
        }
        else{
            foreach(var data in blunderSpotDataList)
            {
                output += $" {data}";
            }
        }




        return output;
    }
}