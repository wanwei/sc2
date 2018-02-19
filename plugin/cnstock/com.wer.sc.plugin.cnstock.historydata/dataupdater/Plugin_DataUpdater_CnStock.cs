using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    [Plugin("CNSTOCK.DATAUPDATER.DAILY", "股票历史数据更新-每日更新", "股票历史数据更新", MarketType.CnStock)]
    public class Plugin_DataUpdater_CnStock :  IPlugin_DataUpdater
    {
        private const string SRCDATAPATH = "srcDataPath";
        private const string PLUGINSRCDATAPATH = "targetDataPath";
        private const string DATACENTERURI = "dataCenterUri";
        private const string UPDATEFILLUP = "updateFillUp";
        private UpdateStepGetter_CnStock stepGetter;

        protected Dictionary<string, object> dic = new Dictionary<string, object>();

        private List<PreparerArgument> args;

        protected PluginHelper pluginHelper;

        public Plugin_DataUpdater_CnStock(PluginHelper pluginHelper)
        {
        }

        public IUpdateHelper PluginHelper
        {
            get
            {
                if (stepGetter == null)
                    Init();
                return stepGetter;
            }
        }

        private void Init()
        {
            string srcDataPath = (string)GetValue(SRCDATAPATH);
            string pluginSrcDataPath = (string)GetValue(PLUGINSRCDATAPATH);
            //string dataCenterUri = (string)GetValue(DATACENTERURI);
            bool updateFillUp = (bool)GetValue(UPDATEFILLUP);

            args = new List<PreparerArgument>();
            args.Add(new PreparerArgument(SRCDATAPATH, "源数据目录", srcDataPath));
            args.Add(new PreparerArgument(PLUGINSRCDATAPATH, "目标目录", pluginSrcDataPath));
            args.Add(new PreparerArgument(UPDATEFILLUP, "补充历史数据？", updateFillUp.ToString()));
            this.stepGetter = new UpdateStepGetter_CnStock(pluginHelper, srcDataPath, pluginSrcDataPath, updateFillUp);
        }

        public List<PreparerArgument> GetAllArguments()
        {
            return args;
        }

        public void SetValue(string key, object value)
        {
            dic.Add(key, value);
        }

        public Object GetValue(string key)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            return null;
        }
    }
}
