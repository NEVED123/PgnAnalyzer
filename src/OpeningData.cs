namespace PgnAnalyzer;

struct OpeningData
{
    public OpeningData(){
        numGames = 0;
    }
    public string? eco {get; set;}
    public int numGames {get; set;}
    public List<RatingData>? ratingData {get; set;}
}