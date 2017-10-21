using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据中心
    /// </summary>
    public class DataCenter : IDataCenter
    {
        private DataCenterInfo config;

        private IDataStore dataStore;

        private IDataReader dataReader;

        private IDataPackageFactory dataPackageFactory;

        private IDataForwardFactory historyDataForwardFactory;

        private IDataNavigateFactory dataNavigateFactory;

        internal DataCenter(DataCenterInfo config, IDataStore dataStore, IDataReader dataReaderFactory)
        {
            this.config = config;
            this.dataStore = dataStore;
            this.dataReader = dataReaderFactory;
            this.dataPackageFactory = new DataPackageFactory(dataReader);
            this.historyDataForwardFactory = new DataForwardFactory(this);
            this.dataNavigateFactory = new DataNavigateFactory(dataReader);
        }

        public DataCenterInfo Config
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

        public static DataCenter Create(string uri)
        {
            return DataCenterManager.Instance.GetDataCenterByUri(uri);
        }

        public static DataCenter Create(DataCenterInfo dataCenterInfo)
        {
            string uristr = dataCenterInfo.Uri;
            Uri uri = new Uri(uristr);
            if (uri.IsFile)
            {
                IDataStore dataStore = DataStoreFactory.CreateDataStore(uri.LocalPath);
                IDataReader dataReader = DataReaderFactory.CreateDataReader(uri.LocalPath);
                return new DataCenter(dataCenterInfo, dataStore, dataReader);
            }
            return null;
        }

        private static DataCenter _default = DataCenterManager.Instance.GetDefaultDataCenter();

        /// <summary>
        /// 当前数据中心
        /// </summary>
        public static DataCenter Default
        {
            get
            {
                return _default;
            }
        }

        public IDataCenterInfo DataCenterInfo
        {
            get
            {
                return this.config;
            }
        }

        public IDataPackageFactory DataPackageFactory
        {
            get
            {
                return this.dataPackageFactory;
            }
        }

        public IDataNavigateFactory DataNavigateFactory
        {
            get
            {
                return dataNavigateFactory;
            }
        }

        public IDataForwardFactory HistoryDataForwardFactory
        {
            get
            {
                return historyDataForwardFactory;
            }
        }
    }
}