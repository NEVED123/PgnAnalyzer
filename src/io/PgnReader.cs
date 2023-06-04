using System.Collections;
using System.Text.RegularExpressions;

namespace PgnAnalyzer.IO;

/*
NOTE: ALL TAGS, EXCLUDING THE MOVETEXT, WILL BE READ AS STRINGS BY DEFAULT.
*/
public class PgnReader : IEnumerator<Pgn>
{
    //TODO: Allow pgn to deal with lack of spacing between tags and game
    //TODO: Implement IEnumerable<T>, set up frame to allow users to use LINQ
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

    private (string Key, string Value) parseTag(string tag)
    {
        string tagNoBrackets = tag.Trim(new char[]{'[', ']'});

        string[] tagArray = tagNoBrackets.Split(' ', 2);

        tagArray[1] = tagArray[1].Trim('"');

        return (tagArray[0], tagArray[1]);
    }

    public void Reset()
    {
        sr.BaseStream.Position = 0;
        sr.DiscardBufferedData();
    }
}