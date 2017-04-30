using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    public class PathUtils
    {
        private string pluginPath;

        private string configPath = ScConfig.Instance.ScPath + "\\config\\";

        public PathUtils(String pluginPath)
        {
            this.pluginPath = pluginPath;
            this.configPath = pluginPath + "\\config\\";
        }

        //private static PathUtils instance_RunInPluginPath = new PathUtils(ScConfig.Instance.ScPath + "\\config\\");

        //public static PathUtils Instance_RunInPluginPath
        //{
        //    get { return instance_RunInPluginPath; }
        //}

        //private static PathUtils instance_RunInBin = new PathUtils(ScConfig.Instance.ScPath + "\\plugin\\cnfutures\\config\\");

        //public static PathUtils Instance_RunInBin
        //{
        //    get { return instance_RunInBin; }
        //}

        public string InstrumentPath
        {
            get { return configPath + "\\instruments.csv"; }
        }

        public string CatelogPath
        {
            get { return configPath + "\\catelogs.csv"; }
        }

        public string TradingSessionDetailPath
        {
            get { return configPath + "\\tradingsessiondetail.config"; }
        }

        public static String CTPPath
        {
            get { return ""; }
        }
    }
}
