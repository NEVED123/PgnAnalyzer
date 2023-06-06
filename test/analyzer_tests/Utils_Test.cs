namespace PgnAnalyzer.Test;

//TODO: ADD SEPERATE TESTING FOLDER FOR USER WITH BASH SCRIPT THAT ONLY RUNS THOSE  

public class Utils_Test
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

    [Test]
    public void gameParseTest()
    {
        StringAssert.AreEqualIgnoringCase("1-0", new Game("1-0").ToString());
        StringAssert.AreEqualIgnoringCase("", new Game("").ToString());

        StringAssert.AreEqualIgnoringCase("e4 e5 Nf3 Nf6", new Game("e4 e5 Nf3 Nf6").ToString());

        StringAssert.AreEqualIgnoringCase(
            "1. d4 c5 2. Nf3 cxd4 3. Nxd4 e5 4. Nb3 d5 5. g3 Nf6 6. Bg2 Be6 7. O-O Nbd7 8. Bg5 Nb6 9. Nc3 d4 10. Ne4 Be7 11. Bxf6 Bxf6 12. Nxf6+ Qxf6 13. Bxb7 Rb8 14. Bc6+ Bd7 15. Bxd7+ Nxd7 16. e3 dxe3 17. fxe3 Qb6 18. Qd5 Qxe3+ 19. Kh1 Rd8 20. Qxf7# 1-0",
            new Game("1. d4 c5 2. Nf3 cxd4 3. Nxd4 e5 4. Nb3 d5 5. g3 Nf6 6. Bg2 Be6 7. O-O Nbd7 8. Bg5 Nb6 9. Nc3 d4 10. Ne4 Be7 11. Bxf6 Bxf6 12. Nxf6+ Qxf6 13. Bxb7 Rb8 14. Bc6+ Bd7 15. Bxd7+ Nxd7 16. e3 dxe3 17. fxe3 Qb6 18. Qd5 Qxe3+ 19. Kh1 Rd8 20. Qxf7# 1-0").ToString());

        StringAssert.AreEqualIgnoringCase("1. e4 { [%eval 0.25] } 1... e5 { [%eval 0.3] } 2. f4?! { [%eval -0.26] } 2... exf4 { [%eval -0.24] } 3. Nf3 { [%eval -0.29] } 3... Be7 { [%eval -0.24] } 4. Bc4 { [%eval -0.45] } 4... h6? { [%eval 1.71] } 5. d4? { [%eval 0.68] } 5... Nf6 { [%eval 0.2] } 6. Nc3 { [%eval 0.51] } 6... d6 { [%eval 0.73] } 7. Bxf4 { [%eval 0.71] } 7... a6 { [%eval 0.73] } 8. Nd5 { [%eval 0.45] } 8... Nxd5 { [%eval 0.54] } 9. Bxd5 { [%eval 0.46] } 9... c6 { [%eval 0.6] } 10. Bb3 { [%eval 0.69] } 10... Bg4 { [%eval 1.01] } 11. O-O { [%eval 0.97] } 11... Nd7 { [%eval 0.96] } 12. e5 { [%eval 0.65] } 12... d5 { [%eval 0.52] } 13. Qd3 { [%eval 0.49] } 13... c5? { [%eval 2.65] } 14. c4? { [%eval 0.83] } 14... b6?? { [%eval 4.32] } 15. cxd5 { [%eval 4.13] } 15... cxd4? { [%eval 5.27] } 16. Kh1? { [%eval 2.41] } 16... Bxf3?! { [%eval 3.36] } 17. Qxf3 { [%eval 3.28] } 17... Nc5 { [%eval 3.35] } 18. Bc2 { [%eval 3.43] } 18... O-O?! { [%eval 4.39] } 19. Bxh6? { [%eval 2.72] } 19... Bg5? { [%eval 4.55] } 20. Bxg7? { [%eval 2.56] } 20... Kxg7 { [%eval 3.78] } 21. Qg4 { [%eval 5.16] } 1-0",
        new Game("1. e4 { [%eval 0.25] } 1... e5 { [%eval 0.3] } 2. f4?! { [%eval -0.26] } 2... exf4 { [%eval -0.24] } 3. Nf3 { [%eval -0.29] } 3... Be7 { [%eval -0.24] } 4. Bc4 { [%eval -0.45] } 4... h6? { [%eval 1.71] } 5. d4? { [%eval 0.68] } 5... Nf6 { [%eval 0.2] } 6. Nc3 { [%eval 0.51] } 6... d6 { [%eval 0.73] } 7. Bxf4 { [%eval 0.71] } 7... a6 { [%eval 0.73] } 8. Nd5 { [%eval 0.45] } 8... Nxd5 { [%eval 0.54] } 9. Bxd5 { [%eval 0.46] } 9... c6 { [%eval 0.6] } 10. Bb3 { [%eval 0.69] } 10... Bg4 { [%eval 1.01] } 11. O-O { [%eval 0.97] } 11... Nd7 { [%eval 0.96] } 12. e5 { [%eval 0.65] } 12... d5 { [%eval 0.52] } 13. Qd3 { [%eval 0.49] } 13... c5? { [%eval 2.65] } 14. c4? { [%eval 0.83] } 14... b6?? { [%eval 4.32] } 15. cxd5 { [%eval 4.13] } 15... cxd4? { [%eval 5.27] } 16. Kh1? { [%eval 2.41] } 16... Bxf3?! { [%eval 3.36] } 17. Qxf3 { [%eval 3.28] } 17... Nc5 { [%eval 3.35] } 18. Bc2 { [%eval 3.43] } 18... O-O?! { [%eval 4.39] } 19. Bxh6? { [%eval 2.72] } 19... Bg5? { [%eval 4.55] } 20. Bxg7? { [%eval 2.56] } 20... Kxg7 { [%eval 3.78] } 21. Qg4 { [%eval 5.16] } 1-0").ToString());

    }

    [Test]
    public void gameEquals()
    {
        Game game1 = new Game("1. d4 c5 2. Nf3 cxd4 1-0");
        Game game2 = new Game("1. d4 c5 2. Nf3 cxd4 1-0");

        Game game3 = new Game("1. d4 c5 2. Nf3 cxd4");
        Game game4 = new Game("1. d4 c5 2. Nf3 cxd4");

        Game game5 = new Game(null, "1-0");
        Game game6 = new Game(null, "1-0");

        Game game7 = new Game("1-0");
        Game game8 = new Game("1-0");

        Assert.False(game1.Equals(game3));
        Assert.False(game1.Equals(null));
        Assert.True(game1.Equals(game2));
        Assert.True(game3.Equals(game4));
        Assert.True(game5.Equals(game6));
        Assert.True(game7.Equals(game8));

    }

    [Test]
    public void HasAnalysis()
    {
        Assert.True(Ply.Parse("e4??").HasAnalysis());
        Assert.False(Ply.Parse("e4").HasAnalysis());
        Assert.True(Game.Parse("1. e4 { [%eval 0.25] } 1... e5 { [%eval 0.3] } 2. f4?! { [%eval -0.26] } 2... exf4 { [%eval -0.24] } 3. Nf3 { [%eval -0.29] } 3... Be7 { [%eval -0.24] } 4. Bc4 { [%eval -0.45] } 4... h6? { [%eval 1.71] } 5. d4? { [%eval 0.68] } 5... Nf6 { [%eval 0.2] } 6. Nc3 { [%eval 0.51] } 6... d6 { [%eval 0.73] } 7. Bxf4 { [%eval 0.71] } 7... a6 { [%eval 0.73] } 8. Nd5 { [%eval 0.45] } 8... Nxd5 { [%eval 0.54] } 9. Bxd5 { [%eval 0.46] } 9... c6 { [%eval 0.6] } 10. Bb3 { [%eval 0.69] } 10... Bg4 { [%eval 1.01] } 11. O-O { [%eval 0.97] } 11... Nd7 { [%eval 0.96] } 12. e5 { [%eval 0.65] } 12... d5 { [%eval 0.52] } 13. Qd3 { [%eval 0.49] } 13... c5? { [%eval 2.65] } 14. c4? { [%eval 0.83] } 14... b6?? { [%eval 4.32] } 15. cxd5 { [%eval 4.13] } 15... cxd4? { [%eval 5.27] } 16. Kh1? { [%eval 2.41] } 16... Bxf3?! { [%eval 3.36] } 17. Qxf3 { [%eval 3.28] } 17... Nc5 { [%eval 3.35] } 18. Bc2 { [%eval 3.43] } 18... O-O?! { [%eval 4.39] } 19. Bxh6? { [%eval 2.72] } 19... Bg5? { [%eval 4.55] } 20. Bxg7? { [%eval 2.56] } 20... Kxg7 { [%eval 3.78] } 21. Qg4 { [%eval 5.16] } 1-0").HasAnalysis());
        Assert.False(Game.Parse("1. d4 c5 2. Nf3 cxd4 3. Nxd4 e5 4. Nb3 d5 5. g3 Nf6 6. Bg2 Be6 7. O-O Nbd7 8. Bg5 Nb6 9. Nc3 d4 10. Ne4 Be7 11. Bxf6 Bxf6 12. Nxf6+ Qxf6 13. Bxb7 Rb8 14. Bc6+ Bd7 15. Bxd7+ Nxd7 16. e3 dxe3 17. fxe3 Qb6 18. Qd5 Qxe3+ 19. Kh1 Rd8 20. Qxf7# 1-0").HasAnalysis());
    }

    
    [Test]
    public void HasAnnotations()
    {
        Assert.True(Game.Parse("1. e4 { [%eval 0.25] } 1... e5 { [%eval 0.3] } 2. f4?! { [%eval -0.26] } 2... exf4 { [%eval -0.24] } 3. Nf3 { [%eval -0.29] } 3... Be7 { [%eval -0.24] } 4. Bc4 { [%eval -0.45] } 4... h6? { [%eval 1.71] } 5. d4? { [%eval 0.68] } 5... Nf6 { [%eval 0.2] } 6. Nc3 { [%eval 0.51] } 6... d6 { [%eval 0.73] } 7. Bxf4 { [%eval 0.71] } 7... a6 { [%eval 0.73] } 8. Nd5 { [%eval 0.45] } 8... Nxd5 { [%eval 0.54] } 9. Bxd5 { [%eval 0.46] } 9... c6 { [%eval 0.6] } 10. Bb3 { [%eval 0.69] } 10... Bg4 { [%eval 1.01] } 11. O-O { [%eval 0.97] } 11... Nd7 { [%eval 0.96] } 12. e5 { [%eval 0.65] } 12... d5 { [%eval 0.52] } 13. Qd3 { [%eval 0.49] } 13... c5? { [%eval 2.65] } 14. c4? { [%eval 0.83] } 14... b6?? { [%eval 4.32] } 15. cxd5 { [%eval 4.13] } 15... cxd4? { [%eval 5.27] } 16. Kh1? { [%eval 2.41] } 16... Bxf3?! { [%eval 3.36] } 17. Qxf3 { [%eval 3.28] } 17... Nc5 { [%eval 3.35] } 18. Bc2 { [%eval 3.43] } 18... O-O?! { [%eval 4.39] } 19. Bxh6? { [%eval 2.72] } 19... Bg5? { [%eval 4.55] } 20. Bxg7? { [%eval 2.56] } 20... Kxg7 { [%eval 3.78] } 21. Qg4 { [%eval 5.16] } 1-0").HasAnnotations());
        Assert.False(Game.Parse("1. d4 c5 2. Nf3 cxd4 3. Nxd4 e5 4. Nb3 d5 5. g3 Nf6 6. Bg2 Be6 7. O-O Nbd7 8. Bg5 Nb6 9. Nc3 d4 10. Ne4 Be7 11. Bxf6 Bxf6 12. Nxf6+ Qxf6 13. Bxb7 Rb8 14. Bc6+ Bd7 15. Bxd7+ Nxd7 16. e3 dxe3 17. fxe3 Qb6 18. Qd5 Qxe3+ 19. Kh1 Rd8 20. Qxf7# 1-0").HasAnnotations());
    }
}