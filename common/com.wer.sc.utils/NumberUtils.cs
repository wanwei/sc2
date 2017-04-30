using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public class NumberUtils
    {
        public static double Xiaoshu(double d)
        {
            return d - (int)d;
        }

        /// <summary>
        /// 得到d1相对于d2的涨幅百分比
        /// 如d1=102,d2=100，则返回2
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double percent(double d1, double d2)
        {
            return Math.Round(((d1 - d2) / d2) * 100, 2);
        }

        public static double percent(float d1, float d2)
        {
            return Math.Round(((d1 - d2) / d2) * 100, 2);
        }


        public static double ToDouble(Object obj)
        {
            return ToDouble(obj, 0);
        }

        public static double ToDouble(Object obj, double defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            if (obj is Double)
            {
                return (Double)obj;
            }
            try
            {
                return Double.Parse(obj.ToString());
            }
            catch (Exception nfe)
            {
                return defaultValue;
            }
        }
    }
}
