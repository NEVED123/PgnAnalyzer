namespace PgnAnalyzer;

struct OpeningData
{
    public OpeningData(){
        numGames = 0;
        ratingDataList = new List<RatingData>();
    }
    public string? eco {get; set;}
    public int numGames {get; set;}
    public List<RatingData> ratingDataList {get; set;}
}