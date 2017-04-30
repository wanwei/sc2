using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.present
{
    [TestClass]
    public class TestKLineDataTimePresent
    {
        [TestMethod]
        public void TestKLineDataTime()
        {
            IPluginMgr mgr = PluginMgrFactory.DefaultPluginMgr;
            PluginInfo pluginInfo = mgr.GetPlugin("CnFutures");
            IPlugin_HistoryData plugin = PluginMgrFactory.DefaultPluginMgr.CreatePluginObject<IPlugin_HistoryData>(pluginInfo);
            
            List<TradingSession> dayOpen = plugin.GetTradingSessions("m05");
            //List<double[]> openTimes = null;
            //OpenTimePeriodUtils.GetKLineTimeList(1, 1, openTimes, KLinePeriod.KLinePeriod_1Minute);
        }
    }
}
