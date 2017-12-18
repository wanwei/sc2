using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class OffsetList<T> : IList<T>
    {
        private List<T> list = new List<T>();

        private int offset;

        public OffsetList(int offset)
        {
            this.offset = offset;
        }

        private int RealIndex(int index)
        {
            return index - offset;
        }

        public virtual T this[int index]
        {
            get
            {
                return list[RealIndex(index)];
            }

            set
            {
                this.list[RealIndex(index)] = value;
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual void Add(T item)
        {
            this.list.Add(item);
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

        public virtual void Insert(int index, T item)
        {
            list.Insert(RealIndex(index), item);
        }

        public virtual bool Remove(T item)
        {
            return list.Remove(item);
        }

        public virtual void RemoveAt(int index)
        {
            list.RemoveAt(RealIndex(index));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
