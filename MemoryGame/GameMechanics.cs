using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemoryGame
{
    public class GameMechanics
    {

        private string[] randomizedWords { get; set; }
        private string[] words { get; set; }
        private string[,] matrixWords { get; set; }
        private string[] matrixNumbers { get; set; }
        private string[,] xMatrix { get; set; }
        private string[] highscores { get; set; }
        private string[] scores { get; set; }
        private int currentChances { get; set; }
        public bool gameWon { get; set; }
       
        
        
        public void SaveWords(string filePath)
        {
            this.words = File.ReadAllLines(@filePath);
        }

        public void ShowWords()
        {
            foreach(string word in this.randomizedWords)
            {
                Console.WriteLine(word);
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
            this.currentChances = difficulty.chances;
            return difficulty;
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

        public string[,] RandomizeRandomizedWords(string[,] randWords)
        {
            Random rand = new Random();
            List<string> rWordsToEmpty = new List<string>();

            foreach(string word in this.randomizedWords)
            {
                rWordsToEmpty.Add(word);
            }

            for (int i = 0; i < this.randomizedWords.Length; i++)
            {
                randWords[0, i] = this.randomizedWords[i];
            }

            for (int i = 0; i < this.randomizedWords.Length; i++)
            {
                int randNum = rand.Next(0, this.randomizedWords.Length);
                while (rWordsToEmpty.Contains(this.randomizedWords[randNum]) == false)
                {
                    randNum = rand.Next(0, this.randomizedWords.Length);
                               
                }
                randWords[1, i] = this.randomizedWords[randNum];
                rWordsToEmpty.Remove(this.randomizedWords[randNum]);
                
            }

            return randWords;
        }

        public void CreateMatrix(DifficultyLevel difficulty)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Level: {0}", difficulty.level);
            Console.WriteLine("Guess chances: {0}", this.currentChances);
            Console.WriteLine();

            this.matrixWords = new string[2, this.randomizedWords.Length];
            this.xMatrix = new string[2,this.randomizedWords.Length];
            
            this.matrixWords = RandomizeRandomizedWords(this.matrixWords);
            
            
            for(int i = 0; i < this.xMatrix.GetLength(1); i++)
            {
                this.xMatrix[0,i] = "X";
                this.xMatrix[1, i] = "X";
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
                Console.Write(" " + this.xMatrix[0,i]);
            }

            Console.WriteLine();
            Console.Write("B");

            for (int i = 0; i < this.randomizedWords.Length; i++)
            {
                Console.Write(" " + this.xMatrix[1,i]);
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
                if (answer.Length == 2)
                {
                    answerFirst = answer.Substring(0, 1);
                    answerFirst = answerFirst.ToUpper();
                    answerSecond = answer.Substring(1, 1);
                }

            } while ((answerFirst != "A" && answerFirst != "B") || ((Array.Exists(this.matrixNumbers, element => element == answerSecond)) == false) || (answer.Length != 2));
            
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
                if (answer.Length == 2)
                {
                    answerFirst = answer.Substring(0, 1);
                    answerFirst = answerFirst.ToUpper();
                    answerSecond = answer.Substring(1, 1);
                }

            } while ((answerFirst != "A" && answerFirst != "B") || ((Array.Exists(this.matrixNumbers, element => element == answerSecond)) == false) || (answer.Length > 2) || (answerFirst == prev));

            return answer;
        }

        
       

        

        public void GameCourse()
        {
            int pointsNeeded = this.randomizedWords.Length;
            int points = 0;
            string firstWord = "";
            string secondWord = "";
            int wordLengthForNumMatrix = 0;
            bool gameFinished = false;
            while (gameFinished == false)
            {
                ShowInitialMatrix();
                Console.WriteLine("Current chances: {0}", this.currentChances);

                
                string answer = GetFirstPlayerCoords();
                string answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                string answerSecond = answer.Substring(1, 1);
                int wordNumberA = 0;
                int wordLetterA = 0;
                int wordNumberB = 0;
                int wordLetterB = 0;
                bool wordAIsX = false;
                bool wordBIsX = false;
                int answerSecondInt = Convert.ToInt32(answerSecond);
                answerSecondInt -= 1;
                switch (answerFirst)
                {
                    case "A":
                        if (this.xMatrix[0, answerSecondInt] == "X") { wordAIsX = true; }
                        this.xMatrix[0,answerSecondInt] = this.matrixWords[0, answerSecondInt];
                        firstWord = this.xMatrix[0, answerSecondInt];
                        wordNumberA = answerSecondInt;
                        wordLetterA = 0;
                        wordLengthForNumMatrix = this.matrixWords[0, answerSecondInt].Length;
                        break;
                    case "B":
                        if (this.xMatrix[1, answerSecondInt] == "X") { wordBIsX = true; }
                        this.xMatrix[1,answerSecondInt] = this.matrixWords[1, answerSecondInt];
                        firstWord = this.xMatrix[1, answerSecondInt];
                        wordNumberA = answerSecondInt;
                        wordLetterA = 1;
                        wordLengthForNumMatrix = this.matrixWords[1, answerSecondInt].Length;
                        break;
                }
                wordLengthForNumMatrix -= 1;
                for (int i = 0; i < wordLengthForNumMatrix; i++)
                {
                    this.matrixNumbers[answerSecondInt] += " ";
                }

                Console.WriteLine();
                Console.Clear();
                ShowInitialMatrix();
                this.matrixNumbers[answerSecondInt] = this.matrixNumbers[answerSecondInt].Substring(0, 1);
                answer = GetSecondPlayerCoords(answerFirst);
                answerFirst = answer.Substring(0, 1);
                answerFirst = answerFirst.ToUpper();
                answerSecond = answer.Substring(1, 1);
                
                answerSecondInt = Convert.ToInt32(answerSecond);
                answerSecondInt -= 1;
                
                switch (answerFirst)
                {
                    case "A":
                        if(this.xMatrix[0, answerSecondInt] == "X") { wordAIsX = true; }
                        this.xMatrix[0, answerSecondInt] = this.matrixWords[0, answerSecondInt];
                        secondWord = this.xMatrix[0, answerSecondInt];
                        wordNumberB = answerSecondInt;
                        wordLetterB = 0;
                        break;
                    case "B":
                        if (this.xMatrix[1, answerSecondInt] == "X") { wordBIsX = true; }
                        this.xMatrix[1, answerSecondInt] = this.matrixWords[1, answerSecondInt];
                        secondWord = this.xMatrix[1, answerSecondInt];
                        wordNumberB = answerSecondInt;
                        wordLetterB = 1;
                        break;
                }
                Console.Clear();
                Console.WriteLine("first and second word: " + firstWord + " " + secondWord);
                if(firstWord != secondWord)
                {
                    if (wordAIsX == true)
                    {
                        this.xMatrix[wordLetterA, wordNumberA] = "X";
                    }
                    if (wordBIsX == true)
                    {
                        this.xMatrix[wordLetterB, wordNumberB] = "X";
                    }

                }
                else if(wordAIsX == true && wordBIsX == true)
                {
                    points += 1;
                }
                this.currentChances -= 1;
                Console.WriteLine();
                Console.WriteLine("==================");
                Console.WriteLine();
                gameFinished = CheckForFinish(points);

                
                
            }

            

        }

        public bool CheckForFinish(int points)
        {

            bool result = false; ;
            if (points >= this.randomizedWords.Length)
            {
                result = true;
                WinGame();
            }
            else if (this.currentChances <= 0)
            {
                result = true;
                LoseGame();
            }
            return result;
        }

        public bool AskForRestart()
        {
            bool result = false;
            Console.WriteLine("Do you want to restart the game?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

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
                    result = true;
                    break;
                case 2:
                    result = false;
                    break;
            }

            return result;

        }

        public void WinGame()
        {
            Console.WriteLine("Congratulations, you have won!");
            Console.WriteLine();
            this.gameWon = true;
            

        }

        public void LoseGame()
        {
            Console.WriteLine("You've lost!");
            Console.WriteLine();
            this.gameWon = false;
        }

        public void ResultReport(DifficultyLevel difficulty, int time)
        {
            Console.WriteLine("You used {0} of your chances and it took you {1} seconds to win!", difficulty.chances - this.currentChances, time);
        }

        public void SaveScore(DifficultyLevel difficulty, int time)
        {
            DateTime localDate = DateTime.Now.Date;
            //name| date | guessing_time | guessing_tries | difficulty
            string name = "";
            do {
                Console.Write("Your name: ");
                name = Console.ReadLine();
            } while ((name.Length < 1) || (name.Contains('|')));

            string result = name;
            result += " | " + DateTime.Now.ToString("dd/mm/yyyy");
            result += " | " + time;
            result += " | " + Convert.ToString(difficulty.chances - this.currentChances);
            result += " | " + difficulty.level;
            File.AppendAllText("../../../Scores.txt", result + Environment.NewLine);
        }

        public void DisplayHighscores()
        {
            
            this.scores = File.ReadAllLines(@"../../../Scores.txt");
            this.highscores = new string[scores.Length];
            int i = 0;
            string chances;
            int index;
            foreach(string scorePoint in scores)
            {
                chances = scorePoint;
                for (int j = 0; j < 3; j++)
                {
                    index = chances.IndexOf("|");
                    chances = chances.Substring(index + 2);
                }
                index = chances.IndexOf("|");
                this.highscores[i] = chances.Substring(0, index);
                
                i++;
            }

            Array.Sort(highscores);

            Console.WriteLine("Highscores: ");
            int highscoreCount = 10;
            if(this.scores.Length < 10) { highscoreCount = this.scores.Length; }
            for(int k = 0; k < highscoreCount; k++)
            {
                
                Console.WriteLine("{0}: chances used: {1}", k+1, this.highscores[k]);
            }
        }

    }
}
