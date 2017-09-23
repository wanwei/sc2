using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan
{
    public class DataProvider_JinShuYuan : IDataProvider
    {
        private DataProvider_JinShuYuan_CodeInfo dataLoader_CodeInfo;

        private DataProvider_JinShuYuan_TickData dataLoader_TickData;

        private DataProvider_JinShuYuan_TradingDay dataLoader_TradingDay;

        public DataProvider_JinShuYuan(string srcDataPath, string pluginPath)
        {
            this.dataLoader_CodeInfo = new DataProvider_JinShuYuan_CodeInfo(srcDataPath, pluginPath);
            this.dataLoader_TradingDay = new DataProvider_JinShuYuan_TradingDay(srcDataPath);
            this.dataLoader_TickData = new DataProvider_JinShuYuan_TickData(srcDataPath, pluginPath);
        }

        public List<CodeInfo> GetNewCodes()
        {
            return dataLoader_CodeInfo.GetCodeInfo();
        }

        public ITickData LoadTickData(string code, int date)
        {
            return dataLoader_TickData.GetTickData(code, date);
        }

        public List<int> GetNewTradingDays()
        {
            return dataLoader_TradingDay.GetTradingDays();
        }

        public List<AppointUpdate> GetAppointUpdate()
        {
            return null;
        }
    }
}