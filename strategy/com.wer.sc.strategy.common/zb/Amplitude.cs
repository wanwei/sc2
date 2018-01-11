using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy.common.zb
{
    public class Amplitude : IDataLooper
    {
        private StrategyArray<float> amplitudes = new StrategyArray<float>();

        private IKLineData klineData;
        //计算长度
        private int length = 50;
        //是否计算阴影部分
        private bool calcShadow = false;

        public Amplitude(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public Amplitude(IKLineData klineData, int length)
        {
            this.klineData = klineData;
            this.length = length;
        }

        public Amplitude(IKLineData klineData, int length, bool calcShadow)
        {
            this.klineData = klineData;
            this.length = length;
            this.calcShadow = calcShadow;
        }

        public void Loop(int barPos)
        {
            int startPos = barPos - length + 1;
            startPos = startPos < 0 ? 0 : startPos;

            float high = klineData.High;
            float low = klineData.Low;

            float currentHigh;
            float currentLow;
            for (int i = startPos; i < barPos; i++)
            {
                if (calcShadow)
                    currentHigh = klineData.Arr_BlockHigh[i];
                else
                    currentHigh = klineData.Arr_High[i];
                if (high < currentHigh)
                    high = currentHigh;
                if (calcShadow)
                    currentLow = klineData.Arr_BlockLow[i];
                else
                    currentLow = klineData.Arr_Low[i];
                if (low > currentLow)
                    low = currentLow;
            }
            amplitudes[barPos] = high - low;
        }

        public StrategyArray<float> Amplitudes
        {
            get { return amplitudes; }
        }
    }
}