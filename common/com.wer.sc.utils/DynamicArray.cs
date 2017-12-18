using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    /// <summary>
    /// 动态数组
    /// </summary>
    public class DynamicArray<T> : IList<T>
    {
        private List<T> list;

        //TODO 优化
        private int offset;

        public DynamicArray()
        {
            this.list = new List<T>();
        }

        public DynamicArray(int capacity)
        {
            this.list = new List<T>(capacity);
        }

        private int RealIndex(int index)
        {
            return index - offset;
        }

        public virtual T this[int index]
        {
            get
            {
                int realIndex = RealIndex(index);
                if (realIndex < 0)
                    return default(T);
                return list[realIndex];
            }

            set
            {
                int realIndex = RealIndex(index);
                if (realIndex >= list.Count)
                {
                    EnlargeList(realIndex);
                }
                this.list[realIndex] = value;
            }
        }

        private void EnlargeList(int index)
        {
            list.AddRange(new T[index - list.Count + 1]);
        }

        private void EnlargeList_Before(int count)
        {
            list.InsertRange(0, new T[count]);
        }

        public int Length
        {
            get
            {
                return list.Count + offset;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public int Count
        {
            get
            {
                return list.Count + offset;
            }
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains<T>(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item) + offset;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}