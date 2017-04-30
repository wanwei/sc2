using com.wer.sc.data.reader.cache;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.impl
{
    public class TradingSessionReader : ITradingSessionReader_Instrument
    {
        private string code;

        private ITradingSessionStore tradingSessionStore;

        private TradingSessionCache_Instrument cache;

        public TradingSessionReader(string code, ITradingSessionStore tradingSessionStore)
        {
            this.code = code;
            this.tradingSessionStore = tradingSessionStore;
            this.cache = new TradingSessionCache_Instrument(code, tradingSessionStore.Load(code));
        }

        public string GetInstrument()
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
