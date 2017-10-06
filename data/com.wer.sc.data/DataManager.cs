using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;

namespace com.wer.sc.data
{
    public class DataManager : IDataCenter
    {
        private string uri;

        private IDataReader dataReader;

        private IDataNavigateFactory dataNavigateFactory;

        DataManager(string dataCenterUri)
        {
            this.dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
        }

        public IDataNavigateFactory GetDataNavigateFactory()
        {
            return null;
        }

        public IDataReader GetDataReader()
        {
            return dataReader;
        }

        public IHistoryDataForwardFactory GetHistoryDataForward()
        {
            return null;
        }

        private static DataManager _instance;

        public static DataManager Instance
        {
            get
            {
                //if(_instance==null)
                //    _instance = 
                return _instance;
            }
        }

        public static DataManager Login(string dataCenterUri)
        {
            _instance = new DataManager(dataCenterUri);
            return _instance;
        }
    }
}