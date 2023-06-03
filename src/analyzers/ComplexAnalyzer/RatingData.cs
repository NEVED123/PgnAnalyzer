namespace PgnAnalyzer.Analyzer;

public class RatingData
{
    public List<OutOfBookData> outOfBookDataList {get; set;} = new List<OutOfBookData>();
    public int? eloMin {get; set;}
    public int whiteWinNum {get; set;} = 0;
    public int blackWinNum {get; set;} = 0;
    public int drawNum {get; set;} = 0;
    public int noResultDataNum {get; set;} = 0;

    public override string ToString()
    {
        string output = "";

        output += $" Elo: >{eloMin}\n";
        output += $" White wins: {whiteWinNum}\n";
        output += $" Black wins: {blackWinNum}\n";
        output += $" Draws: {drawNum}\n";

        if(outOfBookDataList.Count > 0)
        {
            output += "Out Of Book Moves:\n";
            
            foreach(var data in outOfBookDataList!)
            {
                output += $" {data}";
            }
        }

        return output;
    }
}