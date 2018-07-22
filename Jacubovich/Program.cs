using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace Jacubovich
{
    
    internal class Program
    {
        private static Random rnd = MyRandom.Rand();
        private static WordGenerator generator = new WordGenerator();
        
        public static void Main(string[] args)
        {
            
            
            
            
            StartGame();
            
        }

        public static void StartGame()
        {
            
            int calk = rnd.Next(2,4);
            int difficult = rnd.Next(1, 5);
            Console.WriteLine($"Уровень сложности: {difficult}");
            
            List<string> a = DataLoader.LoadTxtFile();
            string word = a[rnd.Next(a.Count)];
            generator.Word = word;
            generator.Encrypting(difficult);
            string encrypWord = generator.EncryptedWord;
            Console.WriteLine(encrypWord);
            StartRound(word, encrypWord, ref calk);
            
        }

        public static void StartRound(string word, string encrypWord, ref int calk)
        {
            bool allRight;
            Console.WriteLine($"У вас {calk} попыток");
            Console.WriteLine("Угадайте одну букву или слово целиком:");
            try
            {
                string str = Console.ReadLine();
                if (int.TryParse(str, out int e))
                {
                    throw new ArgumentException("Слово не может состоять из цифр!");
                }

                string answerWord = generator.GetAnswer(str, word, encrypWord, out allRight);
                if (calk <= 0)
                {
                    Console.WriteLine("Попытки закнчились. Вы проиграли");
                    DataLoader.DeletFromTxt(word);
                    StartGame();
                    
                }
                else
                {
                    if (allRight)
                    {
                        Console.WriteLine($"У вас {calk} попыток");
                        if (!answerWord.Contains("*"))
                        {
                            Console.WriteLine($"Вы угадали слово {answerWord}\n" +
                                              $"Новая игра");
                            DataLoader.DeletFromTxt(word);
                            StartGame();
                        }
                        else
                        {
                            Console.WriteLine($"Вы правильно угадали букву осталось доотгадывать {answerWord}");
                            StartRound(word, answerWord, ref calk);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"У вас {calk} попыток");
                        if (!answerWord.Contains("*"))
                        {
                            Console.WriteLine($"Неправильно угадали слово\n" +
                                              $"Вы проиграли \n" +
                                              $"Новая игра");
                            DataLoader.DeletFromTxt(word);
                            StartGame();
                        }
                        else
                        {
                            Console.WriteLine($"Вы неправильно угадали букву {answerWord}");
                            calk--;
                            StartRound(word, answerWord, ref calk);
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}