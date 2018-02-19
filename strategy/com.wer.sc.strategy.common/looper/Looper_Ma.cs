using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.looper
{
    /// <summary>
    /// 基于MA
    /// </summary>
    public class Looper_Ma : IDataLooper
    {
        private IKLineData klineData;

        private List<int> periods;

        private Dictionary<int, MaData> dic_Period_MaData = new Dictionary<int, MaData>();

        public Looper_Ma(IKLineData klineData, List<int> periods)
        {
            this.klineData = klineData;
            this.periods = periods;
            foreach (int period in periods)
            {
                dic_Period_MaData.Add(period, new MaData(period));
            }
        }

        public MaData GetMaData(int period)
        {
            if (dic_Period_MaData.ContainsKey(period))
                return dic_Period_MaData[period];
            return null;
        }

        public void Loop(int barPos)
        {
            foreach (int period in periods)
            {
                MaData maData = dic_Period_MaData[period];
                maData.AddValue(barPos, klineData.GetBar(barPos), CalcMa(period, barPos));
            }
        }

        private double CalcMa(int period, int barPos)
        {
            int startPos = barPos - period + 1;
            startPos = startPos < 0 ? 0 : startPos;

            float total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                float value = klineData.Arr_End[i];
                total += value;
            }
            return total / period;
        }
    }

    public class MaData
    {
        private int period;

        private StrategyArray<double> data = new StrategyArray<double>();

        private List<MaCrossPoint> recentCrossPoints = new List<MaCrossPoint>();

        public MaData(int period)
        {
            this.period = period;
        }

        public void AddValue(int barPos, IKLineBar klineBar, double value)
        {
            data[barPos] = value;
            if (klineBar.High > value && klineBar.Low < value)
            {
                MaCrossPoint point = new MaCrossPoint(barPos, klineBar, value);
                recentCrossPoints.Add(point);
            }
        }

        public StrategyArray<double> Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// MA和K线最近的交叉点
        /// </summary>
        public List<MaCrossPoint> RecentCrossPoints
        {
            get
            {
                return recentCrossPoints;
            }
        }
    }

    /// <summary>
    /// ma和bar的交叉点
    /// </summary>
    public class MaCrossPoint
    {
        private int barPos;

        private IKLineBar klineBar;

        private double maPrice;

        public MaCrossPoint(int barPos,IKLineBar klineBar,double maPrice)
        {
            this.barPos = barPos;
            this.klineBar = klineBar;
            this.maPrice = maPrice;
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }
        }

        public IKLineBar KlineBar
        {
            get
            {
                return klineBar;
            }            
        }

        public double MaPrice
        {
            get
            {
                return maPrice;
            }            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(barPos + ":");
            sb.Append(klineBar + "|" + maPrice);
            return sb.ToString();
        }
    }
}
