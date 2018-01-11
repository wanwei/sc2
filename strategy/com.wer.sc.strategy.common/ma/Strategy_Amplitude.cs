using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.ma
{
    public class Strategy_Amplitude : StrategyAbstract
    {
        private StrategyArray<float> amplitudes = new StrategyArray<float>();

        private int amplitudeLength = 50;

        public int AmplitudeLength
        {
            get
            {
                return amplitudeLength;
            }

            set
            {
                amplitudeLength = value;
            }
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(MainKLinePeriod);
            if (klineData == null)
                throw new StrategyException("没找到" + MainKLinePeriod + "K线数据");

            GenAmplitude(klineData, amplitudes, AmplitudeLength);
        }

        private void GenAmplitude(IKLineData klineData, IList<float> amplitudeList, int length)
        {
            int barPos = klineData.BarPos;
            int startPos = barPos - length + 1;
            startPos = startPos < 0 ? 0 : startPos;

            float high = klineData.High;
            float low = klineData.Low;

            for (int i = startPos; i < barPos; i++)
            {
                float currentHigh = klineData.Arr_High[i];
                if (high < currentHigh)
                    high = currentHigh;
                float currentLow = klineData.Arr_Low[i];
                if (low > currentLow)
                    low = currentLow;
            }
            amplitudeList[barPos] = high - low;
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}