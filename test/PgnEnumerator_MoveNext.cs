namespace test;
using PgnAnalyzer.Utils;
using PgnAnalyzer.IO;

public class Tests
{
    PgnReader? reader;

    [SetUp]
    public void Setup()
    {
        reader = new PgnReader("test.pgn");
    }

    [Test]
    public void ReadsGames()
    {
        while(reader!.MoveNext())
        {
            Console.WriteLine(reader.Current);
        }

        Assert.Pass();
    }

    [Test]
    public void ReturnsPgn()
    {
        reader!.MoveNext();

        Pgn pgn = reader.Current;

        StringAssert.IsMatch((string)pgn["Event"], "Rated Blitz game");
    }

    [Test]
    public void ThrowsErrorIfNoCurrent()
    {
        Assert.Throws<InvalidOperationException>(
        () => { Pgn pgn = reader!.Current; } );
    }
}