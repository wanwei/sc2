using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class ListUtils
    {
        public static List<int> EmptyIntList = new List<int>();


        public static String[] list2Arr(List<String> list)
        {
            return list.ToArray();
        }

        public static String ToString(List<String> arr)
        {
            if (arr == null)
                return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Count; i++)
            {
                if (i != 0)
                    sb.Append(",");
                sb.Append(arr[i]);
            }
            return sb.ToString();
        }

        //public static String ToString<T>(List<T> arr)
        //{
        //    if (arr == null)
        //        return "";
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < arr.Count; i++)
        //    {
        //        if (i != 0)
        //            sb.Append(",");
        //        sb.Append(arr[i]);
        //    }
        //    return sb.ToString();
        //}

        public static String ToString(String[] arr)
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
