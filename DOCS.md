# Documentation

Detailed class usage and code [examples](#examples).

## Table Of Contents
* [PgnAnalyzer.Utils](#pgnanalyzerutils)
    * [Pgn](#public-class-pgn)
    * [Game](#public-class-game)
    * [Move](#public-class-move)
    * [Ply](#public-class-ply)
* [PgnAnalyzer.IO](#pgnanalyzerio)
    * [EcoReader](#public-class-ecoreader)

## PgnAnalyzer.Utils

Includes classes and helper methods for chess analysis.

### ``public class Pgn``

Stores pgn tags as key/value pairs that can be indexed like a dictionary. Keys are case-insensitive, See examples for usage.

#### Properties
| Property | Description |
| ----------- | ----------- |
| ``public Game game { get; set; }`` | Sequence of moves that make up the game. |

#### Methods

| Method | Description |
| ----------- | ----------- |
| ``public void Add(string key, object value)`` | Adds the specifed tag to the pgn. |
| ``public bool Remove(string key)`` | Returns true if the tag is successfully found and removed; otherwise, false. This method returns false if key is not found in the pgn. |
| ``public bool ContaingsTag(string key)`` | True if the pgn contains tag with the specifed key; otherwise, false.|
| ``public bool TryGetValue(string tag, out object? value)`` | Returns true if the pgn contains an tag with the specified key; otherwise, false. |
| ``public override string ToString()`` | Returns the pgn as a properly formatted string. |
| ``public override string ToString(ChessPrintOptions options)`` | Returns the pgn as a properly formatted string with the specified printing options. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``Pgn()`` | Initializes a new instance of the Pgn class. |

### public class Game

Moves can be indexed by move number like a dictionary.

#### Properties
| Property | Description |
| ----------- | ----------- |
| ``public IList<Move> readonlyMoves { get; set; }`` | Readonly list of moves that make up the game, sorted by move number. |
| ``public string? result { get; set; }`` | Result of the game as a string, i.e "1-0", "0-1". It is recommended to keep these as [SAN result values](https://en.wikipedia.org/wiki/Algebraic_notation_(chess)#End_of_game), however they can be any string value. |

#### Methods
| Method | Description |
| ----------- | ----------- |
| ``public static Game Parse(string moveString)`` | Parses a game string and returns a new instance of Game. Throws an exception if the game cannot be parsed. |
|``public void AddMove(Move move)``| Adds a move to the game. If the new move contains a move number, it will be placed in the correct place. Otherwise, it will be appended to the end of the list. |
|``public void AddMoves(IList<Move> moves)``| Adds a list of moves to the game in a similar fashion to AddMove(Move). |
|`` public void ClearMoves()``| Clears all moves in the list.|
|`` public void RemoveMove(int moveNum)``| Removes all moves with the specified move number.|
| ``public override string ToString()`` | Returns the game as a properly formatted string. |
| ``public override string ToString(ChessPrintOptions options)`` | Returns the game as a properly formatted string with the specified print options. |
| ``public bool Equals(Game? obj)`` | Checks for value equality against another instance of Game. |
| ``public bool HasAnalysis()`` | Checks whether the game contains any analysis information. |
| ``public bool HasAnnotations()`` | Checks whether the game contains any annotations. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``Game()`` | Initializes a new instance of the Game class. |
| ``Game(List<Move>? moves, string? result)`` | Initializes a new instance of the Game class with specified move sequence and result. |
| ``Game(string moveString)`` | Initializes a new instance of the Game class by parsing a given game string.|

### public class Move

Represents a complete move in a chess game.

#### Properties
| Property | Description |
| ----------- | ----------- |
| ``public Ply? whitePly { get; set; }`` | Ply containing information about white's turn. |
| ``public Ply? blackPly { get; set; }`` | Ply containing information about blacks's turn. |
| ``public int? moveNum { get; set; }`` | Indicates the move number of the game. |

#### Methods
| Method | Description |
| ----------- | ----------- |
| ``public static Move Parse(string moveString)`` | Parses a move string and returns a new instance of Move. Throws an exception if the game cannot be parsed. |
| ``public override string ToString()`` | Returns the move as a properly formatted string. |
| ``public override bool Equals(object? obj)`` | Checks for value equality against another object. |
| ``public bool Equals(Game? obj)`` | Checks for value equality against another instance of Move. |
| ``public override int GetHashCode()`` | Returns a hashcode for the instance of Move. |
| ``public static string ListToString(IEnumerable<Move> moves)`` | Returns a properly formatted string for a sequence of move instances. |
| ``public bool HasAnalysis()`` | Checks whether the move contains any analysis information. |
| ``public bool HasAnnotations()`` | Checks whether the move contains any annotations. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``Move()`` | Initializes a new instance of the Move class. |
| ``Move(Ply? whitePly, Ply? blackPly, int? moveNum)`` | Initializes a new instance of the Game class with specified Plys and move number. |
| ``Move(string moveString)`` | Initializes a new instance of the Move class by parsing a given move string.|

### public class Ply

Represents a ply (half-move) in a chess game.

#### Properties
| Property | Description |
| ----------- | ----------- |
| ``public string? san { get; set; }`` | SAN for this Ply. Not restricted to a specific format. |
| ``public string? annotation { get; set; }`` | Annotation for this ply (i.e {This move creates a backwards pawn}). Curly braces are not part of stored string, and are added upon convering to a string.|
| ``public string? analysis { get; set; }`` | Represents analysis of a game (i.e the ?? after Nc6??). |

#### Methods
| Method | Description |
| ----------- | ----------- |
| ``public static Ply Parse(string moveString)`` | Parses a ply string and returns a new instance of Ply. Throws an exception if the game cannot be parsed. |
| ``public override string ToString()`` | Returns the ply as a properly formatted string. |
| ``public override bool Equals(object? obj)`` | Checks for value equality against another object. |
| ``public bool Equals(Ply? obj)`` | Checks for value equality against another instance of Ply. |
| ``public override int GetHashCode()`` | Returns a hashcode for the instance of Ply. |
| ``public static List<Ply?> ToPlyList(IEnumerable<Move> moves)`` | Returns a list of ply by deconstructing a list of moves, filtering out null plys. |
| ``public static List<Ply?> ToPlyList(Game game)`` | Returns a list of ply by deconstructing a game, filtering out null plys. |
| ``public static List<Ply?> ToPlyListIncludingNulls(IEnumerable<Move> moves)`` | Returns a list of ply by deconstructing a list of moves, including null plys. |
| ``public static List<Ply?> ToPlyListIncludingNulls(Game game)`` | Returns a list of ply by deconstructing a game, including null plys. |
| ``public bool HasAnalysis()`` | Checks whether the ply contains any analysis information. |
| ``public bool HasAnnotations()`` | Checks whether the ply contains any annotations. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``Ply()`` | Initializes a new instance of the Ply class. |
| ``Ply(string? san, string? analysis, string? annotation)`` | Initializes a new instance of the Game class with specified move sequence and result |
| ``public Ply(string plyString)`` | Initializes a new instance of the Ply class by parsing a given ply string.|

### public class Eco

Represents an [Eco](#https://en.wikipedia.org/wiki/Encyclopaedia_of_Chess_Openings) opening.

| Property | Description |
| ----------- | ----------- |
| ``public string? code { get; set; }`` | Eco code for this opening. Not restricted to a specific format. |
| ``public string? name { get; set; }`` | Name for this opening. |
| ``public List<Move>? analysis { get; set; }`` | List of moves for this opening. |

#### Methods
| Method | Description |
| ----------- | ----------- |
| ``public override string ToString()`` | Returns the eco as a properly formatted string. |
| ``public override bool Equals(object? obj)`` | Checks for value equality against another object. |
| ``public bool Equals(Ply? obj)`` | Checks for value equality against another instance of Eco. |
| ``public override int GetHashCode()`` | Returns a hashcode for the instance of Eco. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``Eco()`` | Initializes a new instance of the Eco class. |
| ``Eco(string? code, string? name, List<Move>? moves)`` | Initializes a new instance of the Eco class with specified eco code, name, and moves. |

## PgnAnalyzer.IO

### public class EcoReader

Turns a tsv file of eco's into an object which can be indexed by code. Indexing is case-insensitive.

#### Properties
\[None]

#### Methods
| Method | Description |
| ----------- | ----------- |
| ``public Eco GetEcoFromCode(string code)`` | Returns an instance of Eco for the corresponding eco code. |
| ``public Eco getEcoFromMoves(IEnumerable<Move>? moves)`` | Returns an instance of Eco for the corresponding eco code. Returns the Eco object for unknown opening if the list is null. |

#### Constructors
| Overloads | Description |
| ----------- | ----------- |
| ``EcoReader(string filepath)`` | Initializes a new instance of the EcoReader class with a path to the eco file to recognize. Eco file must be a tsv file. |


## Examples

### If the game has analysis, find the first blunder and move number. 

```C#

public void addGame(Pgn pgn){

    //...

    Game? game = pgn.game;

    int? blunderMoveNum = null;

    string? blunderSan = null;

    if(game != null && game.HasAnalysis())
    {
        foreach(Move move in game.moves)
        {
            blunderMoveNum = move.moveNum;
            if(move.blackPly != null && move.blackPly.analysis == "??")
            {
                blunderSan = move.blackPly.san;
                break;
            }
            if(move.whitePly != null && move.whitePly.analysis == "??")
            {
                blunderSan = move.whitePly.san;
                break;
            }
        }
    }

    // ...
}

```
### Get the event and ratings of each player.

```C#
public void addGame(Pgn pgn){

    //...

    string chessEvent = "Unknown";
    int whiteRating = -1;
    int blackRating = -1;

    //These are common tags, so they will be stored as their logical datatypes when scanned from a pgn file.
    if(pgn.ContainsTag("Event"))
    {
        chessEvent = (string)pgn["Event"];
    }
    if(pgn.ContainsTag("WhiteElo"))
    {   
        whiteRating = (int)pgn["WhiteElo"]
    }
    if(pgn.ContainsTag("BlackElo"))
    {   
        blackRating = (int)pgn["BlackElo"];
    }

    // ...
}

```
