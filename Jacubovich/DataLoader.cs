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

        private static void SaveTxtFile(string line)
        {
            try
            {
                File.WriteAllText($"{directory}/{txtFileName}", "\n" + line);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(directory);
                SaveTxtFile(line);
            }
        }
        
        public static List<string> DeletFromTxt(string value)
        {
            List<string> words = LoadTxtFile();
            words.Remove(value);
            SaveTxtFile(words);
            return LoadTxtFile();
        }
        
        
        public static List<string> LoadTxtFile()
        {
            try
            {
                List<string> words = new List<string>();
                string line = null;
                using (StreamReader sr = new StreamReader($"{directory}/{txtFileName}", System.Text.Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        if (string.IsNullOrEmpty(line))
                        {
                            throw new ApplicationException($"файл {txtFileName} пустой");
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
                Console.WriteLine($"Отсутствует файл {txtFileName}");
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