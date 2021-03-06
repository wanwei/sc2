﻿using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class TradingTimeStore_File : ITradingTimeStore
    {
        private DataPathUtils dataPathUtils;

        public TradingTimeStore_File(DataPathUtils dataPathUtils)
        {
            this.dataPathUtils = dataPathUtils;
        }

        public void Save(string code, IList<ITradingTime> tradingTimes)
        {
            if (tradingTimes == null)
                return;
            string path = dataPathUtils.GetTradingTimePath(code);            
            TextExchangeUtils.Write<ITradingTime>(path, tradingTimes);
        }

        public IList<ITradingTime> Load(string code)
        {
            string path = dataPathUtils.GetTradingTimePath(code);
            return TextExchangeUtils.Load<ITradingTime>(path, typeof(TradingTime));
        }

        public void Delete(string code)
        {
            string path = dataPathUtils.GetTradingTimePath(code);
            File.Delete(path);
        }      
    }
}
