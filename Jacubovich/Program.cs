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
            List<string> a = DataLoader.LoadTxtFile();
            WordGenerator wg = new WordGenerator();
            string g = a[rnd.Next(a.Count)];
            wg.Word = g;
            wg.Encrypting(1);
            
            Console.WriteLine(g);
            Console.WriteLine(wg.EncryptedWord);
        }
    }
}