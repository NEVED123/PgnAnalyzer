namespace PgnAnalyzer.Utils;

[Flags]
public enum ChessPrintOptions
{
    Default = 0,
    NoAnalysis = 1,
    NoAnnotations = 2,
    NoResult = 4,
}