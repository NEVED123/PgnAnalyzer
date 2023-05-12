namespace test;

public class Tests
{
    PgnEnumerator enumerator;

    [SetUp]
    public void Setup()
    {
        enumerator = new PgnEnumerator("test.pgn");
    }

    [Test]
    public void ReturnsPgn()
    {
        enumerator.MoveNext();

        Pgn pgn = enumerator.Current;

        StringAssert.IsMatch(pgn["Event"], "Rated Blitz game");
    }

    [Test]
    public void ReturnsAllPgn()
    {
        while(enumerator.MoveNext())
        {
            Console.Write(enumerator.Current);
        }

        Assert.Pass();
    }

    [Test]
    public void ThrowsErrorIfNoCurrent()
    {
        Assert.Throws<InvalidOperationException>(
        () => { Pgn pgn = enumerator.Current; } );
    }
}