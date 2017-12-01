using com.wer.sc.data;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.ma
{
    public class Strategy_MA : StrategyAbstract
    {
        private bool isMainMa = false;

        private int maPeriod;

        private Dictionary<KLinePeriod, List<double>> dic_Period_MA = new Dictionary<KLinePeriod, List<double>>();

        public bool IsMainMa
        {
            get
            {
                return isMainMa;
            }

            set
            {
                isMainMa = value;
            }
        }

        public int MaPeriod
        {
            get
            {
                return maPeriod;
            }

            set
            {
                maPeriod = value;
            }
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            if (IsMainMa)
            {
                IList<IStrategyOnBarInfo> bars = currentData.Bars;
                RecordBars(bars);
            }
            else
            {
                IList<IStrategyOnBarInfo> bars = currentData.FinishedBars;
                RecordBars(bars);
            }
        }

        private void RecordBars(IList<IStrategyOnBarInfo> bars)
        {
            for (int i = 0; i < bars.Count; i++)
            {
                IStrategyOnBarInfo barInfo = bars[i];
                KLinePeriod period = barInfo.KLinePeriod;
                List<double> MaList;
                if (dic_Period_MA.ContainsKey(period))
                {
                    MaList = dic_Period_MA[period];
                }
                else
                {
                    MaList = new List<double>();
                    dic_Period_MA.Add(period, MaList);
                }
                MaList.Add(GetMa(barInfo, this.maPeriod));
            }
        }

        private double GetMa(IStrategyOnBarInfo barInfo, int period)
        {
            IKLineData klineData = barInfo.KLineData;
            int start = barInfo.BarPos - period;
            start = start < 0 ? 0 : start;
            double ma = 0;
            for (int i = start; i <= barInfo.BarPos; i++)
            {
                ma += klineData.Arr_End[i];
            }
            return ma / (barInfo.BarPos - start + 1);
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}
