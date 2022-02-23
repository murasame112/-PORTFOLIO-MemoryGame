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
        private string[] xMatrixA { get; set; }
        private string[] xMatrixB { get; set; }
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
            this.xMatrixA = new string[randomizedWords.Length];
            this.xMatrixB = new string[randomizedWords.Length];
            this.bWords = RandomizeRandomizedWords(this.bWords);
            
            for(int i = 0; i < this.xMatrixA.Length; i++)
            {
                this.xMatrixA[i] = "X";
            }

            for (int i = 0; i < this.xMatrixB.Length; i++)
            {
                this.xMatrixB[i] = "X";
            }

            this.matrixNumbers = new string[difficulty.numberOfWords];
            for(int i = 1; i <= difficulty.numberOfWords; i++)
            {
                this.matrixNumbers[i - 1] = Convert.ToString(i);
            }

            
        }

        public void ShowInitialMatrix()
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
                Console.Write(" " + xMatrixA[i]);
            }

            Console.WriteLine();
            Console.Write("B");

            for (int i = 0; i < this.bWords.Length; i++)
            {
                Console.Write(" " + xMatrixB[i]);
            }
            Console.WriteLine();
        }

        public string GetFirstPlayerCoords()
        {
            string answer = "";
            string answerFirst = "";
            string answerSecond = "";

            do
            {
                Console.Write("Enter your answer: ");
                answer = Console.ReadLine();
                answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                answerSecond = answer.Substring(1, 1);

            } while ((answerFirst != "A" && answerFirst != "B") || ((Array.Exists(this.matrixNumbers, element => element == answerSecond)) == false) || answer.Length > 2);
            
            return answer;

        }

        public string GetSecondPlayerCoords(string prev)
        {
            string answer = "";
            string answerFirst = "";
            string answerSecond = "";

            do
            {
                Console.Write("Enter your answer: ");
                answer = Console.ReadLine();
                answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                answerSecond = answer.Substring(1, 1);

            } while ((answerFirst != "A" && answerFirst != "B") || ((Array.Exists(this.matrixNumbers, element => element == answerSecond)) == false) || (answer.Length > 2) || (answerFirst == prev));

            return answer;
        }

        public void GameCourse()
        {
            bool gameFinished = false;
            while (gameFinished == false) {
                ShowInitialMatrix();

                string answer = GetFirstPlayerCoords();
                string answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                string answerSecond = answer.Substring(1, 1);

                int answerSecondInt = Convert.ToInt32(answerSecond);
                answerSecondInt -= 1;
                switch (answerFirst)
                {
                    case "A":
                        this.xMatrixA[answerSecondInt] = this.randomizedWords[answerSecondInt];
                        break;
                    case "B":
                        this.xMatrixB[answerSecondInt] = this.bWords[answerSecondInt];
                        break;
                }
                ShowInitialMatrix();
                answer = GetSecondPlayerCoords(answerFirst);
                answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                answerSecond = answer.Substring(1, 1);

                answerSecondInt = Convert.ToInt32(answerSecond);
                answerSecondInt -= 1;
                switch (answerFirst)
                {
                    case "A":
                        this.xMatrixA[answerSecondInt] = this.randomizedWords[answerSecondInt];
                        break;
                    case "B":
                        this.xMatrixB[answerSecondInt] = this.bWords[answerSecondInt];
                        break;
                }


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
