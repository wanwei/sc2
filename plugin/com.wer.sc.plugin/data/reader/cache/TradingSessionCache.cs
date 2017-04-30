using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.cache
{
    /// <summary>
    /// 开盘时间的缓存实现
    /// </summary>
    public class TradingSessionCache : ITradingSessionReader_Instrument
    {
        private ITradingDayReader openDateCache;

        private List<TradingSession> dayStartTimes;

        private List<int> openDates;

        private List<double> startTimes;

        private Dictionary<int, double> dicStartTimes;

        private Dictionary<double, int> dicOpenDates;

        public TradingSessionCache(List<TradingSession> dayStartTimes)
        {
            this.dayStartTimes = dayStartTimes;
            this.openDates = new List<int>(dayStartTimes.Count);
            this.startTimes = new List<double>(dayStartTimes.Count);
            this.dicStartTimes = new Dictionary<int, double>(dayStartTimes.Count);
            this.dicOpenDates = new Dictionary<double, int>(dayStartTimes.Count);
            for (int i = 0; i < dayStartTimes.Count; i++)
            {
                TradingSession startTime = dayStartTimes[i];
                this.openDates.Add(startTime.TradingDay);
                this.startTimes.Add(startTime.StartTime);
                this.dicStartTimes.Add(startTime.TradingDay, startTime.StartTime);
                this.dicOpenDates.Add(startTime.StartTime, startTime.TradingDay);
            }
        }

        public List<double> GetAllStartTimes()
        {
            return startTimes;
        }

        public ITradingDayReader GetOpenDateCache()
        {
            if (openDateCache == null)
                openDateCache = new TradingDayCache(openDates);
            return openDateCache;
        }

        public int GetTradingDay(double time)
        {
            int openDate = GetOpenDate2(time);
            if (openDate >= 0)
                return openDate;

            int date = (int)time;
            if (IsTimeInThisDay(date, time))
                return date;
            int nextdate = GetOpenDateCache().GetNextOpenDate(date);
            if (IsTimeInThisDay(nextdate, time))
                return nextdate;
            int prevdate = GetOpenDateCache().GetPrevOpenDate(date);
            if (IsTimeInThisDay(prevdate, time))
                return prevdate;
            return -1;
        }

        private bool IsTimeInThisDay(int date, double time)
        {
            if (date < 0)
                return false;
            double todayStartTime = GetStartTime(date);
            double nextStartTime = GetStartTime(GetOpenDateCache().GetNextOpenDate(date));
            return (time > todayStartTime && time < nextStartTime);
        }

        private int GetOpenDate2(double startTime)
        {
            int value;
            bool exist = dicOpenDates.TryGetValue(startTime, out value);
            return exist ? value : -1;
        }

        public bool IsStartTime(double time)
        {
            return dicOpenDates.Keys.Contains(time);
        }

        public double GetStartTime(int date)
        {
            double value;
            bool exist = dicStartTimes.TryGetValue(date, out value);
            return exist ? value : -1;
        }

        public List<double> GetStartTimes(int startDate)
        {
            ITradingDayReader openDateCache = GetOpenDateCache();
            int startIndex = openDateCache.GetOpenDateIndex(startDate);
            int count = openDates.Count - startIndex;
            return startTimes.GetRange(startIndex, count);
        }

        public List<double> GetStartTimes(int startDate, int endDate)
        {
            ITradingDayReader openDateCache = GetOpenDateCache();
            int startIndex = openDateCache.GetOpenDateIndex(startDate);
            int endIndex = openDateCache.GetOpenDateIndex(endDate);
            return startTimes.GetRange(startIndex, endIndex - startIndex + 1);
        }

        public string GetInstrument()
        {
            throw new NotImplementedException();
        }

        public TradingSession GetTradingSession(int date)
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time)
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time, bool forward)
        {
            throw new NotImplementedException();
        }
    }
}
