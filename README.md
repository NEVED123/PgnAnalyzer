# PgnAnalyzer
PgnAnalyzer parses your [PGN](https://en.wikipedia.org/wiki/Portable_Game_Notation) file into a set of utility classes and provides environment to easily gather data about a large set of games. This data can then be easily exported to a file type of your choice.

NOTE - This is not a chess engine, nor is there built in functionality for checking the validity of moves, etc. It is optimized for data collection from existing chess PGNs.

## Table Of Contents
* [Installation](#installation)
  * [Prerequsites](#prerequisites)
* [Usage](#usage)
  * [Creating your own analyzer](#creating-your-own-analyzer)
  * [Creating your own serializer](#optional-creating-your-own-serializer--configuring-existing-serializers)
  * [Executing Analysis](#executing-analysis)
  * [Example](#example)
  * [Additional Configurations](#additional-configurations)
  * [Testing](#testing)
* [Documentation](DOCS.md)

## Installation

Note: This has only been tested on Windows.

### Prerequisites
* [Git](https://git-scm.com/downloads)
* [.NET Framework](https://dotnet.microsoft.com/en-us/download) (Comes with Windows)
* Any text editor will work, however this framework works best with [VSCode](https://code.visualstudio.com/download).

1. Open the command line and change the directory to where you want to install.
2. Run ``git clone https://github.com/NEVED123/PgnAnalyzer.git`` followed by ``cd PgnAnalyzer``
3. Open this directory in your text editor of choice. (If you are using VSCode, you may get a popup asking if you want to install missing dependencies - Do this)

## Usage

### Creating your own analyzer

To perform your own, custom analysis, you must create an analyzer class. To do this, go to the ``analyzers`` folder and create a new C# file. It can be called anything, so long as it has file extension ``.cs``. Use this template to start your analyzer:

#### analyzers/TemplateAnalyzer.cs
```C#
namespace PgnAnalyzer.Analyzer; 

/*  
    Template for a custom analyzer class. See examples for more information.
*/

public class TemplateAnalyzer : IAnalyzer //<--Your analysis class must implement the IAnalyzer interface.
{
    public void AddGame(Pgn pgn)
    {   
        /*
            Logic to perform for each new game. 
            Think of this block of code as the body 
            of a for loop that iterates over all games in the pgn file.
        */
    }

    public object Export()
    {
        /*
            Performed when all games have been iterated through and analyzed.
            Return any object which represents your complete analysis.
            The only requirement for the object is that it must have a parameterless constructor.
            If your analysis requires it to have a public constructor with parameters, add a
            private parameterless constructor to it.
        */
        return new object(); //<--Can be an instance of any class, this is just a placeholder
    }
}

```
Implementation details, examples, and this template to make your own analyzer can be found in the ``analyzers`` folder.

### (Optional) Creating your own serializer / Configuring existing serializers

The serializer is what converts the exported object into a file. To make your own, create a file under ``serializers``. Create a class with the name FiletypeSerializerWrapper (Ex. xml -> XmlSerializerWrapper). Be sure the name of the class has no typos, and that the first letter in the filetype is capitalized. Use this template to get started:

#### serializers/TemplateSerializerWrapper.cs
```C#
namespace PgnAnalyzer.Serializer; //<-- The custom serializer must belong to the Serializer namespace.

/*
    Template for creating a custom serializer.
    The serializer must have the name [Fileformat]SerializerWrapper, with file type capitalized.
        ex. txt -> TxtSerializerWrapper
            xml -> XmlSerializerWrapper
*/

public class TemplateSerializerWrapper : ISerializerWrapper //<-- Serializer must implement the ISerializerWrapper interface.
{
    public void Serialize(string filename, object obj)
    {
        //Your complete serializer logic, from reflection to creating the file.
        //See the other serializers for more examples.
    }
}
```

Serializers for XML and JSON have been provided, along with this template to make your own. 

### Executing Analysis

Once you have created an analysis class, you are ready to analyze! The command to activate the framework is ``analyzer``. If you are using a Unix based system, you will need to use ``bash analyzer``, or do additional configuring.

#### Synopsis
```
analyzer <analyzer_class file_format path_to_pgn [options]> | <[-h|--help]> | <[-b|--boop]>
```
#### Arguments
| Argument     | Description |
| ----------- | ----------- |
| ``analyzer_class`` | Name of the class to use for analysis. Class must be in the PgnAnalyzer.Analyzer namespace. Argument does not need to include the .cs file extension. |
| ``file_format`` | Format of the file to export to. If using a custom serializer, be sure the class is in the PgnAnalyzer.Serializer namespace, and that the class name of the correct format.|
| ``path_to_pgn`` | Path and name of pgn file to analyze.|
| ``-b,--boop`` | Make a booping sound and abort. No real purpose, but kinda fun sometimes.|
| ``-h,--help`` | Show command line help.|

#### Options
| Option | Description |
| ----------- | ----------- |
| ``-x, --export <FILE_PATH>`` | Location and name of the exported file.|
| ``-c, --count <INTEGER>`` | Indicate analysis progress after this many games. |

### Example

The following properly formatted PGN file containing 12 games has been provided:

#### samplegames.pgn
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

1. e4 e5 2. Bc4 d5 3. Bb5+ c6 4. Bd3 d4 5. Nf3 f6 6. Bc4 Bg4 7. h3 Bh5 8. g4 Bg6 9. d3 Bb4+ 10. c3 dxc3 11. bxc3 Bc5 12. Qa4 Qb6 13. Nbd2 Bxf2+ 14. Kd1 Nd7 15. Be6 Nc5 16. Qc4 Ke7 17. g5 Nxe6 18. gxf6+ gxf6 19. Ba3+ Kd7 20. Rf1 Ne7 21. Rxf2 Qxf2 22. Rb1 Rab8 23. d4 Bh5 24. dxe5 Bxf3+ 25. Kc2 fxe5 26. Qb4 Rhe8 1-0

(10 more games)
```
You can get PGN's from your own games through lichess and chess.com. If you want to use large PGN samples, refer to the lichess [game database](https://database.lichess.org/).

To analyze this data and have the results exported to your desktop, run:

Windows
```
analyzer ComplexAnalyzer json samplegames.pgn --export path\to\your\desktop\filename
```
Unix
```
bash analyzer ComplexAnalyzer json samplegames.pgn --export path/to/your/desktop/filename
```

#### path/to/your/desktop/filename.json
The ``\u0027s`` is caused by the apostrophe in the name of the opening, "Bishop's Opening"
```json
[
  {
    "ecoCode": "A02",
    "ecoName": "Bird\u0027s Opening",
    "ecoMoves": "1. f4",
    "numGames": 1,
    "ratingDataList": [
      {
        "outOfBookDataList": [
          {
            "san": "b6",
            "count": 1
          }
        ],
        "eloMin": 1500,
        "whiteWinNum": 1,
        "blackWinNum": 0,
        "drawNum": 0,
        "noResultDataNum": 0
      }
    ]
  },
  {
    "ecoCode": "B01",
    "ecoName": "Scandinavian",
    "ecoMoves": "1. e4 d5",
    "numGames": 1,
    "ratingDataList": [
      {
        "outOfBookDataList": [
          {
            "san": "exd5",
            "count": 1
          }
        ],
        "eloMin": 1500,
        "whiteWinNum": 1,
        "blackWinNum": 0,
        "drawNum": 0,
        "noResultDataNum": 0
      }
    ]
  },
  ...201 more lines
]
```

### Additional Configurations

#### Custom ECO file
An extensive eco file has been [provided](https://lichess.org/forum/general-chess-discussion/eco-code-csv-sheet) (``eco.tsv``), which is used to resolve ECO information. However, if you wish to use your own eco file, you must ensure that it is of the same format as the example provided. 

#### Runtime configurations
The analyzer CLI is a mask on top of the dotnet command line. If you want [finer control over how the project is built and run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run), you can run:
```
dotnet run --project src/pgnanalyzer.csproj [<dotnet_args>] -- [<analyzer_args>]
```

### Testing
If you wish to test any part of your code, place your test files under ``test/my_tests``. The template below has been provided:

#### test/my_tests/Template_Test.cs
```C#
namespace PgnAnalyzer.MyTest; //<---Any test you write must be in the MyTest namespace to be run via "analyzer test"

//This framework uses NUnit for testing. See https://github.com/nunit/nunit-csharp-samples

public class Template_Test
{
    [SetUp]
    public void SetUp()
    {
        //Code to be run before each test
    }

    [Test]
    public void Test()
    {
        //Test code with Assert statements. 
        //For more on NUnit and Assertions, visit https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html
        Assert.Pass();
    }
}
```

To run your tests, run:
Windows
```
analyzer test
```
Unix
```
bash analyzer test
```

#### Testing configurations
If you want [finer control over how tests are performed](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test), run:
```
dotnet test [<dotnet_args>]
```
Note that there are other tests specific to the framework, so you may wish to filter these out if taking this route.

## Documentation

Click [here](DOCS.md) for detailed documentation and examples.



