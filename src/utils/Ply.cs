namespace PgnAnalyzer.Utils;

public class Ply{
    public Ply(string san, string annotation, string analysis){
        this.san = san;
        this.annotation = annotation;
        this.analysis = analysis;
    }

    public string san {get; set;}
    public string annotation {get; set;}
    public string analysis {get; set;}

    public override string ToString(){

        return $"{san} {annotation} {analysis}";

    }
}   