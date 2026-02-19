using System;
using System.Collections;
using Lab1.LogsClasses;

namespace Lab1.Collections
{
    public class SaleCollection : IEnumerable
    {
        private MatchInfo[] items;
        private int count;

        public SaleCollection(int capacity = 10)
        {
            items = new MatchInfo[capacity];
            count = 0;
        }

        public int Count => count;

        public MatchInfo GetAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range");
            return items[index];
        }

        public void SetAt(int index, MatchInfo value)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range");
            items[index] = value;
        }

        public void Add(MatchInfo item)
        {
            if (count == items.Length)
                Array.Resize(ref items, items.Length * 2);
            items[count++] = item;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range");

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
        }

        public IEnumerator GetEnumerator()
        {
            return new SaleEnumerator(this);
        }

        private class SaleEnumerator : IEnumerator
        {
            private SaleCollection collection;
            private int index = -1;

            public SaleEnumerator(SaleCollection collection)
            {
                this.collection = collection;
            }

            public object Current
            {
                get
                {
                    if (index < 0 || index >= collection.count)
                        throw new InvalidOperationException();
                    return collection.items[index];
                }
            }

            public bool MoveNext()
            {
                index++;
                return index < collection.count;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }
}
