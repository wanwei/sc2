using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;
using System.Configuration;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [Plugin("CNFUTURES.DATAUPDATER.BIAOPUYONGHUA", "期货历史数据更新", "期货历史数据更新", MarketType.CnFutures)]
    //[Plugin("HISTORYDATA.CNFUTURES", "中国期货市场", "提供中国期货市场的各种数据，包括大连、上期、郑州、中金四个期货交易所", MarketType.CnFutures)]
    public class Plugin_DataUpdater_CnFutures_BiaoPuYongHua : IPlugin_DataUpdater
    {
        private const string SRCDATAPATH = "srcDataPath";
        private const string PLUGINSRCDATAPATH = "targetDataPath";
        private const string DATACENTERURI = "dataCenterUri";
        private const string UPDATEFILLUP = "updateFillUp";
        private UpdateStepGetter_CnFutures stepGetter;

        protected Dictionary<string, object> dic = new Dictionary<string, object>();

        private List<PreparerArgument> args;

        private PluginHelper pluginHelper;

        public Plugin_DataUpdater_CnFutures_BiaoPuYongHua(PluginHelper pluginHelper)
        {
            this.pluginHelper = pluginHelper;
        }

        public IUpdateStepGetter UpdateStepGetter
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
            string dataCenterUri = (string)GetValue(DATACENTERURI);
            bool updateFillUp = (bool)GetValue(UPDATEFILLUP);
            this.stepGetter = new UpdateStepGetter_CnFutures(pluginHelper, srcDataPath, pluginSrcDataPath, dataCenterUri, updateFillUp);

            args = new List<PreparerArgument>();
            args.Add(new PreparerArgument(SRCDATAPATH, "源数据目录", srcDataPath));
            args.Add(new PreparerArgument(PLUGINSRCDATAPATH, "目标目录", pluginSrcDataPath));
            args.Add(new PreparerArgument(DATACENTERURI, "数据中心地址", dataCenterUri));
            args.Add(new PreparerArgument(UPDATEFILLUP, "补充历史数据？", updateFillUp.ToString()));
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