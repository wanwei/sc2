using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class DataStore_File : IDataStore
    {
        private string dataCenterPath;

        private DataPathUtils dataPathUtils;

        public DataStore_File(string dataCenterPath)
        {
            this.dataCenterPath = dataCenterPath;
            this.dataPathUtils = new DataPathUtils(dataCenterPath);
        }

        public IUpdateInfoStore CreateUpdateInfoStore()
        {
            return new UpdateInfoStore_File(dataPathUtils.GetUpdateInfoPath());
        }

        public IInstrumentStore CreateInstrumentStore()
        {
            return new InstrumentStore_File(dataPathUtils.GetInstrumentPath());
        }

        public ITradingDayStore CreateTradingDayStore()
        {
            return new TradingDayStore_File(dataPathUtils.GetTradingDayPath());
        }

        public ITradingSessionStore CreateTradingSessionStore()
        {
            return new TradingSessionStore_File(dataPathUtils);
        }

        public IKLineDataStore CreateKLineDataStore()
        {
            return new KLineDataStore_File(dataPathUtils);
        }

        public ITickDataStore CreateTickDataStore()
        {
            return new TickDataStore_File(dataPathUtils);
        }

        public IAccountStore CreateAccountStore()
        {
            return new AccountStore_File(dataPathUtils);
        }
    }
}
