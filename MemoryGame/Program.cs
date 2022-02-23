using System;

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
            dl = gm.ChooseDifficulty();
            
            gm.RandomizeXWords(dl);
            gm.ShowWords();
            gm.CreateMatrix(dl);
            gm.GameCourse();
        }
    }
}
