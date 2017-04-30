using com.wer.sc.data.cache.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache
{
    public class DataCacheFactory
    {
        private DataReaderFactory dataReaderFactory;

        public DataCacheFactory(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public IDataCache_Code CreateCache_Code(String code)
        {
            return new DataCache_Code(dataReaderFactory, code);
        }

        public IDataCache_Code CreateCache_Code(String code, int startDate, int endDate)
        {
            return new DataCache_Code(dataReaderFactory, code, startDate, endDate);
        }

        public IDataCache_Date CreateCache_Date(String date)
        {
            return null;
        }

        public IDataCache_Date CreateCache_Date(String date, List<String> codes)
        {
            return null;
        }
    }
}
