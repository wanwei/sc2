using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线时间信息
    /// 包括每一个bar的起止时间
    /// 每日的起止bar
    /// </summary>
    public class KLineDataTimeInfo : IKLineDataTimeInfo
    {
        private KLinePeriod klinePeriod;

        private IList<double[]> klineTimeInfo;

        private ISet<int> set_PeriodEnd;

        private ISet<int> set_DayEnd;

        private IList<int> periodEndBarPoses;

        private IList<int> dayEndBarPoses;

        private IList<int> tradingDays;

        private Dictionary<int, int> dic_TradingDay_StartPos = new Dictionary<int, int>();

        private Dictionary<int, int> dic_TradingDay_EndPos = new Dictionary<int, int>();

        public KLineDataTimeInfo(IList<double[]> klineTimeInfo, IList<int> periodEndBarPoses, IList<int> tradingDays, IList<int> dayEndBarPoses, KLinePeriod klinePeriod)
        {
            this.klineTimeInfo = klineTimeInfo;
            this.periodEndBarPoses = periodEndBarPoses;
            this.dayEndBarPoses = dayEndBarPoses;
            this.set_PeriodEnd = new HashSet<int>();
            for (int i = 0; i < periodEndBarPoses.Count; i++)
                set_PeriodEnd.Add(periodEndBarPoses[i]);
            this.set_DayEnd = new HashSet<int>();
            for (int i = 0; i < dayEndBarPoses.Count; i++)
            {
                set_DayEnd.Add(dayEndBarPoses[i]);
                int tradingDay = tradingDays[i];
                if (i == 0)
                {
                    dic_TradingDay_StartPos.Add(tradingDay, 0);
                    dic_TradingDay_EndPos.Add(tradingDay, dayEndBarPoses[0]);
                }
                else
                {
                    dic_TradingDay_StartPos.Add(tradingDay, dayEndBarPoses[i - 1] + 1);
                    dic_TradingDay_EndPos.Add(tradingDay, dayEndBarPoses[i]);
                }
            }
            this.tradingDays = tradingDays;
            this.klinePeriod = klinePeriod;
        }

        /// <summary>
        /// 得到barPos对应的K线的
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public double[] GetKLineTime(int barPos)
        {
            return klineTimeInfo[barPos];
        }

        public int GetDayStartPos(int tradingDay)
        {
            if (dic_TradingDay_StartPos.ContainsKey(tradingDay))
                return dic_TradingDay_StartPos[tradingDay];
            return -1;
        }

        public int GetDayEndPos(int tradingDay)
        {
            if (dic_TradingDay_EndPos.ContainsKey(tradingDay))
                return dic_TradingDay_EndPos[tradingDay];
            return -1;
        }

        public IList<double[]> GetTradingTime(int tradingDay)
        {
            throw new NotImplementedException();
        }

        public bool IsPeriodStart(int barPos)
        {
            if (barPos == 0)
                return true;
            return IsPeriodEnd(barPos - 1);
        }

        public bool IsPeriodEnd(int barPos)
        {
            return set_PeriodEnd.Contains(barPos);
        }

        public bool IsDayStart(int barPos)
        {
            if (barPos == 0)
                return true;
            return IsDayEnd(barPos - 1);
        }

        public bool IsDayEnd(int barPos)
        {
            return set_DayEnd.Contains(barPos);
        }

        public KLinePeriod KLinePeriod
        {
            get { return klinePeriod; }
        }

        public IList<int> TradingTimeEndBarPoses
        {
            get
            {
                return periodEndBarPoses;
            }
        }

        public IList<int> DayEndBarPoses
        {
            get
            {
                return dayEndBarPoses;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("KLinePeriod:").Append(klinePeriod);
            for (int i = 0; i < klineTimeInfo.Count; i++)
            {
                sb.Append("\r\n");
                double[] timeInfo = klineTimeInfo[i];
                sb.Append(timeInfo[0]).Append("-").Append(timeInfo[1]).Append(",");
                sb.Append(IsPeriodEnd(i)).Append(",");
                sb.Append(IsDayEnd(i));
            }
            sb.Append("\r\nTradingDay:");
            for (int i = 0; i < tradingDays.Count; i++)
            {
                sb.Append("\r\n");
                int tradingDay = tradingDays[i];
                sb.Append(tradingDay).Append(":");
                sb.Append(GetDayStartPos(tradingDay));
                sb.Append("-");
                sb.Append(GetDayEndPos(tradingDay));
            }
            return sb.ToString();
        }
    }
}