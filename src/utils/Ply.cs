using System.Text.RegularExpressions;

namespace PgnAnalyzer.Utils;

public class Ply{
    public Ply(string san, string? analysis, string? annotation){
        this.san = san;
        this.annotation = annotation;
        this.analysis = analysis;
    }

    public Ply(string plyString){
        Ply ply = Parse(plyString);

        this.san = ply.san;
        this.annotation = ply.annotation;
        this.analysis = ply.analysis;
    }

    public static Ply Parse(string plyString)
    {
        if(!Regex.Match(plyString, ChessRegex.Ply).Success)
        {
            throw new InvalidDataException("ply \"{plyString}\" is not in proper PGN form");
        }

        string san = Regex.Match(plyString, ChessRegex.SanMove).Value.Trim(' '); 

        string? annotation = null;
        
        var annotationMatch = Regex.Match(plyString, ChessRegex.Annotation);

        if(annotationMatch.Success)
        {
            annotation = annotationMatch.Value.Trim(' ');
        }

        string? analysis = null;
        
        var analysisMatch = Regex.Match(plyString, ChessRegex.Analysis);

        if(analysisMatch.Success)
        {
            analysis = analysisMatch.Value.Trim(' ');
        }

        return new Ply(san, analysis, annotation);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Ply);
    }

    public bool Equals(Ply? obj)
    {
        return obj != null 
        && this.san == obj.san 
        && this.annotation == obj.annotation 
        && this.analysis == this.analysis;
    }

    public override int GetHashCode()
    {
        return (san, annotation, analysis).GetHashCode();
    }

    public string san {get; set;}
    public string? annotation {get; set;}
    public string? analysis {get; set;}

    public override string ToString(){

        string output = san;

        if(analysis != null)
        {
            output += analysis;
        }

        if(annotation != null)
        {
            output += $" {annotation}";
        }
        
        return output;
    }
}   
