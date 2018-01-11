using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 开盘时间的缓存实现
    /// </summary>
    public class CacheUtils_TradingTime : ITradingTimeReader_Code
    {
        private string instrumentId;

        private CacheUtils_TradingDay tradingDayCache;

        private IList<TradingTime> tradingSessionList;

        private List<int> tradingDays;

        private List<double> startTimes;

        private Dictionary<int, TradingTime> dic_TradingDay_TradingSession;

        private Dictionary<double, int> dic_StartTime_TradingDay;

        public CacheUtils_TradingTime(string instrumentId, IList<TradingTime> tradingSessionList)
        {
            this.instrumentId = instrumentId;
            this.tradingSessionList = tradingSessionList;
            this.tradingDays = new List<int>(tradingSessionList.Count);
            this.startTimes = new List<double>(tradingSessionList.Count);
            this.dic_TradingDay_TradingSession = new Dictionary<int, TradingTime>(tradingSessionList.Count);
            this.dic_StartTime_TradingDay = new Dictionary<double, int>(tradingSessionList.Count);
            for (int i = 0; i < tradingSessionList.Count; i++)
            {
                TradingTime tradingSession = tradingSessionList[i];
                this.tradingDays.Add(tradingSession.TradingDay);
                this.startTimes.Add(tradingSession.OpenTime);
                this.dic_TradingDay_TradingSession.Add(tradingSession.TradingDay, tradingSession);
                this.dic_StartTime_TradingDay.Add(tradingSession.OpenTime, tradingSession.TradingDay);
            }
        }

        private ITradingDayReader GetTradingDayCache()
        {
            if (tradingDayCache == null)
                tradingDayCache = new CacheUtils_TradingDay(tradingDays);
            return tradingDayCache;
        }

        public int GetTradingDay(double time)
        {
            if (dic_StartTime_TradingDay.ContainsKey(time))
                return dic_StartTime_TradingDay[time];

            int tradingDay = (int)time;
            if (IsTimeInTradingSession(tradingDay, time))
                return tradingDay;

            int nextdate = GetTradingDayCache().GetNextTradingDay(tradingDay);
            if (IsTimeInTradingSession(nextdate, time))
                return nextdate;

            int prevdate = GetTradingDayCache().GetPrevTradingDay(tradingDay);
            if (IsTimeInTradingSession(prevdate, time))
                return prevdate;
            return -1;
        }

        private bool IsTimeInTradingSession(int tradingDay, double time)
        {
            if (tradingDay < 0)
                return false;
            ITradingTime tradingSession = GetTradingTime(tradingDay);
            if (tradingSession == null)
                return false;
            return time >= tradingSession.OpenTime && time <= tradingSession.CloseTime;
        }

        private bool IsTimeInThisDay(int date, double time)
        {
            if (date < 0)
                return false;
            //Console.WriteLine(date + "," + time);
            double todayStartTime = GetStartTime(date);
            if (todayStartTime < 0)
                return false;
            double nextStartTime = GetStartTime(GetTradingDayCache().GetNextTradingDay(date));
            return (time > todayStartTime && time < nextStartTime);
        }

        private int GetTradingDayInternal(double startTime)
        {
            int value;
            bool exist = dic_StartTime_TradingDay.TryGetValue(startTime, out value);
            return exist ? value : -1;
        }

        public bool IsStartTime(double time)
        {
            return dic_StartTime_TradingDay.Keys.Contains(time);
        }

        public double GetStartTime(int date)
        {
            TradingTime value;
            bool exist = dic_TradingDay_TradingSession.TryGetValue(date, out value);
            return exist ? value.OpenTime : -1;
        }

        public List<double> GetStartTimes(int startDate)
        {
            ITradingDayReader openDateCache = GetTradingDayCache();
            int startIndex = openDateCache.GetTradingDayIndex(startDate);
            int count = tradingDays.Count - startIndex;
            return startTimes.GetRange(startIndex, count);
        }

        public List<double> GetStartTimes(int startDate, int endDate)
        {
            ITradingDayReader openDateCache = GetTradingDayCache();
            int startIndex = openDateCache.GetTradingDayIndex(startDate);
            int endIndex = openDateCache.GetTradingDayIndex(endDate);
            return startTimes.GetRange(startIndex, endIndex - startIndex + 1);
        }

        public string GetCode()
        {
            return instrumentId;
        }

        public ITradingDayReader GetTradingDayReader()
        {
            return GetTradingDayCache();
        }

        public ITradingTime GetTradingTime(int date)
        {
            if (dic_TradingDay_TradingSession.ContainsKey(date))
                return dic_TradingDay_TradingSession[date];
            return null;
        }

        public IList<ITradingTime> GetTradingTime(int start, int end)
        {            
            IList<int> tradingDays = GetTradingDayCache().GetTradingDays(start, end);
            List<ITradingTime> tradingTimeArr = new List<ITradingTime>();
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                tradingTimeArr.Add(GetTradingTime(tradingDay));
            }
            return tradingTimeArr;
        }

        public int GetRecentTradingDay(double time)
        {
            return GetRecentTradingDay(time, false);
        }

        public int GetRecentTradingDay(double time, bool findForward)
        {
            if (dic_StartTime_TradingDay.ContainsKey(time))
                return dic_StartTime_TradingDay[time];
            int tradingDay = (int)time;
            if (tradingDay > this.tradingDays.Last<int>() && !findForward)
            {
                return this.tradingDays[tradingDays.Count - 1];
            }
            if (tradingDay < this.tradingDays[0] && findForward)
            {
                return this.tradingDays[0];
            }
            ITradingTime tradingTime = GetTradingTime(tradingDay);
            if (tradingTime != null)
            {
                if (time >= tradingTime.OpenTime && time <= tradingTime.CloseTime)
                    return tradingDay;
                if (time > tradingTime.CloseTime)
                {
                    int nTradingDay = GetTradingDayCache().GetNextTradingDay(tradingDay);
                    ITradingTime ntradingTime = GetTradingTime(nTradingDay);
                    if (ntradingTime != null && time >= ntradingTime.OpenTime && time <= ntradingTime.CloseTime)
                        return nTradingDay;
                }

                if (findForward)
                {
                    if (time > tradingTime.CloseTime)
                        return GetTradingDayCache().GetNextTradingDay(tradingDay);
                    return tradingDay;
                }
                else
                {
                    if (time < tradingTime.OpenTime)
                        return GetTradingDayCache().GetPrevTradingDay(tradingDay);
                    return tradingDay;
                }
            }

            if (findForward)
            {
                return GetTradingDayCache().GetNextTradingDay(tradingDay);
            }

            int nextTradingDay = GetTradingDayCache().GetNextTradingDay(tradingDay);
            if (IsTimeInThisDay(nextTradingDay, time))
                return nextTradingDay;

            return GetTradingDayCache().GetPrevTradingDay(tradingDay);
        }

        public double GetRecentTradingTime(double time, bool findForward)
        {
            int tradingDay = GetRecentTradingDay(time, findForward);
            if (tradingDay < 0)
                return -1;
            ITradingTime tradingTime = GetTradingTime(tradingDay);
            if (tradingTime.OpenTime <= time && tradingTime.CloseTime >= time)
            {
                double[] timeArr = tradingTime.GetPeriodTime(0);
                if (timeArr[0] <= time && timeArr[1] >= time)
                    return time;
                for (int i = 1; i < tradingTime.PeriodCount; i++)
                {
                    timeArr = tradingTime.GetPeriodTime(i);
                    if (timeArr[0] > time)
                        return tradingTime.GetPeriodTime(i - 1)[1];
                    if (timeArr[0] <= time && timeArr[1] >= time)
                        return time;
                }
                return tradingTime.CloseTime;
            }
            else
            {
                if (findForward)
                {
                    return tradingTime.OpenTime;
                }
                else
                {
                    return tradingTime.CloseTime;
                }
            }
        }

        public List<double[]> GetTradingTime(string code, int date)
        {
            throw new NotImplementedException();
        }
    }
}
