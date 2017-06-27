using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;

namespace com.wer.sc.strategy.cnfutures.import
{
    /// <summary>
    /// 
    /// </summary>
    public class Strategy_Ma : IStrategy
    {
        private int length;

        private KLinePeriod period;

        private List<double> maPrice = new List<double>();

        public List<double> MaPrice
        {
            get
            {
                return maPrice;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public Strategy_Ma(KLinePeriod klinePeriod, int length)
        {
            this.period = klinePeriod;
            this.length = length;
        }

        public void OnBar(IRealTimeDataReader currentData)
        {
            IKLineData klineData = currentData.GetKLineData(period);
            int barPos = klineData.BarPos;
            int startPos = barPos - length;
            startPos = startPos < 0 ? 0 : startPos;

            double total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                total += klineData.Arr_End[i];
            }
            this.maPrice.Add(total / (barPos - startPos + 1));
        }


        public void OnTick(IRealTimeDataReader currentData)
        {

        }

        public void StrategyStart()
        {

        }

        public void StrategyEnd()
        {

        }

        public StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }
    }
}
