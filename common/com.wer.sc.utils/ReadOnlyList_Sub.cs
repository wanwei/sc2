using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class ReadOnlyList_Sub<T> : IList<T>
    {
        private IList<T> list;
        private int startIndex;
        private int endIndex;

        public ReadOnlyList_Sub(IList<T> list, int startIndex, int endIndex)
        {
            this.list = list;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                    throw new IndexOutOfRangeException(index + "超出了列表界线" + Count);
                return list[index + startIndex];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                return endIndex - startIndex + 1;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public int IndexOf(T item)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                T t = list[i];
                if (t.Equals(item))
                    return i - startIndex;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
