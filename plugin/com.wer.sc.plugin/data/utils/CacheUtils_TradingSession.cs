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
    public class CacheUtils_TradingSession : ITradingSessionReader_Code
    {
        private string instrumentId;

        private CacheUtils_TradingDay tradingDayCache;

        private List<TradingSession> tradingSessionList;

        private List<int> tradingDays;

        private List<double> startTimes;

        private Dictionary<int, TradingSession> dic_TradingDay_TradingSession;

        private Dictionary<double, int> dic_StartTime_TradingDay;

        public CacheUtils_TradingSession(string instrumentId, List<TradingSession> tradingSessionList)
        {
            this.instrumentId = instrumentId;
            this.tradingSessionList = tradingSessionList;
            this.tradingDays = new List<int>(tradingSessionList.Count);
            this.startTimes = new List<double>(tradingSessionList.Count);
            this.dic_TradingDay_TradingSession = new Dictionary<int, TradingSession>(tradingSessionList.Count);
            this.dic_StartTime_TradingDay = new Dictionary<double, int>(tradingSessionList.Count);
            for (int i = 0; i < tradingSessionList.Count; i++)
            {
                TradingSession tradingSession = tradingSessionList[i];
                this.tradingDays.Add(tradingSession.TradingDay);
                this.startTimes.Add(tradingSession.StartTime);
                this.dic_TradingDay_TradingSession.Add(tradingSession.TradingDay, tradingSession);
                this.dic_StartTime_TradingDay.Add(tradingSession.StartTime, tradingSession.TradingDay);
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
            TradingSession tradingSession = GetTradingSession(tradingDay);
            if (tradingSession == null)
                return false;            
            return time >= tradingSession.StartTime && time <= tradingSession.EndTime;
        }

        private bool IsTimeInThisDay(int date, double time)
        {
            if (date < 0)
                return false;
            Console.WriteLine(date + "," + time);
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
            TradingSession value;
            bool exist = dic_TradingDay_TradingSession.TryGetValue(date, out value);
            return exist ? value.StartTime : -1;
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

        public TradingSession GetTradingSession(int date)
        {
            if (dic_TradingDay_TradingSession.ContainsKey(date))
                return dic_TradingDay_TradingSession[date];
            return null;
        }

        public int GetRecentTradingDay(double time)
        {
            return GetRecentTradingDay(time, true);
        }

        public int GetRecentTradingDay(double time, bool forward)
        {
            if (dic_StartTime_TradingDay.ContainsKey(time))
                return dic_StartTime_TradingDay[time];
            int tradingDay = (int)time;
            TradingSession tradingSession = GetTradingSession(tradingDay);
            if (tradingSession != null)
            {
                if (time >= tradingSession.StartTime && time <= tradingSession.EndTime)
                    return tradingDay;
                if (forward)
                {
                    if (time > tradingSession.EndTime)
                        return GetTradingDayCache().GetNextTradingDay(tradingDay);
                    return tradingDay;
                }
                else
                {
                    if (time < tradingSession.StartTime)
                        return GetTradingDayCache().GetPrevTradingDay(tradingDay);
                    return tradingDay;
                }
            }

            if (forward)
            {
                return GetTradingDayCache().GetNextTradingDay(tradingDay);
            }

            int nextTradingDay = GetTradingDayCache().GetNextTradingDay(tradingDay);
            if (IsTimeInThisDay(nextTradingDay, time))
                return nextTradingDay;

            return GetTradingDayCache().GetPrevTradingDay(tradingDay);
        }
    }
}
