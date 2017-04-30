using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    /// <summary>
    /// 这个类专门为
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadOnlyList_TmpValue<T> : IList<T>
    {
        private IList<T> array;

        private int tmpIndex = -1;

        private T tmpValue;

        public ReadOnlyList_TmpValue(IList<T> array)
        {
            this.array = array;
        }

        public T GetRealValue(int index)
        {
            return array[index];
        }

        public T this[int index]
        {
            get
            {
                if (index == tmpIndex)
                    return tmpValue;
                return array[index];
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
                return array.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }


        public bool Contains(T item)
        {
            return array.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.array.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            return this.array.IndexOf(item);
        }

        //public T[] GetArray()
        //{
        //    return array;
        //}

        public void SetTmpValue(int index, T value)
        {
            this.tmpIndex = index;
            this.tmpValue = value;
        }

        public void ClearTmpValue()
        {
            this.tmpIndex = -1;
        }

        public int TmpIndex
        {
            get
            {
                return tmpIndex;
            }
        }

        public T TmpValue
        {
            get
            {
                return tmpValue;
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
