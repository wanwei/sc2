using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache
{
    public class DataCache : IDataReader
    {
        public ICodeReader CodeReader
        {
            get
            {
                return null;
            }
        }

        public IKLineDataReader KLineDataReader
        {
            get
            {
                return null;
            }
        }

        public ITickDataReader TickDataReader
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITimeLineDataReader TimeLineDataReader
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITradingDayReader TradingDayReader
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //public ITradingSessionReader_Code CreateTradingSessionReader(string code)
        //{
        //    throw new NotImplementedException();
        //}

        public ITradingTimeReader_Code CreateTradingTimeReader(string code)
        {
            throw new NotImplementedException();
        }

        public ITradingDayReader GetTradingDayReader(string code)
        {
            throw new NotImplementedException();
        }
    }
}
