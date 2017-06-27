using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class MathUtils
    {
        public static T RefData<T>(IList<T> arr, int currentIndex, int len)
        {
            int start = currentIndex - len;
            start = start < 0 ? 0 : start;
            return arr[start];
        }

        public static float Highest(IList<float> list, int currentIndex, int len)
        {
            int start = currentIndex - len;
            start = start < 0 ? 0 : start;
            float highest = float.MinValue;
            for (int i = start; i <= currentIndex; i++)
            {
                float d = list[i];
                if (d > highest)
                    highest = d;
            }
            return highest;
        }

        public static float Lowest(IList<float> list, int currentIndex, int len)
        {
            int start = currentIndex - len;
            start = start < 0 ? 0 : start;
            float lowest = float.MaxValue;
            for (int i = start; i <= currentIndex; i++)
            {
                float d = list[i];
                if (d < lowest)
                    lowest = d;
            }
            return lowest;
        }
    }
}
