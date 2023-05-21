namespace test;

public class PgnAnalyzer_Test
{

    //white makes the first blunder, 28. g5
    public string moveTextWhiteBlunder = "1. d4 { [%eval 0.2] } 1... d5 { [%eval 0.2] } 2. Nc3 { [%eval 0.09] } 2... c6 { [%eval 0.37] } 3. Bf4 { [%eval 0.32] } 3... Nf6 { [%eval 0.26] } 4. Be5?! { [%eval -0.3] } 4... e6 { [%eval -0.24] } 5. Bxf6 { [%eval -0.19] } 5... Qxf6 { [%eval -0.14] } 6. a3 { [%eval -0.2] } 6... Bd6 { [%eval -0.01] } 7. e4 { [%eval -0.2] } 7... e5 { [%eval -0.11] } 8. exd5 { [%eval -0.37] } 8... O-O { [%eval 0.01] } 9. dxe5?! { [%eval -0.8] } 9... Qxe5+? { [%eval 0.4] } 10. Qe2 { [%eval 0.04] } 10... cxd5 { [%eval 0.08] } 11. Qxe5 { [%eval -0.21] } 11... Bxe5 { [%eval -0.18] } 12. Nxd5 { [%eval -0.48] } 12... Bxb2 { [%eval -0.36] } 13. Ra2 { [%eval -0.76] } 13... Be5 { [%eval -0.69] } 14. Nf3 { [%eval -0.74] } 14... Bd6 { [%eval -0.72] } 15. Bb5 { [%eval -1.09] } 15... a6 { [%eval -0.67] } 16. Ba4 { [%eval -1.02] } 16... b5 { [%eval -0.91] } 17. Bb3 { [%eval -0.81] } 17... Re8+ { [%eval -0.74] } 18. Kd1?! { [%eval -1.63] } 18... Be6?! { [%eval -1.11] } 19. Nb6 { [%eval -1.46] } 19... Bxb3? { [%eval 0.0] } 20. cxb3 { [%eval 0.0] } 20... Ra7 { [%eval -0.07] } 21. Re1 { [%eval 0.0] } 21... Rxe1+ { [%eval 0.0] } 22. Kxe1 { [%eval 0.0] } 22... Rb7 { [%eval 0.02] } 23. Nd5 { [%eval 0.0] } 23... Nc6 { [%eval 0.05] } 24. b4 { [%eval 0.0] } 24... Ne5 { [%eval 0.29] } 25. Ke2 { [%eval 0.14] } 25... Nxf3 { [%eval 0.17] } 26. gxf3?! { [%eval -0.78] } 26... Bxh2 { [%eval -0.85] } 27. f4 { [%eval -0.72] } 27... Rd7 { [%eval -0.79] } 28. Rd2 { [%eval -1.22] } 28... g5?? { [%eval 5.33] } 29. Kf3?? { [%eval 0.16] } 29... gxf4?? { [%eval 9.24] } 30. Rd4?? { [%eval 1.62] } 1-0";
    //black makes first blunder, 14. b6
    public string moveTextBlackBlunder = "1. e4 { [%eval 0.25] } 1... e5 { [%eval 0.3] } 2. f4?! { [%eval -0.26] } 2... exf4 { [%eval -0.24] } 3. Nf3 { [%eval -0.29] } 3... Be7 { [%eval -0.24] } 4. Bc4 { [%eval -0.45] } 4... h6? { [%eval 1.71] } 5. d4? { [%eval 0.68] } 5... Nf6 { [%eval 0.2] } 6. Nc3 { [%eval 0.51] } 6... d6 { [%eval 0.73] } 7. Bxf4 { [%eval 0.71] } 7... a6 { [%eval 0.73] } 8. Nd5 { [%eval 0.45] } 8... Nxd5 { [%eval 0.54] } 9. Bxd5 { [%eval 0.46] } 9... c6 { [%eval 0.6] } 10. Bb3 { [%eval 0.69] } 10... Bg4 { [%eval 1.01] } 11. O-O { [%eval 0.97] } 11... Nd7 { [%eval 0.96] } 12. e5 { [%eval 0.65] } 12... d5 { [%eval 0.52] } 13. Qd3 { [%eval 0.49] } 13... c5? { [%eval 2.65] } 14. c4? { [%eval 0.83] } 14... b6?? { [%eval 4.32] } 15. cxd5 { [%eval 4.13] } 15... cxd4? { [%eval 5.27] } 16. Kh1? { [%eval 2.41] } 16... Bxf3?! { [%eval 3.36] } 17. Qxf3 { [%eval 3.28] } 17... Nc5 { [%eval 3.35] } 18. Bc2 { [%eval 3.43] } 18... O-O?! { [%eval 4.39] } 19. Bxh6? { [%eval 2.72] } 19... Bg5? { [%eval 4.55] } 20. Bxg7? { [%eval 2.56] } 20... Kxg7 { [%eval 3.78] } 21. Qg4 { [%eval 5.16] } 1-0";

    public string moveTextNoAnalysis = "1. d4 c5 2. dxc5 Qa5+ 3. Nd2 Qxc5 4. Nb3 Qc7 5. Nf3 d6 6. e4 e5 7. Bd3 Bg4 8. h3 Bxf3 9. Qxf3 Nf6 10. Bg5 Be7 11. O-O a5 12. c3 a4 13. Nd2 h6 0-1";

    public PgnAnalysis pgnAnalysis = new PgnAnalysis(); 
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void getFirstBlunderTest()
    {
        Assert.AreEqual(28, pgnAnalysis.getFirstBlunderNum(moveTextWhiteBlunder));
        Assert.AreEqual(14, pgnAnalysis.getFirstBlunderNum(moveTextBlackBlunder));
    }

    [Test]
    public void getFirstBlunderSanTest()
    {
        StringAssert.AreEqualIgnoringCase("g5",pgnAnalysis.getFirstBlunder(moveTextWhiteBlunder));
        StringAssert.AreEqualIgnoringCase("b6",pgnAnalysis.getFirstBlunder(moveTextBlackBlunder));
    }


    [Test]
    public void hasAnalysisTest()
    {
        Assert.IsTrue(pgnAnalysis.hasAnalysis(moveTextWhiteBlunder));
        Assert.IsFalse(pgnAnalysis.hasAnalysis(moveTextNoAnalysis));
    }

    [Test]
    public void symmetricMoveDifferenceTest()
    {
        //A37
        string eco = "1 c4 c5 2 Nc3 Nc6 3 g3 g6 4 Bg2 Bg7 5 Nf3";
        string game = "1. Nf3 c5 2. g3 Nc6 3. Bg2 g6 4. c4 Bg7 5. Nc3 a6 6. O-O Rb8 7. a4 Nb4 8. d4 cxd4 9. Nxd4 Nf6 10. e4 d6 11. a5 O-O 12. h3 Bd7 13. Be3 Bc6 14. Nxc6 bxc6 15. Qd2 Nd7 16. Bd4 Ne5 17. b3 c5 18. Be3 Ned3 19. Rfb1 Qd7 20. Ra4 Rb7 21. Nd5 Qd8 22. Nb6 Rxb6 23. axb6 Qxb6 24. f4 Rb8 25. Kh2 Bh6 26. e5 dxe5 27. fxe5 Rd8 28. Bxh6 Nxe5 29. Qe3 f6 30. Bf4 Rd3 31. Qe2 Kf7 32. Bxe5 fxe5 33. Rxb4 cxb4 34. Qxd3 1-0";
        string expected = "a6 6. O-O Rb8 7. a4 Nb4 8. d4 cxd4 9. Nxd4 Nf6 10. e4 d6 11. a5 O-O 12. h3 Bd7 13. Be3 Bc6 14. Nxc6 bxc6 15. Qd2 Nd7 16. Bd4 Ne5 17. b3 c5 18. Be3 Ned3 19. Rfb1 Qd7 20. Ra4 Rb7 21. Nd5 Qd8 22. Nb6 Rxb6 23. axb6 Qxb6 24. f4 Rb8 25. Kh2 Bh6 26. e5 dxe5 27. fxe5 Rd8 28. Bxh6 Nxe5 29. Qe3 f6 30. Bf4 Rd3 31. Qe2 Kf7 32. Bxe5 fxe5 33. Rxb4 cxb4 34. Qxd3 1-0";
        StringAssert.AreEqualIgnoringCase(expected, pgnAnalysis.symmetricMoveDifference(game, eco));

        eco = "1 e4 e6";
        game = "1. e4 e6 2. Nf3 d6 3. Nc3 c6 4. d4 Nd7 5. Bc4 b6 6. d5 Bb7 7. dxe6 fxe6 8. Bxe6 Qf6 9. Bxd7+ Kxd7 10. Bg5 Qg6 11. O-O Nf6 12. e5 Ne4 13. exd6 Nxg5 14. Ne5+ Kd8 15. Nxg6 hxg6 16. Re1 c5 17. d7 1-0";
        expected = "2. Nf3 d6 3. Nc3 c6 4. d4 Nd7 5. Bc4 b6 6. d5 Bb7 7. dxe6 fxe6 8. Bxe6 Qf6 9. Bxd7+ Kxd7 10. Bg5 Qg6 11. O-O Nf6 12. e5 Ne4 13. exd6 Nxg5 14. Ne5+ Kd8 15. Nxg6 hxg6 16. Re1 c5 17. d7 1-0";

        StringAssert.AreEqualIgnoringCase(expected, pgnAnalysis.symmetricMoveDifference(game, eco));
    }
    

    [Test]
    public void getFirstMoveSan()
    {
        string whiteFirstNoAnalysis = "5. Nf3 d6 6. e4 e5 7. Bd3 Bg4 8. h3 Bxf3 9. Qxf3 Nf6 10. Bg5 Be7 11. O-O a5 12. c3 a4 13. Nd2 h6 0-1"; 
        StringAssert.AreEqualIgnoringCase(pgnAnalysis.getFirstMoveSan(whiteFirstNoAnalysis), "Nf3");

        string blackFirstNoAnalysis = "Bg4 8. h3 Bxf3 9. Qxf3 Nf6 10. Bg5 Be7 11. O-O a5 12. c3 a4 13. Nd2 h6 0-1";
        StringAssert.AreEqualIgnoringCase(pgnAnalysis.getFirstMoveSan(blackFirstNoAnalysis), "Bg4");
    }

    [Test]
    public void getFirstMoveNumTest()
    {
        Assert.AreEqual(pgnAnalysis.getFirstMoveNum(moveTextNoAnalysis), 1);

        string blackFirst = "Qc7 5. Nf3 d6 6. e4 e5 7. Bd3 Bg4 8. h3 Bxf3 9. Qxf3 Nf6 10. Bg5 Be7 11. O-O a5 12. c3 a4 13. Nd2 h6 0-1";

        Assert.AreEqual(pgnAnalysis.getFirstMoveNum(blackFirst), 4);
    }

    [Test]
    public void getEcoFromStringTest()
    {
        string game = "1. e4 e5 2. d3 Nc6 3. c3 d5 4. b3 dxe4 5. f3 exd3 6. Bxd3 Nf6 7. Bb2 Bd6 8. Qc2 h7 0-1";
        StringAssert.AreEqualIgnoringCase(pgnAnalysis.getEcoFromString(game), "C20");
    }

    [Test]
    public void getStringFromEcoTest()
    {
        StringAssert.AreEqualIgnoringCase(pgnAnalysis.getStringFromEco("C20"), "1 e4 e5");
    }

    [Test]
    public void getResultFromStringTest()
    {
        StringAssert.AreEqualIgnoringCase(pgnAnalysis.getResultFromString(moveTextWhiteBlunder), "1-0");
    }

    [Test]
    public void getAverageRatingTest()
    {
        Pgn pgn = new Pgn();

        pgn["WhiteElo"] = "1000";
        pgn["BlackElo"] = "2000";

        Assert.AreEqual(pgnAnalysis.getAverageRating(pgn), 1500);
    }

    [Test]
    public void getEloBasketTest()
    {
        Pgn pgn = new Pgn();

        pgn["WhiteElo"] = "1000";
        pgn["BlackElo"] = "1500";

        Assert.AreEqual(pgnAnalysis.getEloBasket(pgn), 1000);
    }

    [Test]
    public void stripCommentsFromGameTest()
    {
        string game = "1. d4 { [%eval 0.2] } 1... d5 { [%eval 0.2] } 2. Nc3 { [%eval 0.09] } 2... c6 { [%eval 0.37] } 3. Bf4 { [%eval 0.32] } 3... Nf6 { [%eval 0.26] }";
        string noComments = "1. d4 d5 2. Nc3 c6 3. Bf4 Nf6"; 
        StringAssert.AreEqualIgnoringCase(noComments, pgnAnalysis.stripCommentsFromGame(game));
    }

    [Test]
    public void analysisTest(){
        var results = pgnAnalysis.analyze("games100s.pgn");

        foreach(var data in results)
        {
            printOpening(data);
        }

        Assert.Pass();

    }

    private void printOpening(OpeningData data)
    {
        Console.WriteLine("ECO: " + data.eco);
        Console.WriteLine("Number of games with this opening: " + data.numGames);
        Console.WriteLine("Rating Pools:");
        foreach(RatingData ratingData in data.ratingDataList)
        {
            Console.WriteLine(" Rating: " + ratingData.eloMin);
            Console.WriteLine(" White Win Number: " + ratingData.whiteWinNum);
            Console.WriteLine(" Black Win Number: " + ratingData.blackWinNum);
            Console.WriteLine(" Draw Number: " + ratingData.drawNum);
            Console.WriteLine(" Out Of Book Moves");
            foreach(OutOfBookData outOfBookData in ratingData.outOfBookDataList)
            {
                Console.WriteLine("     Move: " + outOfBookData.san);
                Console.WriteLine("     Move Number: " + outOfBookData.moveNum);
                Console.WriteLine("     Count: " + outOfBookData.count);
            }
            Console.WriteLine(" Blunders");
            foreach(BlunderSpotData blunderSpotData in ratingData.blunderSpotDataList)
            {
                Console.WriteLine("     Move: " + blunderSpotData.moveNum);
                Console.WriteLine("     Count: " + blunderSpotData.count);
            }
        }
    }  
        
}