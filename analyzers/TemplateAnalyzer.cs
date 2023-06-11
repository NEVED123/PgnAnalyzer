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

            Values from your tags will be converted into strings except for the following:
                Round - int
                PlyCount - int
                WhiteRatingDiff - int
                BlackRatingDiff - int
                WhiteElo - int
                BlackElo - int 
                Date or UTCDate - DateTime with time 00:00:00
                Time or UTCTime - DateTime with day 01/01/0001
        */
    }

    public object Export()
    {
        /*
            Performed when all games have been iterated through and analyzed.
            Return any object which represents your complete analysis.
            The only requirement for the object is that it must have a parameterless constructor.
            If your analysis requires it to have a constructor with parameters, add a
            private parameterless constructor to it.
        */
        return new object(); //<--Can be an instance of any class, this is just a placeholder
    }
}