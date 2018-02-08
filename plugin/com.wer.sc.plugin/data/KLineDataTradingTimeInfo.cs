using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线的交易时间信息
    /// 该类能够得到交易时段和K线bar的对应关系
    /// 
    /// </summary>
    public class KLineDataTradingTimeInfo : IKLineDataTradingTimeInfo
    {
        //所有的交易日
        private List<int> tradingDays = new List<int>();

        //每个交易日的交易时段信息
        private Dictionary<int, IKLineDataTradingTimeInfo_Day> dic_TradingDay_KLineTimeInfo = new Dictionary<int, IKLineDataTradingTimeInfo_Day>();

        private List<IKLineDataTradingTimeInfo_Day> timeInfo_DayList = new List<IKLineDataTradingTimeInfo_Day>();

        public List<int> TradingDays
        {
            get
            {
                return tradingDays;
            }
        }

        public List<IKLineDataTradingTimeInfo_Day> TradingDayInfos
        {
            get { return timeInfo_DayList; }
        }

        /// <summary>
        /// 构造函数
        /// 传入K线信息以及
        /// </summary>
        /// <param name="klineData"></param>
        /// <param name="tradingTimes"></param>
        public KLineDataTradingTimeInfo(IKLineData klineData, IList<ITradingTime> tradingTimes)
        {
            if (klineData.Period.Equals(KLinePeriod.KLinePeriod_1Day))
                Init_Day(klineData, tradingTimes);
            else
                Init(klineData, tradingTimes);
        }

        private void Init_Day(IKLineData klineData, IList<ITradingTime> tradingTimes)
        {
            for (int i = 0; i < tradingTimes.Count; i++)
            {
                ITradingTime tradingTime = tradingTimes[i];
                int tradingDay = (int)klineData.GetBar(i).Time;
                KLineDataTradingTimeInfo_Day timeInfo = new KLineDataTradingTimeInfo_Day(tradingDay);
                timeInfo.StartPos = i;
                timeInfo.EndPos = i;                
                this.dic_TradingDay_KLineTimeInfo.Add(timeInfo.TradingDay, timeInfo);
                this.timeInfo_DayList.Add(timeInfo);
                tradingDays.Add(tradingTime.TradingDay);
            }
        }

        private void Init(IKLineData klineData, IList<ITradingTime> tradingTimes)
        {
            int currentKLineBarPos = 0;
            for (int i = 0; i < tradingTimes.Count; i++)
            {
                ITradingTime tradingTime = tradingTimes[i];
                KLineDataTradingTimeInfo_Day timeInfo = CalcKLineTimeInfo_Day(tradingTime, klineData, currentKLineBarPos);
                if (timeInfo == null)
                    continue;
                this.dic_TradingDay_KLineTimeInfo.Add(timeInfo.TradingDay, timeInfo);
                this.timeInfo_DayList.Add(timeInfo);
                currentKLineBarPos = timeInfo.EndPos;
                tradingDays.Add(tradingTime.TradingDay);
            }

            for (int i = 1; i < timeInfo_DayList.Count; i++)
            {
                int lastTradingDay = timeInfo_DayList[i - 1].TradingDay;
                KLineDataTradingTimeInfo_Day tradingTimeInfo_Day = (KLineDataTradingTimeInfo_Day)timeInfo_DayList[i];
                int currentTradingDay = tradingTimeInfo_Day.TradingDay;
                tradingTimeInfo_Day.holidayCount = TimeUtils.Substract(currentTradingDay, lastTradingDay).Days - 1;
            }
        }

        private KLineDataTradingTimeInfo_Day CalcKLineTimeInfo_Day(ITradingTime tradingTime, IKLineData klineData, int currentBarPos)
        {
            if (tradingTime.PeriodCount == 0)
                return null;
            KLineDataTradingTimeInfo_Day klineTimeInfo_Day = new KLineDataTradingTimeInfo_Day(tradingTime.TradingDay);

            for (int i = 0; i < tradingTime.PeriodCount; i++)
            {
                double[] periodTime = tradingTime.GetPeriodTime(i);
                int startPos = CalcEndBarPos(klineData, periodTime[0], currentBarPos);
                int endPos = CalcEndBarPos(klineData, periodTime[1], startPos + 1);
                currentBarPos = endPos + 1;
                KLineDataTradingTimeInfo_Periods timeInfo = new KLineDataTradingTimeInfo_Periods(i, startPos, endPos);
                klineTimeInfo_Day.AddTradingPeriods(timeInfo);
            }
            int dayStartPos = klineTimeInfo_Day.TradingPeriods[0].StartPos;
            int dayEndPos = klineTimeInfo_Day.TradingPeriods[klineTimeInfo_Day.TradingPeriods.Count - 1].EndPos;
            klineTimeInfo_Day.StartPos = dayStartPos;
            klineTimeInfo_Day.EndPos = dayEndPos;
            return klineTimeInfo_Day;
        }

        private int CalcEndBarPos(IKLineData klineData, double time, int currentBarPos)
        {
            int barPos = currentBarPos;
            while (barPos < klineData.Length)
            {
                double klineTime = klineData.Arr_Time[barPos];
                if (klineTime == time)
                    return barPos;
                if (klineTime > time)
                    return barPos - 1;
                barPos++;
            }
            return klineData.Length - 1;
        }

        public IKLineDataTradingTimeInfo_Day GetKLineTimeInfo_Day(int tradingDay)
        {
            if (!dic_TradingDay_KLineTimeInfo.ContainsKey(tradingDay))
                return null;
            return dic_TradingDay_KLineTimeInfo[tradingDay];
        }

        public IKLineDataTradingTimeInfo_Periods GetTradingPeriodsByBarPos(int barPos)
        {
            for (int i = 0; i < timeInfo_DayList.Count; i++)
            {
                IKLineDataTradingTimeInfo_Day timeInfo_Day = timeInfo_DayList[i];
                if (timeInfo_Day.StartPos <= barPos && timeInfo_Day.EndPos >= barPos)
                {
                    return timeInfo_Day.FindPeriods(barPos);
                }
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < timeInfo_DayList.Count; i++)
            {
                sb.Append(timeInfo_DayList[i].ToString());
            }
            return sb.ToString();
        }
    }

    public class KLineDataTradingTimeInfo_Day : IKLineDataTradingTimeInfo_Day
    {
        private int tradingDay;

        private int startPos;

        private int endPos;

        internal int holidayCount = -1;

        private List<IKLineDataTradingTimeInfo_Periods> tradingPeriodsArr = new List<IKLineDataTradingTimeInfo_Periods>();

        public int TradingDay
        {
            get
            {
                return tradingDay;
            }
        }

        public int BarCount
        {
            get { return endPos - startPos + 1; }
        }

        public int StartPos
        {
            get
            {
                return startPos;
            }
            set
            {
                startPos = value;
            }
        }

        public int EndPos
        {
            get
            {
                return endPos;
            }
            set
            {
                endPos = value;
            }
        }

        public IList<IKLineDataTradingTimeInfo_Periods> TradingPeriods
        {
            get
            {
                return tradingPeriodsArr;
            }
        }

        public int HolidayCount
        {
            get
            {
                return holidayCount;
            }
        }

        public KLineDataTradingTimeInfo_Day(int tradingDay)
        {
            this.tradingDay = tradingDay;
        }

        public IKLineDataTradingTimeInfo_Periods FindPeriods(int barPos)
        {
            for (int i = 0; i < TradingPeriods.Count; i++)
            {
                IKLineDataTradingTimeInfo_Periods periods = TradingPeriods[i];
                if (periods.StartPos <= barPos && periods.EndPos >= barPos)
                    return periods;
            }
            return null;
        }

        public void AddTradingPeriods(IKLineDataTradingTimeInfo_Periods tradingPeriods)
        {
            ((KLineDataTradingTimeInfo_Periods)tradingPeriods).klineTimeInfo_Day = this;
            this.tradingPeriodsArr.Add(tradingPeriods);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("TradingDay:");
            sb.Append(tradingDay).Append(",");
            sb.Append(startPos).Append(",");
            sb.Append(endPos).Append(",");
            sb.Append(holidayCount).Append("\r\n");
            for (int i = 0; i < tradingPeriodsArr.Count; i++)
            {
                IKLineDataTradingTimeInfo_Periods periods = tradingPeriodsArr[i];
                sb.Append(periods.ToString()).Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class KLineDataTradingTimeInfo_Periods : IKLineDataTradingTimeInfo_Periods
    {
        internal IKLineDataTradingTimeInfo_Day klineTimeInfo_Day;

        private int periodIndex;

        private int startPos;

        private int endPos;

        public int BarCount
        {
            get { return endPos - startPos + 1; }
        }

        public int StartPos
        {
            get
            {
                return startPos;
            }
        }

        public int EndPos
        {
            get
            {
                return endPos;
            }
        }

        public int PeriodIndex
        {
            get
            {
                return periodIndex;
            }
        }

        public IKLineDataTradingTimeInfo_Day KlineTimeInfo_Day
        {
            get
            {
                return klineTimeInfo_Day;
            }
        }

        public KLineDataTradingTimeInfo_Periods(int periodIndex, int startPos, int endPos)
        {
            this.periodIndex = periodIndex;
            this.startPos = startPos;
            this.endPos = endPos;
        }

        public override string ToString()
        {
            return "TradingPeriods:" + periodIndex + "," + startPos + "," + endPos;
        }
    }
}
