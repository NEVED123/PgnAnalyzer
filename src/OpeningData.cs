namespace PgnAnalyzer;

public struct OpeningData
{
    public OpeningData(string eco){
        numGames = 0;
        ratingDataList = new List<RatingData>();
        this.eco = eco;
    }
    public string eco {get; init;}
    public int numGames {get; set;}
    public List<RatingData> ratingDataList {get; set;}
}