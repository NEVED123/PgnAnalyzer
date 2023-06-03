namespace PgnAnalyzer.Serializer;

public interface ISerializerWrapper
{
    void Serialize(string filename, object obj);
}