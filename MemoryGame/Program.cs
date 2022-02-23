using System;

namespace MemoryGame
{
    class Program
    {

        static void Main(string[] args)
        {
            GameMechanics gm = new GameMechanics();

            string filePath = "../../../Words.txt";

            gm.SaveWords(filePath);
            //gm.ChooseDifficulty();
            gm.numberOfWords = 4;
            gm.RandomizeXWords(gm.numberOfWords);
            gm.ShowWords();

        }
    }
}
