using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TradingTimeReader : ITradingTimeReader_Code
    {
        private string code;

        private ITradingTimeStore tradingSessionStore;

        private CacheUtils_TradingTime cache;

        public TradingTimeReader(string code, ITradingTimeStore tradingSessionStore)
        {
            this.code = code;
            this.tradingSessionStore = tradingSessionStore;
            this.cache = new CacheUtils_TradingTime(code, tradingSessionStore.Load(code));
        }

        public string GetCode()
        {
            return code;
        }

        public int GetRecentTradingDay(double time)
        {
            return cache.GetRecentTradingDay(time);
        }

        public int GetRecentTradingDay(double time, bool forward)
        {
            return cache.GetRecentTradingDay(time, forward);
        }

        public double GetRecentTradingTime(double time, bool findForward)
        {
            return -1;
        }

        public int GetTradingDay(double time)
        {
            return cache.GetTradingDay(time);
        }

        public ITradingTime GetTradingTime(int date)
        {
            return new TradingTime(date, GetTradingTime(code, date));
        }

        public IList<ITradingTime> GetTradingTime(int start, int end)
        {
            return null;
        }

        public bool IsStartTime(double time)
        {
            return cache.IsStartTime(time);
        }  

        private List<double[]> GetTradingTime(string code, int date)
        {
            return cache.GetTradingTime(code, date);
        }

        public ITradingDayReader GetTradingDayReader()
        {
            return cache.GetTradingDayReader();
        }
    }
}
