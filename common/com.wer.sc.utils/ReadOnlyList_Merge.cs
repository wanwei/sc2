using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class ReadOnlyList_Merge<T> : IList<T>
    {
        private IList<T> list1;

        private IList<T> list2;

        public ReadOnlyList_Merge(IList<T> list1, IList<T> list2)
        {
            this.list1 = list1;
            this.list2 = list2;
        }

        public T this[int index]
        {
            get
            {
                if (index < list1.Count)
                    return list1[index];
                return list2[index - list1.Count];
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
                return list1.Count + list2.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
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

        public int IndexOf(T item)
        {
            int index = list1.IndexOf(item);
            if (index > 0)
                return index;
            index = list2.IndexOf(item);
            if (index < 0)
                return -1;
            return index + list1.Count;
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