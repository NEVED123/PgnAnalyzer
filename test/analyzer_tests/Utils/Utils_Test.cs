namespace PgnAnalyzer.Test;

public class Move_Test
{
    [Test]
    public void moveParseTest()
    {
        StringAssert.AreEqualIgnoringCase("1. d4 c5", new Move("1. d4 c5").ToString());
        StringAssert.AreEqualIgnoringCase("1. d4 { [%eval 0.2] } 1... d5 { [%eval 0.2] }", new Move("1. d4 { [%eval 0.2] } 1... d5 { [%eval 0.2] }").ToString());
        StringAssert.AreEqualIgnoringCase("10. Be5?! e6??", new Move("10. Be5?! e6??").ToString());
        StringAssert.AreEqualIgnoringCase("23. gxf3?! { [%eval -0.78] } 23... Bxh2!! { [%eval -0.85] }", new Move("23. gxf3?! { [%eval -0.78] } 23... Bxh2!! { [%eval -0.85] }").ToString());
        StringAssert.AreEqualIgnoringCase("e4 {Annotation} e5", new Move("e4 {Annotation} e5").ToString());
        StringAssert.AreEqualIgnoringCase("e4 e5", new Move("e4 e5").ToString());
        StringAssert.AreEqualIgnoringCase("1. e4", new Move("1. e4").ToString());
        StringAssert.AreEqualIgnoringCase("e4 {Annotation}", new Move("e4 {Annotation}").ToString());
        //StringAssert.AreEqualIgnoringCase("1... e5", new Move("1... e5").ToString());
        StringAssert.AreEqualIgnoringCase("e4", new Move("e4").ToString());
    }
}