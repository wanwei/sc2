using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginMgrFactory
    {
        public static IPluginMgr CreatePluginMgr(string path)
        {
            PluginMgr mgr = new PluginMgr(path);
            mgr.Load();
            return mgr;
        }

        private static IPluginMgr CreateDefaultPluginMgr()
        {            
            string path = ScConfig.Instance.ScPath + "\\plugin\\";
            return CreatePluginMgr(path);
        }

        private static IPluginMgr pluginMgr = CreateDefaultPluginMgr();

        public static IPluginMgr DefaultPluginMgr
        {
            get
            {
                return pluginMgr;
            }
        }
    }
}
