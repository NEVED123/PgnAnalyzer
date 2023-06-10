namespace PgnAnalyzer.Test;
public class Eco_Test
{
    EcoReader? ecoReader;

    [SetUp]
    public void Setup()
    {
        ecoReader = new EcoReader("testeco.tsv");
    }

    [Test]
    public void getEcoFromMoves()
    {
        Eco c23 = new Eco("C23", "Bishop's Opening", Game.Parse("1 e4 e5 2 Bc4").readOnlyMoves);


        Eco b19= new Eco("B19", "Caro-Kann, Classical", Game.Parse("1 e4 c6 2 d4 d5 3 Nc3 dxe4 4 Nxe4 Bf5 5 Ng3 Bg6 6 h4 h6 7 Nf3 Nd7").readOnlyMoves);

        //Console.WriteLine(ecoReader!.getEcoFromMoves(Game.Parse("1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 1-0").readOnlyMoves));
        Assert.True(c23.Equals(ecoReader!.getEcoFromMoves(Game.Parse("1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 1-0").readOnlyMoves)));
        Assert.True(b19.Equals(ecoReader!.getEcoFromMoves(Game.Parse("1. e4 c6 2. d4 d5 3. Nc3 dxe4 4. Nxe4 Bf5 5. Ng3 Bg6 6. h4 h6 7. Nf3 Nd7 8. h5 Bh7 9. Bd3 Bxd3 10. Qxd3 Ngf6 11. Bf4 e6 12. O-O-O Qa5 13. Kb1 Be7 14. Rhe1 Nd5 15. Be3 Nxe3 16. Rxe3 Nf6 17. Qe2 Nd5 18. Rxe6 fxe6 19. Qxe6 Qc7 20. Ne5 Qd6 21. Qf7+ Kd8 22. c4 Qf6 23. Ne4 Qxf7 24. Nxf7+ Kc7 25. Nxh8 Rxh8 26. cxd5 cxd5 27. Nc5 Bf6 28. Ne6+ Kd6 29. Nf4 Re8 30. Ng6 Re2 31. f4 Rxg2 32. f5 Rg4 33. Ne5 Rf4 34. Nf7+ Kc7 35. Nxh6 Rf3 36. Rc1+ Kb6 37. b4 a6 38. a4 a5 0-1").readOnlyMoves)));
    }

    [Test]
    public void getMovesFromEcoTest()
    {
        Eco c20 = new Eco("C20","King's Pawn Game", Game.Parse("1 e4 e5").readOnlyMoves);
        Assert.True(c20.Equals(ecoReader!.GetEcoFromCode("C20")));
    }

    [Test]
    public void tostring()
    {
        Eco c20 = new Eco("C20","King's Pawn Game", Game.Parse("1 e4 e5").readOnlyMoves);

        //Console.WriteLine(c20);

        Assert.Pass();
    }

    [Test]
    public void indexer()
    {
        Eco c23 = new Eco("C23", "Bishop's Opening", Game.Parse("1 e4 e5 2 Bc4").readOnlyMoves);

        Assert.True(c23.Equals(ecoReader!["C23"]));
    }
    
}