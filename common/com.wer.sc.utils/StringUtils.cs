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
    }
}
