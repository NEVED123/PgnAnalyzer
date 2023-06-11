namespace PgnAnalyzer.Utils;

public class Pgn 
{
    public Game? game {get; set;}

    private Dictionary<string, object> tags = new Dictionary<string, object>(new CaseInsensitiveStringComparer());

    public override string ToString()
    {
        return ToString(ChessPrintOptions.Default);
    }

    public string ToString(ChessPrintOptions options)
    {
        string output = ""; 

        foreach(KeyValuePair<String, Object> tag in tags)
        {
            output += $"[{tag.Key} \"{tag.Value}\"]\n";
        }

        if(game != null)
        {
            output += $"\n{game.ToString(options)}\n";
        }
        
        return output;
    }

    public object this[string key]
    {
        get
        {
            return tags[key];
        }
        set
        {
            tags[key] = value;
        }
    }

    public void Add(string key, object value)
    {
        tags[key] = value;
    }

    public bool ContainsTag(string key)
    {
        return tags.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return tags.Remove(key);
    }

    public bool TryGetValue(string tag, out object? value)
    {
        return tags.TryGetValue(tag, out value);
    }

    private class CaseInsensitiveStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.ToLowerInvariant().GetHashCode();
        }
    }
}



