﻿using com.wer.sc.data.store;
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
        private ITradingTimeStore tradingTimeStore;
        private IMainContractReader mainContractReader;

        public DataReader(string dataCenterUri)
        {
            this.dataStore = DataStoreFactory.CreateDataStore(dataCenterUri);
            this.codeReader = new CodeReader(dataStore.CreateInstrumentStore());
            this.tradingDayReader = new CacheUtils_TradingDay(dataStore.CreateTradingDayStore().Load());
            this.tradingSessionStore = dataStore.CreateTradingSessionStore();
            this.tradingTimeStore = dataStore.CreateTradingTimeStore();
            this.tickDataReader = new TickDataReader(dataStore, this);
            this.klineDataReader = new KLineDataReader(dataStore, this);
            this.timeLineDataReader = new TimeLineDataReader(this);
            this.mainContractReader = new MainContractReader(dataStore);
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
            return CreateTradingTimeReader(code).GetTradingDayReader();
        }

        public ITradingTimeReader_Code CreateTradingTimeReader(string code)
        {
            IList<ITradingTime> sessions = tradingTimeStore.Load(code);
            CacheUtils_TradingTime cache = new CacheUtils_TradingTime(code, sessions);
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

        public IMainContractReader MainContractReader
        {
            get
            {
                return mainContractReader;
            }
        }
    }
}
