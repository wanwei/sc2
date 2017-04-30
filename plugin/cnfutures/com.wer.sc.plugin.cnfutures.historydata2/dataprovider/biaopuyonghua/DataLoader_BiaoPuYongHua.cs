using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.biaopuyonghua
{
    public class DataProvider_BiaoPuYongHua : DataLoader_Abstract
    {
        private DataLoader_TickData dataLoader_TickData;

        private DataLoader_TradingDay dataLoader_TradingDay;

        public DataProvider_BiaoPuYongHua(PluginHelper pluginHelper, string srcDataPath) : base(pluginHelper, srcDataPath)
        {
            this.dataLoader_TradingDay = new DataLoader_TradingDay(srcDataPath);
            this.dataLoader_TickData = new DataLoader_TickData(srcDataPath, this);
        }

        public override ITickData LoadTickData(string code, int date)
        {
            return this.dataLoader_TickData.GetTickData(code, date);
        }

        public override ITradingDayReader LoadTradingDayReader()
        {
            return this.dataLoader_TradingDay.GetTradingDayReader();
        }
    }
}
