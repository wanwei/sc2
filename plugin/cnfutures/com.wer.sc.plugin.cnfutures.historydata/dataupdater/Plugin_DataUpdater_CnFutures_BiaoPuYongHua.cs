using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;
using System.Configuration;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [Plugin("CNFUTURES.DATAUPDATER.BIAOPUYONGHUA", "期货历史数据更新-标普永华", "期货历史数据更新-标普永华，包括大连、上期、郑州三个商品期货交易所", MarketType.CnFutures)]
    public class Plugin_DataUpdater_CnFutures_BiaoPuYongHua : Plugin_DataUpdater_CnFutures_Abstract, IPlugin_DataUpdater
    {
        private IDataProvider dataProvider;

        public Plugin_DataUpdater_CnFutures_BiaoPuYongHua(PluginHelper pluginHelper) : base(pluginHelper)
        {
        }

        public override IDataProvider GetDataProvider(string srcDataPath)
        {
            if (dataProvider != null)
                return dataProvider;
            dataProvider = new DataProvider_BiaoPuYongHua(srcDataPath, pluginHelper.PluginDirPath);
            return dataProvider;
        }
    }
}