using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 交易时间实现类
    /// 该类会记录所有中间休息停盘时间
    /// </summary>
    public class TradingTime : ITradingTime
    {
        private int tradingDay;

        private List<double[]> tradingPeriods;

        public TradingTime()
        {
            this.tradingPeriods = new List<double[]>();
        }

        public TradingTime(int tradingDay, List<double[]> tradingPeriods)
        {
            this.tradingDay = tradingDay;
            this.tradingPeriods = tradingPeriods;
        }

        public int TradingDay
        {
            get
            {
                return tradingDay;
            }
        }

        public double OpenTime
        {
            get
            {
                return tradingPeriods[0][0];
            }
        }

        public double CloseTime
        {
            get
            {
                return tradingPeriods[tradingPeriods.Count - 1][1];
            }
        }

        public bool IsOverNight
        {
            get
            {
                return (int)CloseTime - (int)OpenTime > 0;
            }
        }

        public int PeriodCount
        {
            get
            {
                return tradingPeriods.Count;
            }
        }

        public double[] GetPeriodTime(int index)
        {
            return tradingPeriods[index];
        }

        public IList<double[]> TradingPeriods
        {
            get { return tradingPeriods; }
        }

        public string SaveToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tradingDay);
            for (int i = 0; i < tradingPeriods.Count; i++)
            {
                sb.Append(",");
                double[] tradingPeriod = tradingPeriods[i];
                sb.Append(tradingPeriod[0]).Append("-").Append(tradingPeriod[1]);
            }
            return sb.ToString();
        }

        public void LoadFromString(string content)
        {
            if (content == null || content == "")
                return;
            string[] arr = content.Split(',');
            this.tradingDay = int.Parse(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                string str = arr[i];
                string[] timeArr = str.Split('-');
                this.tradingPeriods.Add(new double[] { double.Parse(timeArr[0]), double.Parse(timeArr[1]) });
            }
        }

        public override string ToString()
        {
            return SaveToString();
        }
    }
}