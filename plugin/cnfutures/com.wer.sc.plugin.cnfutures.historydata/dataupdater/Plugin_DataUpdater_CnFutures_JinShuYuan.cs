using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [Plugin("CNFUTURES.DATAUPDATER.JINSHUYUAN", "期货历史数据更新-金数源", "期货历史数据更新-金数源", MarketType.CnFutures)]
    public class Plugin_DataUpdater_CnFutures_JinShuYuan : Plugin_DataUpdater_CnFutures_Abstract, IPlugin_DataUpdater
    {
        private IDataProvider dataProvider;

        public Plugin_DataUpdater_CnFutures_JinShuYuan(PluginHelper pluginHelper) : base(pluginHelper)
        {
        }

        public override IDataProvider GetDataProvider(string srcDataPath)
        {
            if (dataProvider != null)
                return dataProvider;
            dataProvider = new DataProvider_JinShuYuan(srcDataPath, pluginHelper.PluginDirPath);
            return dataProvider;
        }

    }
}
