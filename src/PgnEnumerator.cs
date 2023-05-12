using System.Collections;
using System.Text.RegularExpressions;

namespace PgnAnalyzer;

public class PgnEnumerator : IEnumerator<Pgn>
{
    private StreamReader sr;
    private Pgn pgn;

    public PgnEnumerator(string pathToPgn)
    {
        sr = new StreamReader(pathToPgn);
        pgn = new Pgn();
    }
    public Pgn Current
    {
        get{
            return pgn;
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

        while(line != null && Regex.Match(line, @"\[(.*)\]").Success)
        {
            //we are reading a tag
            var tag = parseTag(line);
            pgn[tag.Key] = tag.Value;
            sr.ReadLine();
        }

        line = sr.ReadLine(); //burn whitespace between tags and game

        while(line != null && Regex.Match(line, @"^[^\[](.*)(\d)(.*)[^\]]$").Success)
        {
            //we are reading a game - the regex is redundant here but for saftey
            pgn.game = pgn.game += line;
            line = sr.ReadLine();
        }

        return true;
    }

    private (string Key, string Value) parseTag(string tag)
    {
        string tagNoBrackets = tag.Trim(new char[]{'[', ']'});

        string[] tagArray = tag.Split(' ', 2);

        return (tagArray[0], tagArray[1]);
    }

    public void Reset()
    {
        sr.BaseStream.Position = 0;
        sr.DiscardBufferedData();
    }
}