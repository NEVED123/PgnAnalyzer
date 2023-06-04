using System.Xml.Serialization;

namespace PgnAnalyzer.Analyzer;

/*
    Data class used in ComplexAnalyzer.cs. 
    Aggregates similar Data classes - see those for more examples.

    Note that all values are initialized, so we have a parameterless constructor
    by default that will appease the serializers.
*/

public class OpeningData
{
    /*
        Serialization attributes allow us to control how object properties
        are serialized. Here, we are using the [XmlElement(string? elementName)]
        attribute to rename the property in the xml file. Visit
        https://learn.microsoft.com/en-us/dotnet/standard/serialization/controlling-xml-serialization-using-attributes
        For more information.
    */
    
    [XmlElement(ElementName = "Eco_Code")]
    public string ecoCode {get;set;} = "A00";

    [XmlElement(ElementName = "Eco_Name")]
    public string ecoName {get;set;} = "Unknown";
    [XmlElement(ElementName = "Eco_Moves")]
    
    public string ecoMoves {get;set;} = "";

    [XmlElement(ElementName = "Number_of_Games")]
    public int numGames {get; set;} = 0;

    [XmlElement(ElementName = "Rating_Data")]
    public List<RatingData> ratingDataList {get; set;} = new List<RatingData>();

    //ToString method is not required
    public override string ToString()
    {
        ecoMoves = ecoMoves.Trim(' ');

        string output = "";

        output += $"ECO: {ecoCode} - {ecoName}\n";
        output += $"Moves: {ecoMoves}";

        output += $" Number of Games: {numGames}\n";


        if(ratingDataList == null || ratingDataList.Count == 0)
        {
            return output;
        }

        output += $"Rating Pools\n";
        foreach(RatingData ratingData in ratingDataList)
        {
            output += $"{ratingData}";
        }

        output += "\n";

        return output;
    }  
}
