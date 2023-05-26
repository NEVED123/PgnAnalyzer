using PgnAnalyzer.IO;
using System.Xml.Serialization;

public class XmlSerializerWrapper : ISerializerWrapper
{
    public void Serialize(string filename, object obj)
    {
        filename += ".xml";
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        serializer.Serialize(new StreamWriter(filename),obj);
    }
}