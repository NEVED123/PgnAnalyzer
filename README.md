# PgnAnalyzer
.NET Console Application that Gathers useful data about openings from a large set of chess PGNs, and writes it to a text file.

NOTE - This is still under development. There will be more flexibility for analysis and export options in the future, as well as more usage options.

## Installation

If you are not using Windows, you will need to have the .NET Framework installed on your machine. Search how to do this on your operating system - Microsoft hates you if you don't use Windows so good luck.

### With Git
1. If you do not have git installed, do so [here](https://git-scm.com/downloads)
2. Open the command line and change the directory to where you want to install.
3. Run ``git clone https://github.com/NEVED123/PgnAnalyzer.git``

### Without Git
1. Click on Code -> Download ZIP and extract wherever you like
2. Open the command line in the project directory

## Usage
1. To tell the .NET framework that you want to run the main script, you must run ``cd src`` so that the project file is in scope. 
2. Run ``dotnet run path/to/pgn.pgn [path/to/export] [exportedFileName]``

   * ``[path/to/pgn.pgn]`` : Required - Path to the PGN file. Note that the pgn must be formated such that there is a space between the tags and games, and that there is a space between each game. An example of a proper PGN format is shown below. 
   * ``[path/to/export]`` : Optional - Path of exported file. If not specified, the file will be exported in the working directory.
   * ``[exportedFileName]`` : Optional - Name of exported file. If not specified, file will be named ``results.txt``. If you specify this, you must also specify the export path.

You can get PGN's from your own games through lichess and chess.com. If you want to use large PGN samples, refer to the lichess [game database](https://database.lichess.org/)

## Example

Run ``dotnet run ../games.pgn C:\Users\neved\OneDrive\Desktop analysis``

Games.pgn (Note that we can accept games with or without analysis):
```

[Event "Rated Classical game"]
[Site "https://lichess.org/0ebuwdjy"]
[White "dopi"]
[Black "FF4"]
[Result "1-0"]
[UTCDate "2013.07.07"]
[UTCTime "17:04:14"]
[WhiteElo "1664"]
[BlackElo "1467"]
[WhiteRatingDiff "+8"]
[BlackRatingDiff "-6"]
[ECO "B01"]
[Opening "Scandinavian Defense: Mieses-Kotroc Variation"]
[TimeControl "0+30"]
[Termination "Time forfeit"]

1. e4 { [%eval 0.21] } 1... d5 { [%eval 0.5] } 2. exd5 { [%eval 0.55] } 2... Qxd5 { [%eval 0.67] } 3. Nc3 { [%eval 0.55] } 3... Qe6+ { [%eval 0.88] } 4. Be2 { [%eval 0.85] } 4... c5?! { [%eval 1.42] } 5. Nf3 { [%eval 1.4] } 5... h6?! { [%eval 1.94] } 6. O-O { [%eval 1.46] } 6... Bd7?! { [%eval 2.34] } 7. Re1?! { [%eval 1.41] } 7... Na6? { [%eval 3.1] } 8. Bxa6? { [%eval 1.12] } 8... Qg6?? { [%eval 7.37] } 9. Bd3? { [%eval 5.57] } 9... Bf5?? { [%eval 11.41] } 10. Bxf5?? { [%eval 6.17] } 10... Qxf5 { [%eval 6.29] } 11. d3? { [%eval 5.03] } 11... O-O-O? { [%eval 7.2] } 12. Ne4? { [%eval 5.09] } 12... b6? { [%eval 6.66] } 13. a4? { [%eval 5.58] } 13... e6?! { [%eval 6.22] } 14. a5 { [%eval 6.02] } 14... c4?? { [%eval 10.36] } 15. axb6 { [%eval 10.75] } 15... axb6 { [%eval 13.17] } 16. Ra6?? { [%eval 6.08] } 16... cxd3?? { [%eval 13.22] } 17. cxd3 { [%eval 12.27] } 17... Bb4 { [%eval 18.88] } 18. Qc2+ { [%eval 13.7] } 18... Bc5 { [%eval 14.9] } 19. Rxb6 { [%eval 16.48] } 19... Rd5 { [%eval 15.22] } 20. Rd6 { [%eval 12.45] } 20... Kc7 { [%eval 17.61] } 21. Rxd5 { [%eval 16.47] } 21... Qxd5 { [%eval 16.98] } 22. Qxc5+ { [%eval 9.96] } 22... Qxc5 { [%eval 9.74] } 23. Nxc5 { [%eval 9.88] } 23... Kd6 { [%eval 10.38] } 24. d4 { [%eval 9.63] } 24... Nf6 { [%eval 9.92] } 25. Ne5 { [%eval 9.98] } 25... Ra8 { [%eval 10.17] } 26. b4?! { [%eval 9.5] } 26... Nd5 { [%eval 9.38] } 27. Bd2?! { [%eval 8.56] } 27... Ra4? { [%eval 16.37] } 28. Nxf7+ { [%eval 16.26] } 28... Kc6 { [%eval 54.52] } 29. Ne5+ { [%eval 13.73] } 29... Kd6 { [%eval 19.25] } 30. Ne4+ { [%eval 10.43] } 30... Ke7 { [%eval 10.55] } 31. Nc6+ { [%eval 10.39] } 1-0

[Event "Rated Classical game"]
[Site "https://lichess.org/l4794f08"]
[White "Deuce"]
[Black "Yura81"]
[Result "0-1"]
[UTCDate "2013.07.07"]
[UTCTime "17:06:14"]
[WhiteElo "1231"]
[BlackElo "1601"]
[WhiteRatingDiff "-4"]
[BlackRatingDiff "+3"]
[ECO "C23"]
[Opening "Bishop's Opening: Khan Gambit"]
[TimeControl "480+0"]
[Termination "Normal"]

1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 27. Qxb7+ Rxb7 28. Rxb7+ Nc7 29. Rxc7+ Kxc7 30. Bxe7 Rxe7 31. Kb3 Qxd2 32. Kc4 Kd6 33. Kb4 Rb7+ 34. Ka5 Qxc3+ 35. Ka6 Qb4 36. h4 Be2# 0-1
 ```

## Output

analysis.txt (Better organization of data coming soon)
```
---RESULTS---
ECO: B00
 Number of Games: 1
Rating Pools
 Elo: >1500
 White wins: 1
 Black wins: 0
 Draws: 0
Out Of Book Moves:
 Move: d5
 moveNum: 1
 count: 1
Blunders:   
 Move Number: 8
 Count: 1


ECO: C23
 Number of Games: 1
Rating Pools
 Elo: >1000
 White wins: 0
 Black wins: 0
 Draws: 0
Out Of Book Moves:
 Move: d5
 moveNum: 2
 count: 1
Blunders: No Data

```











