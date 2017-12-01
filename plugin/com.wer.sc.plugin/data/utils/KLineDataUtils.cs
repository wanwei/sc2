using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineDataUtils
    {
        public static List<float> MaList(IKLineData klineData, int startIndex, int endIndex, int len)
        {
            List<float> maList = new List<float>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                maList.Add(Ma(klineData, i, len));
            }
            return maList;
        }

        public static float Ma(IKLineData klineData, int index, int len)
        {
            float d = 0;
            int start = index - len + 1;
            start = start < 0 ? 0 : start;
            len = index - start + 1;
            for (int i = start; i <= index; i++)
            {
                d += klineData.Arr_End[i];
            }
            return d / len;
        }
    }
}
