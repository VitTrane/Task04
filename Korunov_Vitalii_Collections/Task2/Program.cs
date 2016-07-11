using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        private static char[] SEPARATORS = { ' ', '.', '\n','\r', ',' };

        private static IDictionary<string,int> _words = new Dictionary<string,int>();

        private static string _pathInputFile = "Input.txt";
        private static string _text;

        static void Main(string[] args)
        {
            _text = ReadTextFile(_pathInputFile);
            _words = CalculateCountIdenticalWords(_text);
            OutputWords(_words);
            
            Console.ReadLine();
        }

        /// <summary>
        /// Читает текстовый файл
        /// </summary>
        /// <param name="path">Полный путь к файлу для чтения</param>
        private static string ReadTextFile(string path)
        {
            string text;
            using (var reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        /// <summary>
        /// Считает количество одинаковых слов в тексте
        /// </summary>
        /// <param name="text">Текст в котором нужно посчитать слова</param>
        private static IDictionary<string,int> CalculateCountIdenticalWords(string text) 
        {
            Dictionary<string, int> identicalWords = new Dictionary<string, int>();

            string[] words = _text.Split(SEPARATORS,StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                string key=word.ToLower();

                if (identicalWords.ContainsKey(key))
                    identicalWords[key] += 1;
                else
                    identicalWords.Add(key, 1);
            }

            return identicalWords;
        }

        private static void OutputWords(IDictionary<string,int> words) 
        {
            foreach (var item in words)
            {
                Console.WriteLine("Слово {0} встретилось {1} раз(а)", item.Key, item.Value);
            }
        }
    }
}
