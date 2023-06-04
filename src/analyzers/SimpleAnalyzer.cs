using PgnAnalyzer.Utils;

namespace PgnAnalyzer.Analyzer; //<---Any analysis you write must be in the Analyzer namespace.

/*  
    Example of a simple analyzer class which gets the average rating of all games in a pgn.
    Analyzers have no class name requirements.
*/

public class SimpleAnalyzer : IAnalyzer //<--Your analysis class must implement the IAnalyzer interface.
{
    //Keep any data types / variables necessary for your analysis.
    private int totalRatingPoints;
    private int numGames;
    private double result;

    /*
        Logic to perform for each new game. 
        Think of this block of code as the body 
        of a for loop that iterates over all games in the pgn file.

        The PGN object stores all tags as key value pairs, which can be accessed like a dictionary.
        The game itself can be accessed as follows:

            Game? game = pgn.game (Note that the game can be null if there is no game associated with a set of tags)

            //Get the moves in a game
            List<Move> moves = game.moves;

            //Get the result of a game
            string? result = game.result ("1-0, 0-1, 1/2-1/2, null")

            Refer to the GitHub page for more examples.
    */
    
    public void addGame(Pgn pgn)
    {  
        if(!pgn.ContainsKey("WhiteElo") || !pgn.ContainsKey("BlackElo"))
        {
            return;
        }

        int whiteElo = 0;
        int blackElo = 0;
 
        //The PGN reader will automatically read any tags as strings, however the PGN class can hold any datatype. Therefore, we must cast into strings before continuing.
        int.TryParse((string)pgn["WhiteElo"], out whiteElo);
        int.TryParse((string)pgn["BlackElo"], out blackElo);

        totalRatingPoints += whiteElo + blackElo;
        numGames++;
    }

    /*
        Performed when all games have been iterated through and analyzed.
        Return any object which represents your complete analysis.
        The only requirement for the object is that it must have a parameterless constructor.
        If your analysis requires it to have a constructor with parameters, add a
        private parameterless constructor to it.
    */
    public object getResults()
    {
        result = totalRatingPoints/(numGames * 2);
        return result;
    }
}