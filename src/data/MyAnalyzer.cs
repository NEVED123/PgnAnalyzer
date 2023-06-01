// using PgnAnalyzer.Utils;
// using PgnAnalyzer.Data;
// using PgnAnalyzer;

// public class MyAnalyzer : IAnalyzer
// {
//     private List<OpeningData> openings = new List<OpeningData>();

//     private EcoReader ecoReader = new EcoReader("eco.tsv");

//     int totalNumGames = 0;

//     public void addGame(Pgn pgn)
//     {
//             //opening info
//             Game game = pgn.game;

//             if(game == null)
//             {
//                 return;
//             }

//             //string eco = pgn.ContainsKey("eco") ? pgn["eco"] : ecoReader.getEcoFromString(game.ToString());

        
//             OpeningData currOpening;
//             int openingIndex = openings.FindIndex(x => x.eco == eco);

//             if(openingIndex >= 0)
//             {  
//                 currOpening = openings[openingIndex];
//             }
//             else{
//                 currOpening = new OpeningData(eco);
//                 openings.Add(currOpening);
//             }
   
//             currOpening.numGames++;

//             //rating data

//             int basket = getEloBasket(currPgn); 

//             List<RatingData> currRatingDataList = currOpening.ratingDataList;

//             int ratingIndex = currRatingDataList.FindIndex(x => x.eloMin == basket);
//             RatingData currRatingData;

//             if(ratingIndex >= 0)
//             {
//                 currRatingData = currRatingDataList[ratingIndex];
//             }
//             else{
//                 currRatingData = new RatingData(basket);
//                 currRatingDataList.Add(currRatingData);
//             } 

//             string result = currPgn.ContainsKey("result") ? currPgn["result"] : getResultFromString(moveText);

//             switch(result)
//             {
//                 case "1-0":
//                     currRatingData.whiteWinNum++;
//                     break;
//                 case "0-1":
//                     currRatingData.blackWinNum++;
//                     break;
//                 case "1/2-1/2":
//                     currRatingData.drawNum++;
//                     break;
//             }

//             //out of book data

//             List<OutOfBookData> currOutOfBookDataList = currRatingData.outOfBookDataList;
            
//             string EcoMoveText = getStringFromEco(eco);
//             string afterBookMoveText = symmetricMoveDifference(moveText, EcoMoveText);
//             string firstAfterBookMoveSan = getFirstMoveSan(afterBookMoveText);
//             int firstAfterBookMoveNum = getFirstMoveNum(afterBookMoveText);

//             OutOfBookData currOutOfBookData;
//             int outOfBookDataIndex = currOutOfBookDataList.FindIndex(x => (x.san == firstAfterBookMoveSan && x.moveNum == firstAfterBookMoveNum));

//             if(outOfBookDataIndex >= 0)
//             {   
//                 currOutOfBookData = currOutOfBookDataList[outOfBookDataIndex];
//             }
//             else{
//                 currOutOfBookData = new OutOfBookData(firstAfterBookMoveSan, firstAfterBookMoveNum);
//                 currOutOfBookDataList.Add(currOutOfBookData);
//             }

//             currOutOfBookData.count++;  

//             //Blunder spots

//             List<BlunderSpotData> currBlunderSpotDataList = currRatingData.blunderSpotDataList;

//             if(hasAnalysis(moveText))
//             {
//                 //TODO: FIGURE OUT WHETHER IT WAS A BLACK OR WHITE BLUNDER
//                 string blunder = getFirstBlunder(moveText);

//                 int blunderMoveNum = getFirstBlunderNum(moveText);

//                 int blunderSpotIndex = currBlunderSpotDataList.FindIndex(x => x.moveNum == blunderMoveNum);
//                 BlunderSpotData currBlunderSpotData;

//                 if(blunderSpotIndex >= 0)
//                 {
//                     currBlunderSpotData = currBlunderSpotDataList[blunderSpotIndex];
//                 }
//                 else{
//                     currBlunderSpotData = new BlunderSpotData(blunderMoveNum);
//                     currBlunderSpotDataList.Add(currBlunderSpotData);
//                 } 

//                 currBlunderSpotData.count++;
//             }

//             totalNumGames++;

//             if(totalNumGames % 10000 == 0)
//             {
//                 Console.WriteLine($"Total games analyzed: {totalNumGames}");
//             }
//     }

//     public object getResults()
//     {
//         return openings;
//     }
// }