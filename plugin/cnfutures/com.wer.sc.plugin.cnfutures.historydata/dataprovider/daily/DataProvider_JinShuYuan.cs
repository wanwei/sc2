using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily
{
    public class DataProvider_JinShuYuan : IDataProvider
    {
        private DataProvider_JinShuYuan_TickData dataLoader_TickData;

        private DataProvider_JinShuYuan_TradingDay dataLoader_TradingDay;

        public DataProvider_JinShuYuan(string srcDataPath, string pluginPath)
        {
            this.dataLoader_TradingDay = new DataProvider_JinShuYuan_TradingDay(srcDataPath);
            this.dataLoader_TickData = new DataProvider_JinShuYuan_TickData(srcDataPath, pluginPath);
        }

        public ITickData LoadTickData(string code, int date)
        {
            return dataLoader_TickData.GetTickData(code, date);
        }

        public ITradingDayReader LoadTradingDayReader()
        {
            return dataLoader_TradingDay.GetTradingDayReader();
        }
    }
}
