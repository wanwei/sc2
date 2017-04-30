using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class ItemObject
    {
        public string Text = "";
        public object Value;

        public ItemObject(string _text, object _value)
        {
            Text = _text;
            Value = _value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
