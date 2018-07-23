using System;
using System.Collections.Generic;
using System.IO;

namespace Jacubovich
{
    public class DataLoader
    {
        const string directory = "../../Data";
        const string txtFileName = "myWords.txt";
        
        private static void SaveTxtFile(List<string> content)
        {
            try
            {
                File.WriteAllLines($"{directory}/{txtFileName}", content);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(directory);
                SaveTxtFile(content);
            }
        }

        public static void SaveTxtFile(string line, string mTxtFileName)
        {
            try
            {
                using (StreamWriter str = new StreamWriter($"{directory}/{mTxtFileName}", true))
                {
                    str.WriteLine(line);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(directory);
            }
        }
        
        public static List<string> DeletFromTxt(string value)
        {
            List<string> words = LoadTxtFile(txtFileName);
            words.Remove(value);
            SaveTxtFile(words);
            return LoadTxtFile(txtFileName);
        }

        public static void ClearText(string mTxtFileName)
        {
            File.WriteAllText($"{directory}/{mTxtFileName}", string.Empty);
        }

        public static void Rewrite()
        {
            List<string> a = LoadTxtFile("oldWords.txt");
            try
            {
                using (StreamWriter str = new StreamWriter($"{directory}/{txtFileName}", true))
                {
                    foreach (string s in a)
                    {
                        str.WriteLine(s);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(directory);
            }
            
        }
        
        public static List<string> LoadTxtFile(string mTxtFileName)
        {
            try
            {
                List<string> words = new List<string>();
                string line = null;
                using (StreamReader sr = new StreamReader($"{directory}/{mTxtFileName}", System.Text.Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        if (string.IsNullOrEmpty(line))
                        {
                            throw new ApplicationException($"файл {mTxtFileName} пустой");
                        }
                        words.Add(line);
                    }
                }

                return words;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Отсутствует директория {directory}");
                return new List<string>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Отсутствует файл {mTxtFileName}");
                return new List<string>();
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }
    }

}