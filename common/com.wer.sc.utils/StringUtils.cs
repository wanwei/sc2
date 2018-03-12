using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public class StringUtils
    {
        public static bool IsEmpty(String str)
        {
            if (str == null)
                return true;
            return str == "";
        }

        public static String GetPercent(double value)
        {
            String str = (Math.Round(value * 10000) / 100).ToString();
            int index = str.IndexOf(".");
            if (index < 0)
                str += ".00";
            else if (index == str.Length - 2)
                str += "0";
            return str + "%";
        }

        public static String GetPercentRound(double value)
        {
            String str = (Math.Round(value * 10000) / 100).ToString();
            int index = str.IndexOf(".");
            if (index < 0)
                return str + "%";
            return str.Substring(0, index) + "%";
        }

        public static String GetTime(String str)
        {
            String timeStr = str.Substring(str.Length - 6, 6);
            return timeStr.Substring(0, 2) + ":" + timeStr.Substring(2, 2) + ":" + timeStr.Substring(2, 2);
        }

        public static String JoinArr<T>(IList<T> arr)
        {
            return JoinArr(arr, ",");
        }

        public static String JoinArr<T>(IList<T> arr, string sep)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Count; i++)
            {
                if (i != 0)
                    sb.Append(sep);
                sb.Append(arr[i]);
            }
            return sb.ToString();
        }

        public static String JoinArr(String[] arr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != 0)
                    sb.Append(",");
                sb.Append(arr[i]);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 将对象转换为字符串，如果对象为null，则返回defaultValue，否则，返回obj.toString()<br />
        /// 例如：
        /// 
        /// StringUtils.obj2Str("a string", "")	= "a string"
        /// StringUtils.obj2Str(new StringBuffer("a string"), null)	= "a string"
        /// StringUtils.obj2Str(seasonList, null)	= ["spring", "summer", "autumn", "winter"] (seasonList是包含了四季的{@link ArrayList})
        /// StringUtils.obj2Str(new StringBuffer(null, "")  = "
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static String obj2Str(Object obj, String defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

    }
}
