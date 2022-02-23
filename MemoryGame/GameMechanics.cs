using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class GameMechanics
    {

        private string[] randomizedWords { get; set; }
        private string[] words { get; set; }
        private string[] bWords { get; set; }
        private string[] matrixNumbers { get; set; }
        public void SaveWords(string filePath)
        {
            this.words = System.IO.File.ReadAllLines(@filePath);
        }

        public void ShowWords()
        {
            foreach(string word in this.randomizedWords)
            {
                Console.WriteLine(word);
            }
        }

        public void RandomizeXWords(DifficultyLevel difficulty)
        {
            int randNum;
            this.randomizedWords = new string[difficulty.numberOfWords];
            Random rand = new Random();

            for (int i = 0; i < difficulty.numberOfWords; i++)
            {
                randNum = rand.Next(0, this.words.Length);
                
                //Checks if word has been already chosen
                if (Array.Exists(this.randomizedWords, element => element == this.words[randNum]) == false)
                {
                    this.randomizedWords[i] = this.words[randNum];
                }
                else
                {
                    i--;
                }
            }
        }

        public DifficultyLevel ChooseDifficulty()
        {
            DifficultyLevel difficulty = new DifficultyLevel();
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");
            int answer = 1;
            do
            {
                Console.Write("Your choice: ");
                if (answer != 1 && answer != 2) { Console.Write("(choose correct number) "); }
                try
                {
                    answer = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    answer = 0;
                }
            } while (answer != 1 && answer != 2);
            Console.WriteLine();

            switch (answer)
            {
                case 1:
                    difficulty.numberOfWords = 4;
                    difficulty.chances = 10;
                    difficulty.level = "Easy";
                    
                    break;
                case 2:
                    difficulty.numberOfWords = 8;
                    difficulty.chances = 15;
                    difficulty.level = "Hard";
                    break;

            }
            difficulty.currentChances = difficulty.chances;
            return difficulty;
        }

        public string[] RandomizeRandomizedWords(string[] randWords)
        {
            Random rand = new Random();

            for(int i = 0; i < this.randomizedWords.Length; i++)
            {
                int randNum = rand.Next(0, this.randomizedWords.Length);
                if (Array.Exists(randWords, element => element == this.randomizedWords[randNum]) == false)
                {
                    randWords[i] = randomizedWords[randNum];
                }
                else
                {
                    i--;
                }
                
             }

            return randWords;
        }

        public void CreateMatrix(DifficultyLevel difficulty)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Level: {0}", difficulty.level);
            Console.WriteLine("Guess chances: {0}", difficulty.currentChances);
            Console.WriteLine();

            this.bWords = new string[randomizedWords.Length];
            this.bWords = RandomizeRandomizedWords(this.bWords);

            this.matrixNumbers = new string[difficulty.numberOfWords];
            for(int i = 1; i <= difficulty.numberOfWords; i++)
            {
                this.matrixNumbers[i - 1] = Convert.ToString(i);
            }

            
        }


        public void GameCourse()
        {
            Console.Write(" ");
            for (int i = 0; i < this.matrixNumbers.Length; i++)
            {
                Console.Write(" " + this.matrixNumbers[i]);
            }

            Console.WriteLine();
            Console.Write("A");

            for (int i = 0; i < this.randomizedWords.Length; i++)
            {
                Console.Write(" " + "X");
            }

            Console.WriteLine();
            Console.Write("B");

            for (int i = 0; i < this.bWords.Length; i++)
            {
                Console.Write(" " + "X");
            }
        }

        /*
        for(int i = 0; i < this.matrixNumbers.Length; i++)
            {
                Console.Write(" " + this.matrixNumbers[i]);
            }

            Console.WriteLine();
            Console.Write("A");

            for(int i = 0; i < this.randomizedWords.Length; i++)
            {
                Console.Write(" " + this.randomizedWords[i]);
            }

            Console.WriteLine();
            Console.Write("B");

            for (int i = 0; i < this.bWords.Length; i++)
            {
                Console.Write(" " + this.bWords[i]);
            } 
        */



    }
}
