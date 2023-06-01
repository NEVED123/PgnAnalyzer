namespace test;
using PgnAnalyzer.Utils;
using PgnAnalyzer.IO;

public class EcoReader_Test
{
    EcoReader? reader;

    [SetUp]
    public void Setup()
    {
        reader = new EcoReader("eco.tsv");
    }

    [Test]
    public void getEcoFromMoves()
    {

        StringAssert.AreEqualIgnoringCase("C23",
        reader!.getEcoFromMoves(Game.Parse("1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 1-0").moves));

        StringAssert.AreEqualIgnoringCase("C20",
        reader.getEcoFromMoves(Game.Parse("1. e4 e5 2. d3 Nc6 3. c3 d5 4. b3 dxe4 5. f3 exd3 6. Bxd3 Nf6 7. Bb2 Bd6 8. Qc2 h7 0-1").moves));

        StringAssert.AreEqualIgnoringCase("B01", reader.getEcoFromMoves(Game.Parse("1. e4 { [%eval 0.21] } 1... d5 2. exd5 { [%eval 0.55] } 2... Qxd5 3. Nc3 { [%eval 0.55] } 3... Qe6+ 4. Be2 { [%eval 0.85] } 4... c5 5. Nf3 { [%eval 1.4] } 5... h6 6. O-O { [%eval 1.46] } 6... Bd7 7. Re1 { [%eval 1.41] } 7... Na6 8. Bxa6 { [%eval 1.12] } 8... Qg6 9. Bd3 { [%eval 5.57] } 9... Bf5 10. Bxf5 { [%eval 6.17] } 10... Qxf5 11. d3 { [%eval 5.03] } 11... O-O-O 12. Ne4 { [%eval 5.09] } 12... b6 13. a4 { [%eval 5.58] } 13... e6 14. a5 { [%eval 6.02] } 14... c4 15. axb6 { [%eval 10.75] } 15... axb6 16. Ra6 { [%eval 6.08] } 16... cxd3 17. cxd3 { [%eval 12.27] } 17... Bb4 18. Qc2+ { [%eval 13.7] } 18... Bc5 19. Rxb6 { [%eval 16.48] } 19... Rd5 20. Rd6 { [%eval 12.45] } 20... Kc7 21. Rxd5 { [%eval 16.47] } 21... Qxd5 22. Qxc5+ { [%eval 9.96] } 22... Qxc5 23. Nxc5 { [%eval 9.88] } 23... Kd6 24. d4 { [%eval 9.63] } 24... Nf6 25. Ne5 { [%eval 9.98] } 25... Ra8 26. b4 { [%eval 9.5] } 26... Nd5 27. Bd2 { [%eval 8.56] } 27... Ra4 28. Nxf7+ { [%eval 16.26] } 28... Kc6 29. Ne5+ { [%eval 13.73] } 29... Kd6 30. Ne4+ { [%eval 10.43] } 30... Ke7 31. Nc6+ { [%eval 10.39] } ").moves));
    }


    [Test]
    public void getMovesFromEcoTest()
    {
        StringAssert.AreEqualIgnoringCase(Game.Parse("1. e4 e5").moves.ToString(), reader!.getMovesFromEco("C20")!.ToString());
    }
    
}