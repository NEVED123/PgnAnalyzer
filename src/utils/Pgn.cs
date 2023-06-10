namespace PgnAnalyzer.Utils;

public class Pgn 
{
    public Game? game {get; set;}

    private Dictionary<string, object> tags = new Dictionary<string, object>();

    public override string ToString()
    {
        string output = ""; 

        foreach(KeyValuePair<String, Object> tag in tags)
        {
            output += $"[{tag.Key} \"{tag.Value}\"]\n";
        }

        if(game != null)
        {
            output += $"\n{game}\n";
        }
        
        return output;
    }

    public object this[string key]
    {
        get
        {
            return tags[key.ToLower()];
        }
        set
        {
            tags[key] = value;
        }
    }

    public bool ContainsTag(string key)
    {
        return tags.ContainsKey(key.ToLower());
    }

    public bool Remove(string key)
    {
        return tags.Remove(key.ToLower());
    }

    public bool TryGetValue(string tag, out object? value)
    {
        return tags.TryGetValue(tag.ToLower(), out value);
    }
}

