using com.wer.sc.data.reader.cache;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.impl
{
    public class DataReader : IDataReader
    {
        private IDataStore dataStore;
        private CodeReader instrumentReader;
        private ITradingDayReader tradingDayReader;
        private ITickDataReader tickDataReader;
        private IKLineDataReader klineDataReader;
        private ITimeLineDataReader timeLineDataReader;

        private ITradingSessionStore tradingSessionStore;

        public DataReader(string dataCenterUri)
        {
            this.dataStore = DataStoreFactory.CreateDataStore(dataCenterUri);
            this.instrumentReader = new CodeReader(dataStore.CreateInstrumentStore());
            this.tradingDayReader = new TradingDayCache(dataStore.CreateTradingDayStore().Load());
            this.tradingSessionStore = dataStore.CreateTradingSessionStore();
            this.tickDataReader = new TickDataReader(dataStore);
            this.klineDataReader = new KLineDataReader(dataStore);
            this.timeLineDataReader = new TimeLineDataReader(this);
        }

        public ICodeReader CodeReader
        {
            get
            {
                return instrumentReader;
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

        public ITradingSessionReader_Instrument CreateTradingSessionReader(string code)
        {
            List<TradingSession> sessions = tradingSessionStore.Load(code);
            TradingSessionCache_Instrument cache = new TradingSessionCache_Instrument(code, sessions);
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
