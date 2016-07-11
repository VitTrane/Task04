using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        private static ICollection<int> _personCollection = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во человек в кругу:");
            int countPersons;
            if (int.TryParse(Console.ReadLine(), out countPersons))
            {
                FillCollection(_personCollection,countPersons);
            }

            OutputList(_personCollection);
            Console.WriteLine();

            RemoveEverySecondElement(_personCollection);

            Console.ReadLine();
        }

        /// <summary>
        /// Удаляет из коллекции каждый второй элемент по кругу
        /// </summary>
        /// <param name="collection"> Коллекция из которой удаляються элементы </param>
        private static void RemoveEverySecondElement(ICollection<int> collection)
        {
            int counter = 0;
            while (_personCollection.Count > 1)
            {
                foreach (var person in _personCollection.ToList())
                {
                    counter++;
                    if (counter == 2)
                    {
                        counter = 0;                        
                        _personCollection.Remove(person);
                        Console.WriteLine("Человек с номером {0} удален.", person);
                    }
                }

                OutputList(_personCollection);
                Console.WriteLine();
            }
        }

        private static void FillCollection(ICollection<int> collection, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                collection.Add(i);
            }
        }

        private static void OutputList(ICollection<int> collection) 
        {
            foreach (var item in collection)
            {
                Console.Write(item + " ");
            }
        }
    }
}
