using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    /// <summary>
    /// K线工具类，用来计算K线的各种信息
    /// </summary>
    public class KLineDataMath
    {
        private IKLineData klineData;

        public KLineDataMath(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public float Height(int len)
        {
            float high2 = klineData.High;
            float low2 = klineData.Low;
            int pos = klineData.BarPos;
            for (int i = pos - len + 1; i < pos; i++)
            {
                float chigh = klineData.Arr_High[i];
                float clow = klineData.Arr_Low[i];
                high2 = high2 > chigh ? high2 : chigh;
                low2 = low2 < clow ? low2 : clow;
            }
            return high2 - low2;
        }

        public float Height(IList<float> values, int len)
        {
            int pos = klineData.BarPos;
            float high = values[pos];
            float low = values[pos];
            for (int i = pos - len + 1; i < pos; i++)
            {
                float chigh = values[i];
                float clow = values[i];
                high = high > chigh ? high : chigh;
                low = low < clow ? low : clow;
            }
            return high - low;
        }

        public float Ma(IList<float> values, int len)
        {
            float ma = 0;
            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i < endIndex; i++)
                ma += values[i];
            return (float)Math.Round(ma / (endIndex - startindex), 3);
        }

        public float Max(IList<float> values, int len)
        {
            float max = 0;
            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i < endIndex; i++)
            {
                if (max < values[i])
                    max = values[i];
            }
            return max;
        }

        public int MaxBars(IList<float> values, int len)
        {
            int index = -1;
            float max = 0;
            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                    index = i;
                }
            }
            return index;
        }

        public float Min(IList<float> values, int len)
        {
            float min = 0;
            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (min > values[i])
                    min = values[i];
            }
            return min;
        }

        public int MinBars(IList<float> values, int len)
        {
            int index = -1;
            float min = float.MaxValue;
            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i <= endIndex; i++)
            {
                if (min > values[i])
                {
                    min = values[i];
                    index = i;
                }
            }
            return index;
        }

        public float Lowest(IList<float> values, int len)
        {
            return values[LowestBars(values, len)];
        }

        public float Highest(IList<float> values, int len)
        {
            return values[HighestBars(values, len)];
        }

        public int LowestBars(IList<float> values, int len)
        {
            float low = float.MaxValue;
            int lowIndex = 0;

            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            for (int i = startindex; i < endIndex; i++)
            {
                float value = values[i];
                if (value < low)
                {
                    lowIndex = i;
                    low = value;
                }
            }
            return lowIndex;
        }

        public int HighestBars(IList<float> values, int len)
        {
            float high = float.MinValue;
            int highIndex = 0;

            int startindex = klineData.BarPos - len + 1;
            int endIndex = klineData.BarPos;
            for (int i = startindex; i < endIndex; i++)
            {
                float value = values[i];
                if (value > high)
                {
                    highIndex = i;
                    high = value;
                }
            }
            return highIndex;
        }

        public float AveragePrice(IList<float> values, int len)
        {
            //TODO
            return 0;
        }

        /**
         * 数组1是否和数组2相交
         * @param values1
         * @param values2
         * @return 0未相交；1数组1向上穿过数组2；2数组1向下穿过数组2
         */
        public int Cross(IList<float> values1, IList<float> values2)
        {
            float value1 = values1[klineData.BarPos];
            float value1Pre = values1[klineData.BarPos - 1];            
            
            float value2 = values2[klineData.BarPos];
            float value2Pre = values2[klineData.BarPos - 1];

            if (value1 > value2 && value1Pre < value2Pre)
                return 1;
            if (value1 < value2 && value1Pre > value2Pre)
                return -1;
            return 0;
        }

        public int Cross(float[] values1, float[] values2)
        {
            float value1 = values1[klineData.BarPos];
            float value1Pre = values1[klineData.BarPos - 1];

            float value2 = values2[klineData.BarPos];
            float value2Pre = values2[klineData.BarPos - 1];

            if (value1 > value2 && value1Pre < value2Pre)
                return 1;
            if (value1 < value2 && value1Pre > value2Pre)
                return -1;
            return 0;
        }
    }
}
