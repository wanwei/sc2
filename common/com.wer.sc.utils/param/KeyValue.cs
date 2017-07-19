using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    public class KeyValue : IComparable<KeyValue>
    {

        private Object key;

        private Object value;

        public object Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public object Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public KeyValue(Object key)
        {
            this.Key = key;
            this.Value = key;
        }

        public KeyValue(Object key, Object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override bool Equals(Object value)
        {
            if (value == null || (!(value is KeyValue)))
                return false;
            KeyValue keyvalue = (KeyValue)value;
            if (keyvalue.Key == null)
                return this.Key == null ? true : false;
            return this.Key == null ? false : this.Key.Equals(keyvalue.Key);
        }
        public override string ToString()
        {
            return StringUtils.obj2Str(Value, "");
        }

        public override int GetHashCode()
        {
            if (this.Key == null)
                return 0;
            return this.Key.GetHashCode();
        }

        public int CompareTo(KeyValue other)
        {
            if (other == null)
                return 1;
            if (other.Key == null)
                return this.Key == null ? 0 : 1;
            if (this.Key == null)
                return -1;
            //return ExpUtils.compareObject(this.key, o.key);
            throw new NotImplementedException();
        }
    }
}
