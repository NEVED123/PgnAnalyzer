namespace test;
using PgnAnalyzer.Utils;
using PgnAnalyzer.IO;

public class Eco_Test
{
    EcoReader? ecoReader;

    [SetUp]
    public void Setup()
    {
        ecoReader = new EcoReader("eco.tsv");
    }

    [Test]
    public void getEcoFromMoves()
    {
        Eco c23 = new Eco("C23", "Bishop's Opening", Game.Parse("1 e4 e5 2 Bc4").moves);

        Assert.True(c23.Equals(ecoReader!.getEcoFromMoves(Game.Parse("1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 1-0").moves)));
    }


    [Test]
    public void getMovesFromEcoTest()
    {
        Eco c20 = new Eco("C20","King's Pawn Game", Game.Parse("1 e4 e5").moves);
        Assert.True(c20.Equals(ecoReader!.GetEcoFromCode("C20")));
    }
    
}