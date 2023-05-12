namespace PgnAnalyzer;

struct BlunderSpotData
{
    public BlunderSpotData()
    {
        count = 0;
    }

    public int? moveNum {get; set;}
    public string? san {get; set;}
    public int count {get; set;}
}