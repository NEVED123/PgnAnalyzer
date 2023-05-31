using System.Text.Json.Serialization;
using System.Text.Json;
using PgnAnalyzer.IO;

public class JsonSerializerWrapper : ISerializerWrapper
{
    public void Serialize(string filename, object obj)
    {
        string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        filename += ".json";
        File.WriteAllText(filename, jsonString);
    }
}