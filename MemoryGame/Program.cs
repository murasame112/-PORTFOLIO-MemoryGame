using System;
using System.Diagnostics;

namespace MemoryGame
{
    class Program
    {

        static void Main(string[] args)
        {
            GameMechanics gm = new GameMechanics();
            DifficultyLevel dl = new DifficultyLevel();

            

            string filePath = "../../../Words.txt";
            
            gm.SaveWords(filePath);
            Stopwatch sw;
            bool gameRestart = true;
            while (gameRestart == true)
            {
                
                sw = Stopwatch.StartNew();
                dl = gm.ChooseDifficulty();
                gm.RandomizeXWords(dl);
                gm.ShowWords();
                gm.CreateMatrix(dl);
                gm.GameCourse();
                sw.Stop();
                double timeD = sw.Elapsed.TotalSeconds;
                int time = Convert.ToInt32(timeD);                
                if (gm.gameWon == true) {
                    gm.ResultReport(dl, time);
                    gm.SaveScore(dl, time); 
                }
                gameRestart = gm.AskForRestart();              
            }

        }
    }
}
