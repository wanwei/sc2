using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class DataReader : IDataReader
    {
        private IDataStore dataStore;
        private ICodeReader codeReader;
        private ITradingDayReader tradingDayReader;
        private ITickDataReader tickDataReader;
        private IKLineDataReader klineDataReader;
        private ITimeLineDataReader timeLineDataReader;
        private ITradingSessionStore tradingSessionStore;

        public DataReader(string dataCenterUri)
        {
            this.dataStore = DataStoreFactory.CreateDataStore(dataCenterUri);
            this.codeReader = new CodeReader(dataStore.CreateInstrumentStore());
            this.tradingDayReader = new CacheUtils_TradingDay(dataStore.CreateTradingDayStore().Load());
            this.tradingSessionStore = dataStore.CreateTradingSessionStore();
            this.tickDataReader = new TickDataReader(dataStore);
            this.klineDataReader = new KLineDataReader(dataStore, this);
            this.timeLineDataReader = new TimeLineDataReader(this);
        }

        public ICodeReader CodeReader
        {
            get
            {
                return codeReader;
            }
        }

        public ITradingDayReader TradingDayReader
        {
            get
            {
                return tradingDayReader;
            }
        }

        public ITradingDayReader GetTradingDayReader(string code)
        {
            throw new NotImplementedException();
        }

        public ITradingSessionReader_Code CreateTradingSessionReader(string code)
        {
            List<TradingSession> sessions = tradingSessionStore.Load(code);
            CacheUtils_TradingSession cache = new CacheUtils_TradingSession(code, sessions);
            return cache;
        }

        public IKLineDataReader KLineDataReader
        {
            get
            {
                return klineDataReader;
            }
        }

        public ITickDataReader TickDataReader
        {
            get
            {
                return tickDataReader;
            }
        }

        public ITimeLineDataReader TimeLineDataReader
        {
            get
            {
                return timeLineDataReader;
            }
        }
    }
}
