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
    public void CorrectDataTypes()
    {
        reader!.MoveNext();

        Pgn pgn = reader.Current;

        StringAssert.IsMatch("Rated Blitz game",(string)pgn["event"]);

        Assert.That(1662, Is.EqualTo((int)pgn["whiteelo"]));

        Assert.That(DateTime.Parse("2013.07.01"), Is.EqualTo((DateTime)pgn["utcdate"]));
    }

    [Test]
    public void ThrowsErrorIfNoCurrent()
    {
        Assert.Throws<InvalidOperationException>(
        () => { Pgn pgn = reader!.Current; } );
    }
}