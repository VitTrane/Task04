using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        private static DynamicArray<int> _list = new DynamicArray<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация класса DynamicArray:");
            Console.WriteLine("Введите сколько элементов добавить в список:");
            int countElements;
            if (int.TryParse(Console.ReadLine(), out countElements)) 
            {
                FillCollection(countElements);
            }
            OutputList(_list);

            Console.WriteLine();
            DemonstrateMethodAddRange();
            OutputList(_list);

            Console.WriteLine();
            DemonstrateMethodRemove();
            OutputList(_list);

            Console.WriteLine();
            DemonstrateMethodInsert();
            OutputList(_list);

            Console.ReadLine();
        }       

        private static void FillCollection(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _list.Add(i);
            }
        }

        private static void OutputList(DynamicArray<int> collection)
        {
            foreach (var item in collection)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Емкость списка: " + _list.Capacity);
            Console.WriteLine("Кол-во элементов в списке: " + _list.Count);
        }

        private static void DemonstrateMethodRemove() 
        {
            Console.WriteLine("Удалим элементы 1 и 5:");
            _list.Remove(1);
            _list.Remove(5);
        }

        private static void DemonstrateMethodAddRange() 
        {
            Console.WriteLine("Добавим к списку элементы 5 6 7 8 9 10:");
            int[] n = { 5, 6, 7, 8, 9, 10 };
            _list.AddRange(n);
        }

        private static void DemonstrateMethodInsert()
        {
            Console.WriteLine("Вставим обратно на место 1 и 5:");
            _list.TryInsert(1, 1);
            _list.TryInsert(5, 5);
        }
    }
}
