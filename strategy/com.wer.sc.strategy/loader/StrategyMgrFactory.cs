using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    public class StrategyMgrFactory
    {
        public static IStrategyAssemblyMgr CreatePluginMgr(string path)
        {
            StrategyMgr mgr = new StrategyMgr(path);
            mgr.Load();
            return mgr;
        }

        private static IStrategyAssemblyMgr CreateDefaultPluginMgr()
        {
            string path = ScConfig.Instance.ScPath + "\\strategy\\";
            return CreatePluginMgr(path);
        }

        private static IStrategyAssemblyMgr pluginMgr = CreateDefaultPluginMgr();

        internal static IStrategyAssemblyMgr DefaultPluginMgr
        {
            get
            {
                return pluginMgr;
            }
        }
    }
}
