using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class DataStore_File : IDataStore
    {
        private IDataCenter dataCenter;
        private string dataCenterPath;

        private DataPathUtils dataPathUtils;

        public DataStore_File(IDataCenter dataCenter, string dataCenterPath)
        {
            this.dataCenter = dataCenter;
            //this.dataCenterPath = dataCenterPath;
            this.dataPathUtils = new DataPathUtils(dataCenterPath);
        }

        public IUpdateInfoStore CreateUpdateInfoStore()
        {
            return new UpdateInfoStore_File(dataPathUtils.GetUpdateInfoPath());
        }

        public ICodeStore CreateInstrumentStore()
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

        public ITradingTimeStore CreateTradingTimeStore()
        {
            return new TradingTimeStore_File(dataPathUtils);
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
            return new AccountStore_File(dataCenter, dataPathUtils);
        }

        public IMainContractStore CreateMainContractStore()
        {
            return new MainContractStore_File(dataPathUtils);
        }
    }
}
