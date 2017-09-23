using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.appoint;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 指定日期更新：
    /// 1：2004-20160429 biaopuyonghua
    /// 2：20160503-20170331 JinShuYuan
    /// 3：20170401-     daily
    /// </summary>
    [Plugin("CNFUTURES.DATAUPDATER.APPOINT", "指定历史数据更新", "历史数据更新", MarketType.CnFutures)]
    public class Plugin_DataUpdater_CnFutures_Appoint : Plugin_DataUpdater_CnFutures_Abstract
    {
        private IDataProvider dataProvider_Appoint;

        public Plugin_DataUpdater_CnFutures_Appoint(PluginHelper pluginHelper) : base(pluginHelper)
        {
            //string srcDataPath1 = GetValue("srcDataPath1").ToString();
            string pluginPath = this.pluginHelper.PluginDirPath;

            this.dataProvider_Appoint = new DataProvider_Appoint(pluginHelper.PluginDirPath);
        }

        public override IDataProvider GetDataProvider(string srcDataPath)
        {
            return this.dataProvider_Appoint;
        }
    }

}
