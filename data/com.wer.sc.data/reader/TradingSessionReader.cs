using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TradingSessionReader : ITradingSessionReader_Code
    {
        private string code;

        private ITradingSessionStore tradingSessionStore;

        private CacheUtils_TradingSession cache;

        public TradingSessionReader(string code, ITradingSessionStore tradingSessionStore)
        {
            this.code = code;
            this.tradingSessionStore = tradingSessionStore;
            this.cache = new CacheUtils_TradingSession(code, tradingSessionStore.Load(code));
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

        public int GetTradingDay(double time)
        {
            return cache.GetTradingDay(time);
        }

        public TradingSession GetTradingSession(int date)
        {
            throw new NotImplementedException();
        }

        public bool IsStartTime(double time)
        {
            throw new NotImplementedException();
        }

    }
}
