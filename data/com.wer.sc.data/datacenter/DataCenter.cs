using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datacenter
{
    /// <summary>
    /// 数据中心
    /// </summary>
    public class DataCenter
    {
        private DataCenterConfig config;

        private IDataStore dataStore;

        private IDataReader dataReader;

        internal DataCenter(DataCenterConfig config, IDataStore dataStore, IDataReader dataReaderFactory)
        {
            this.config = config;
            this.dataStore = dataStore;
            this.dataReader = dataReaderFactory;
        }

        public DataCenterConfig Config
        {
            get
            {
                return config;
            }
        }

        public IDataStore DataStore
        {
            get
            {
                return dataStore;
            }
        }

        public IDataReader DataReader
        {
            get
            {
                return dataReader;
            }            
        }
    }
}