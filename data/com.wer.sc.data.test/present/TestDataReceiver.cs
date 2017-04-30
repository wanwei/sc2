using com.wer.sc.data.receiver2;
using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    [TestClass]
    public class TestDataReceiver
    {
        [TestMethod]
        public void TestReceive()
        {
            List<PluginInfo> plugins = PluginMgrFactory.DefaultPluginMgr.GetPlugins(typeof(IPlugin_Market));
            
            //plugins[0].PluginName
            
            //DataReceiver receiver = new DataReceiver()
        }
    }
}
