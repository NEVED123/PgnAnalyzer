using PgnAnalyzer.IO;

namespace PgnAnalyzer.Test;

public class PgnReader_Test
{
    PgnReader? reader;

    [SetUp]
    public void Setup()
    {
        reader = new PgnReader("test.pgn");
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