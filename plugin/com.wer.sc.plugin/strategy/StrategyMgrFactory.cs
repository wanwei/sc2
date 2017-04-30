using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyMgrFactory
    {
        public static IStrategyMgr CreatePluginMgr(string path)
        {
            StrategyMgr mgr = new StrategyMgr(path);
            mgr.Load();
            return mgr;
        }

        //private static IStrategyMgr CreateDefaultPluginMgr()
        //{

        //    //string dllPath = Assembly.GetExecutingAssembly().Location;
        //    //Console.WriteLine(dllPath);
        //    //FileInfo f = new FileInfo(dllPath);
        //    //string path = f.DirectoryName + "\\plugin\\";
        //    string path = ScConfig.Instance.ScPath + "\\strategy\\";
        //    //Console.WriteLine(path);
        //    return CreatePluginMgr(path);
        //}

        //private static IStrategyMgr pluginMgr = CreateDefaultPluginMgr();

        //public static IStrategyMgr DefaultPluginMgr
        //{
        //    get
        //    {
        //        return pluginMgr;
        //    }
        //}
    }
}
