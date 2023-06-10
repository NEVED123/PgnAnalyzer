using System.Collections;
using System.Text.RegularExpressions;

namespace PgnAnalyzer.IO;

/*
Common Tags this thing understands:
Date/UTCDate - DateTime
    standard is yyyy.mm.dd, ?? for unknown  
        if unable to parse, use DateTime.Min
Time/UTCTime - DateTime
    standard is HH:MM:SS
Round - int
PlyCount - int
WhiteRatingDiff - int
BlackRatingDiff - int
WhiteElo - int
BlackElo - int
Event - String
Site - String
White - String
Black - String
Result - String
Annotator - String
TimeControl - String
Termination - String
Mode - String
FEN - String
ECO - String
Opening - String
*/

public class PgnReader : IEnumerator<Pgn>
{
    private StreamReader sr;
    private Pgn? pgn;

    public PgnReader(string pathToPgn)
    {
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

        //read one pgn
        string? line = sr.ReadLine();

        if(line == null){
            return false;
        }

        while(line != null && Regex.Match(line, ChessRegex.Tag).Success)
        {
            //we are reading a tag

            var tag = parseTag(line);
            pgn[tag.Key] = tag.Value;
            line = sr.ReadLine();
        }

        if(line == null)
        {
            //here the file terminates before reading the game
            pgn = null;
            return false;
        }

        line = sr.ReadLine(); //burn whitespace between tags and game

        string game = "";

        while(line != null && Regex.Matches(line, ChessRegex.Move).Count > 0)
        {
            game += line;
            line = sr.ReadLine();
        }

        pgn.game = new Game(game);

        return true;
    }

    private (string Key, object Value) parseTag(string tag)
    {
        string tagNoBrackets = tag.Trim(new char[]{'[', ']'});

        string[] tagArray = tagNoBrackets.Split(' ', 2);

        string key = tagArray[0].Trim(new char[]{'"',' '}).ToLower();
        string valueString = tagArray[1].Trim(new char[]{'"',' '});

        object? value;

        switch(key)
        {
            case "date":
            case "utcdate":
            case "time":
            case "utctime":
                DateTime.TryParse(valueString, out DateTime date);
                value = date;
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

    public void Reset()
    {
        sr.BaseStream.Position = 0;
        sr.DiscardBufferedData();
    }
}