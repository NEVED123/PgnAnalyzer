using System.Xml.Serialization;

namespace PgnAnalyzer.Analyzer;

public class RatingData
{
    [XmlElement(ElementName = "Out_Of_Book_Moves")]
    public List<OutOfBookData> outOfBookDataList {get; set;} = new List<OutOfBookData>();

    [XmlElement(ElementName = "Elo_Category")]
    public int? eloMin {get; set;}

    [XmlElement(ElementName = "Number_of_White_Wins")]
    public int whiteWinNum {get; set;} = 0;

    [XmlElement(ElementName = "Number_of_Black_Wins")]
    public int blackWinNum {get; set;} = 0;

    [XmlElement(ElementName = "Number_of_Draws")]
    public int drawNum {get; set;} = 0;

    [XmlElement(ElementName = "No_Result_Found")]
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