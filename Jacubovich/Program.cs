using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace Jacubovich
{
    
    internal class Program
    {
        private static Random rnd = MyRandom.Rand();
        public static void Main(string[] args)
        {
            
            
            
            
            StartGame();
            
        }

        public static void StartGame()
        {
            int difficult = rnd.Next(1, 5);
            Console.WriteLine($"Уровень сложности: {difficult}");
            WordGenerator generator = new WordGenerator();
            List<string> a = DataLoader.LoadTxtFile();
            string word = a[rnd.Next(a.Count)];
            generator.Word = word;
            generator.Encrypting(difficult);  
        }
        
        
    }
}