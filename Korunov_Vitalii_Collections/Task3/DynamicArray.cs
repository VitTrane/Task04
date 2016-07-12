using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 8;
        private const int DEFAULT_COUNT = 0;

        private T[] _list;

        private int _count;

        public DynamicArray()
        {
            NameValueCollection settings =
                (NameValueCollection)ConfigurationManager.GetSection("dynamicListSettings");

            var defaultCapacity = settings["DefaultCapacity"];
            int length = 0;
            if (defaultCapacity != null)
                length = int.Parse(defaultCapacity);
            else
                length = DEFAULT_CAPACITY;

            _count = DEFAULT_COUNT;
            _list = new T[length];
        }

        public DynamicArray(int capacity)
        {
            int length = capacity;
            _count = DEFAULT_COUNT;
            _list = new T[capacity];
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            int length = collection.ToList().Count;
            _count = length;
            _list = new T[length];
            Array.Copy(collection.ToArray(), _list, length);
        }

        /// <summary>
        /// Возвращает число элементов, которое содержиться в коллекции
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Возвращает емкость коллекции
        /// </summary>
        public int Capacity
        {
            get { return _list.Length; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException();

                return _list[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException();

                _list[index] = value;
            }
        }

        /// <summary>
        /// Добавляет объект в конец списка
        /// </summary>
        /// <param name="item">Объект, добавляемый в конец колекцыы</param>
        public void Add(T item)
        {
            if (_count >= _list.Length)
            {
                ExpandList(_list.Length);
            }

            _list[_count] = item;
            _count++;
        }

        /// <summary>
        /// Добавляет элементы указанной коллекциии в конец списка
        /// </summary>
        /// <param name="collection">Коллекция, элементы которой добавляются в конец списка</param>
        public void AddRange(IEnumerable<T> collection)
        {
            int countInCollection = collection.ToList().Count;
            if (_list.Length - _count < countInCollection)
            {
                ExpandList(countInCollection - (_list.Length - _count));
            }

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Добавляет элемент в список в позиции с указанным индексом
        /// </summary>
        /// <param name="index">Индекс по которому нужно вставить элемент</param>
        /// <param name="item">Элемент, вставляемый по указанному индексу</param>
        public bool TryInsert(int index, T item)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException();

            try
            {
                if (_count + 1 > _list.Length)
                {
                    ExpandList(_list.Length);
                }

                _count++;
                for (int i = _count - 1; i > index; i--)
                {
                    _list[i] = _list[i - 1];
                }
                _list[index] = item;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Удаляет первое вхождение указанного объекта из списка
        /// </summary>
        /// <param name="item">Объект, который следует удалить</param>
        public bool Remove(T item)
        {
            bool isRemoved = false;
            for (int i = 0; i < _count; i++)
            {
                if (item.Equals(_list[i]))
                {
                    ShiftArray(_list, i, _count);
                    _count--;
                    isRemoved = true;
                    break;
                }
            }

            return isRemoved;
        }

        /// <summary>
        /// Возращает перечислитель, осуществляющий перебор элементов списка
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new DataIterator<T>(_list, _count);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Расширяет массив
        /// </summary>
        /// <param name="capacity">Величина, на которую нужно расширить массив</param>
        private void ExpandList(int capacity)
        {
            int length = _list.Length + capacity;
            T[] extendedList = new T[length];
            Array.Copy(_list, extendedList, _count);
            _list = extendedList;
        }

        /// <summary>
        /// Сдвигает массив на один элемент влево
        /// </summary>
        /// <param name="array">Массив, который нужно сдвинуть</param>
        /// <param name="startShiftElement">Индекс, с которого нужно сдвинуть</param>
        /// <param name="indexLastElement">Индекс последнего, до которого нужно сдвигать</param>
        private void ShiftArray(T[] array, int startShiftElement, int indexLastElement)
        {
            for (int i = startShiftElement; i < indexLastElement - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[indexLastElement - 1] = default(T);
        }

        private class DataIterator<TIterator> : IEnumerator<TIterator>
        {
            private TIterator[] _list;
            private int _count;
            private int _index = -1;

            public DataIterator(TIterator[] collection, int countElements)
            {
                _list = collection;
                _count = countElements;
            }

            public TIterator Current
            {
                get { return _list[_index]; }
            }

            public void Dispose()
            {

            }

            object System.Collections.IEnumerator.Current
            {
                get { return _list[_index]; }
            }

            public bool MoveNext()
            {
                if (_index >= _count - 1)
                {
                    Reset();
                    return false;
                }

                _index++;
                return true;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
