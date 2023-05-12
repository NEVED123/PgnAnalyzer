using System.Globalization;

namespace PgnAnalyzer;

public class Pgn 
{
    public Pgn(params (string, string)[] tags)
    {
       addTags(tags);
    }

    public void addTags(params (string, string)[] tags)
    {
         foreach((string, string) tag in tags)
        {
            string key = tag.Item1.ToLower();
            string value = tag.Item2;

            switch(key)
            {
                case "event":
                    eventName = value;
                    break;
                case "site":
                    site = value;
                    break;
                case "date":
                case "utcdate":
                    date = value;
                    break;
                case "time":
                case "utctime":
                    time = value;
                    break;
                case "round":
                    round = value;
                    break;
                case "white":
                    white = value;
                    break;
                case "black":
                    black = value;
                    break;
                case "whiteelo":
                    whiteElo = value;
                    break;
                case "blackelo":
                    blackElo = value;
                    break;
                case "whitetitle":
                    whiteTitle = value;
                    break;
                case "blacktitle":
                    blackTitle = value;
                    break;
                case "result":
                    result = value;
                    break;
                case "game":
                    game = value;
                    break;
                case "eco":
                    eco = value;
                    break;
                case "timecontrol":
                    timeControl = value;
                    break;
                case "termination":
                    termination = value;
                    break;
                default:
                    otherTags.Add((key, value));
                    break;
                
            }
        }
    }
    public string eventName { get; set;} = "";
    public string site { get; set;} = "";
    public string date { get; set;} = "";
    public string time  { get; set;} = "";
    public string round { get; set;} = "";
    public string white { get; set;} = "";
    public string black { get; set;} = "";
    public string whiteElo {get; set;} = "";
    public string blackElo {get; set;} = "";
    public string whiteTitle {get; set;} = "";
    public string blackTitle{get; set;} = "";
    public string result { get; set;} = "";
    public string game { get; set;} = "";
    public string eco { get; set;} = "";
    public string timeControl {get; set;} = "";
    public string termination {get; set;} = "";

    public List<(String, String)> otherTags {get;} = new List<(String, String)>();
}

