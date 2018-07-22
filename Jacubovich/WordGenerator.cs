﻿using System;
using System.Collections.Generic;

namespace Jacubovich
{
    public class WordGenerator
    {
        public string Word { get; set; }
        public string EncryptedWord { get; private set; }
        private Random rand = MyRandom.Rand();

        public void Encrypting(int difficulty)
        {
            try
            {
                if (Word == null)
                {
                    throw new NullReferenceException("Слова кончились");
                }

                char[] bufWord = Word.ToCharArray();
                char[] encryptedWord = null;
                
                if (difficulty == 4)
                {
                    encryptedWord = Filling(Word);
                }
                else if (difficulty == 3)
                {
                    encryptedWord = Filling(Word);
                    int visibleChar = rand.Next(bufWord.Length);
                    for (int i = 0; i < bufWord.Length; i++)
                    {
                        if (i == visibleChar)
                        {
                            encryptedWord[i] = bufWord[i];
                        }
                        else
                        {
                            encryptedWord[i] = '*';
                        }
                    }
                } 
                else if (difficulty == 2)
                {
                    List<int> bufList = GenerateNumbers(2, Word.Length);
                    encryptedWord = Filling(Word);
                    for (int i = 0; i < bufWord.Length; i++)
                    {
                        for (int j = 0; j < bufList.Count; j++)
                        {
                            if (bufList[j] == i)
                            {
                                encryptedWord[i] = bufWord[i];
                                bufList.RemoveAt(j);
                            }
                        }
                    } 
                }
                else if (difficulty == 1)
                {
                    List<int> bufList = GenerateNumbers(3, Word.Length);
                    encryptedWord = Filling(Word);
                    for (int i = 0; i < bufWord.Length; i++)
                    {
                        for (int j = 0; j < bufList.Count; j++)
                        {
                            
                                
                            if (bufList[j] == i)
                            {
                                encryptedWord[i] = bufWord[i];
                                bufList.RemoveAt(j);
                            }
                        }
                    } 
                }
                EncryptedWord = new string(encryptedWord);
                
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
            
        }

        private char[] Filling(string word)
        {
            char[] fillingWord = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                fillingWord[i] = '*';
            }

            return fillingWord;
        }

        public List<int> GenerateNumbers(int count, int length)
        {
            List<int> buf = new List<int>();
            List<int> result = new List<int>();
            for (int i = 0; i < length; i++)
            {
                buf.Add(i);
            }

            for (int i = 0; i < count; i++)
            {
                int a = rand.Next(buf.Count);
                result.Add(buf[a]);
                buf.RemoveAt(a);
            }

            return result;
        }

        public string GetAnswer(string answer, string word, string encryptWord, out bool allright)
        {
            List<char> encWordArr = new List<char>(encryptWord.ToCharArray());
            List<char> wordArr = new List<char>(word.ToCharArray());
            List<char> answerArr = new List<char>(answer.ToCharArray());
            if (answerArr.Count == 1)
            {
                if (wordArr.Contains(answer[0]))
                {
                    for (int i = 0; i < wordArr.Count; i++)
                    {
                        if (answer[0] == wordArr[i])
                        {
                            encWordArr[i] = answer[0];
                            allright = true;
                            return new string(encWordArr.ToArray());
                        }
                    }
                } else
                {
                    allright = false;
                    return encryptWord;
                }
            }
            else
            {
                if (answer == word)
                {
                    allright = true;
                    return answer;
                }
                else
                {
                    allright = false;
                    return encryptWord;
                }
            }
            allright = false;
            return encryptWord;
        }
    }
}