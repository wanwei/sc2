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
        //private string pluginPath;

        private string configPath;// = ScConfig.Instance.ScPath + "\\config\\";

        public PathUtils(String pluginPath)
        {
            if (pluginPath == null || pluginPath.Equals("") || (!pluginPath.EndsWith("\\")))
                this.configPath = pluginPath + "config\\";
            else
                this.configPath = pluginPath + "\\config\\";
        }

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

        //public static String CTPPath
        //{
        //    get { return ""; }
        //}
    }
}