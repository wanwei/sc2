using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class EnumUtils
    {
        public static Object Parse(Type type, object obj)
        {
            if (obj == null)
                return null;
            if (obj is int)
                return Parse(type, (int)obj);
            return Parse(type, obj.ToString());
        }

        public static Object Parse(Type type, string str)
        {
            return Enum.Parse(type, str);
        }

        public static Object Parse(Type type, int value)
        {
            return Parse(type, Enum.GetName(type, value));
        }
    }
}
