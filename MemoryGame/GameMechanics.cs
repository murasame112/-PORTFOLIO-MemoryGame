using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class GameMechanics
    {
        

        string[] words; 
        public void SaveWords(string filePath)
        {
            words = System.IO.File.ReadAllLines(@filePath);
        }

        public void ShowWords()
        {
            foreach(string word in words)
            {
                Console.WriteLine(word);
            }
        }

       /* public string GenerateXWords(int number)
        {
            

        }*/



    }
}
