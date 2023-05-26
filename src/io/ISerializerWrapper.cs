namespace PgnAnalyzer.IO;

public interface ISerializerWrapper
{
    void Serialize(string filename, object obj);
}