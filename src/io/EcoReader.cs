public class EcoReader
{
    public EcoReader(string filePath)
    {
        sr = new StreamReader(filePath);
    }

    private StreamReader sr;

    public string getStringFromEco(string eco)
    {
        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string ecoFromFile = splitLine[0];

            if(ecoFromFile == eco)
            {
                sr.Close();
                return splitLine[2];
            }

            line = sr.ReadLine();
        }

        throw new InvalidOperationException();
    }

    public string getEcoFromString(string moveText)
    {
        string moveTextNoPeriod = moveText.Replace(".", String.Empty);

        sr.ReadLine(); //burn header

        string? line = sr.ReadLine();

        //Eco for unknown opening
        string bestFitEco = "A00";

        while(line != null)
        {
            string[] splitLine = line.Split('\t');

            string eco = splitLine[0];
            string ecoMoveText = splitLine[2];

            if(moveTextNoPeriod.Contains(ecoMoveText))
            {
                if(ecoMoveText.Length > bestFitEco.Length)
                {
                    bestFitEco = eco;
                }
            }

            line = sr.ReadLine();
        }

        sr.Close();

        return bestFitEco;
    }


}