using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public class ObjectUtils
    {
        public const char TYPE_BOOLEAN = 'e';

        public const char TYPE_STRING = 'c';

        public const char TYPE_INTEGER = 'i';

        public const char TYPE_DOUBLE = 'd';

        public const char TYPE_FLOAT = 'f';

        public const char TYPE_LONG = 'l';

        public const char TYPE_BIGDECMAL = 'b';

        public const char TYPE_TEXT = 't'; //就是clob类型 

        public const char TYPE_BLOB = 'o'; // blob类型

        public static Object string2Object(String value, char type)
        {
            if (value == null || value == "")
                return null;

            switch (type)
            {
                case TYPE_STRING:
                    return value;
                case TYPE_INTEGER:
                    return int.Parse(value);
                case TYPE_LONG:
                    return long.Parse(value);
                case TYPE_FLOAT:
                    return float.Parse(value);
                case TYPE_DOUBLE:
                    return double.Parse(value);
                case TYPE_BOOLEAN:
                    return bool.Parse(value);
                default:
                    return null;
            }
        }

    }
}
