using System.Xml.Serialization;

namespace PgnAnalyzer.Analyzer;

public class OutOfBookData
{
    [XmlElement(ElementName = "San")]
    public string? san {get; set;}

    [XmlElement(ElementName = "Count")]
    public int count {get; set;} = 0;

    public override string ToString()
    {
        return $"Move: {san}\ncount: {count}\n";
    }
}