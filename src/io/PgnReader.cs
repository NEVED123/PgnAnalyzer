using System.Collections;
using System.Text.RegularExpressions;

namespace PgnAnalyzer.IO;

public class PgnReader : IEnumerator<Pgn>
{
    private StreamReader sr;
    private Pgn? pgn;

    private string? line = null;

    private int PgnFileLine = 0;
    private string pathToPgn;

    public PgnReader(string pathToPgn)
    {
        this.pathToPgn = pathToPgn;
        sr = new StreamReader(pathToPgn);
    }
    public Pgn Current
    {
        get
        {
            if(pgn != null)
            {
                return pgn;
            }
            else    
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current
    {
        get{
            return Current;
        }
    }

    public void Dispose()
    {
        sr.Close();
    }

    public bool MoveNext()
    {
        pgn = new Pgn();

        bool pgnScanned = false;

        string pgnElementsRegex = $"{ChessRegex.Tag}|{ChessRegex.GameWithAtLeastOneMove}";

        if(line == null)
        {
            line = sr.ReadLine(); //initializes file reading, or we are at end of pgn
            PgnFileLine++;
        }

        while(!pgnScanned && line != null)
        {
            if(line == null || line == String.Empty)
            {
                line = sr.ReadLine();
                PgnFileLine++;
                continue;
            }

            if(Regex.Matches(line, pgnElementsRegex).Count != 1)
            {
                //we are not reading an empty line or a typical pgn element.
                Console.WriteLine($"WARNING: Line {PgnFileLine} of {pathToPgn} is improperly formatted. This may result in faulty parsing.");
                line = sr.ReadLine();
                PgnFileLine++;
                continue;
            }

            if(Regex.Match(line, ChessRegex.Tag).Success)
            {
                var tag = parseTag(line);
                pgn[tag.Key] = tag.Value;
                line = sr.ReadLine();
                PgnFileLine++;
                continue;
            }

            string moveText = "";

            //If we reach this code, we are now expecting a game, although its possible there is none
            while(!pgnScanned)
            {
                if(line != null && Regex.Matches(line, ChessRegex.GameWithAtLeastOneMove).Count == 1)
                {
                    moveText += line + " ";
                    line = sr.ReadLine();
                    PgnFileLine++;
                }
                else
                {
                    pgn.game = Game.Parse(moveText);
                    pgnScanned = true;                    
                }
            }
        }

        Console.WriteLine(pgn);

        return pgnScanned;
    }

    private (string Key, object Value) parseTag(string tag)
    {
        string tagNoBrackets = tag.Trim(new char[]{'[', ']'});

        string[] tagArray = tagNoBrackets.Split(' ', 2);

        string key = tagArray[0].Trim(new char[]{'"',' '}).ToLower();
        string valueString = tagArray[1].Trim(new char[]{'"',' '}).ToLower();

        object? value;

        switch(key)
        {
            case "date":
            case "utcdate":
                value = ParsePgnDate(valueString);
            break;
            case "time":
            case "utctime":
                value = ParsePgnTime(valueString);
            break;
            case "round":
            case "plycount":
            case "whiteratingdiff":
            case "blackratingdiff":
            case "whiteelo":
            case "blackelo":
                int.TryParse(valueString, out int round);
                value = round;
            break;
            default:
                value = valueString;
            break;
        }
        
        return (key, value);
    }

    private DateTime ParsePgnDate(string input)
    {
        DateTime.TryParseExact(input,"yyyy.MM.dd",null,System.Globalization.DateTimeStyles.None, out DateTime date);

        return new DateTime(date.Year,date.Month,date.Day,0,0,0);
    }

    private DateTime ParsePgnTime(string input)
    {
        DateTime.TryParseExact(input,"HH:mm:ss",null, System.Globalization.DateTimeStyles.None,out DateTime time);

        return new DateTime(1,1,1,time.Hour,time.Minute,time.Second);
    }

    public void Reset()
    {
        sr.BaseStream.Position = 0;
        sr.DiscardBufferedData();
    }
}