using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class BooleanUtils
    {
        public static bool Parse(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool)
                return (bool)obj;
            return Boolean.Parse(obj.ToString());
        }
    }
}
