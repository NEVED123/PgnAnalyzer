namespace PgnAnalyzer.Test;

public class Ply_Test
{
    [Test]
    public void plyParseTest()
    {
        StringAssert.AreEqualIgnoringCase("Be5?! { [%eval -0.3] }", new Ply("Be5?! { [%eval -0.3] }").ToString());
        StringAssert.AreEqualIgnoringCase("d4 { [%eval 0.2] }", new Ply("d4 { [%eval 0.2] }").ToString());
        StringAssert.AreEqualIgnoringCase("Be5?!", new Ply("Be5?!").ToString());
        StringAssert.AreEqualIgnoringCase("d4", new Ply("d4").ToString());
    }

    [Test]
    public void HasAnalysis()
    {
        Assert.True(Ply.Parse("e4??").HasAnalysis());
        Assert.False(Ply.Parse("e4").HasAnalysis());      
    }

}