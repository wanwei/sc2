using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;
using com.wer.sc.data.update;
using com.wer.sc.data.datacenter;

namespace com.wer.sc.data.updater
{
    public class Plugin_DataUpdater : IPlugin_DataUpdater
    {
        private const string SRCDATAPATH = "srcDataPath";
        private const string PLUGINSRCDATAPATH = "pluginSrcDataPath";
        private const string DATACENTERURI = "dataCenterUri";
        private const string UPDATEFILLUP = "updateFillUp";

        private IUpdateStepGetter updateStepGetter;

        private List<PreparerArgument> args;

        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public Plugin_DataUpdater()
        {
        }


        public IUpdateStepGetter UpdateStepGetter
        {
            get
            {
                if (updateStepGetter == null)
                    Init();
                return updateStepGetter;
            }
        }

        private void Init()
        {
            string srcDataPath = (string)GetValue(SRCDATAPATH);
            string dataCenterUri = (string)GetValue(DATACENTERURI);
            bool isFillUp = (bool)GetValue(UPDATEFILLUP);

            IPlugin_HistoryData plugin_HistoryData = new Plugin_HistoryData_Default(srcDataPath);
            //IPlugin_HistoryData plugin_HistoryData = null;// = new Plugin_DataUpdater();
            DataCenter dataCenter = DataCenterManager.Instance.GetDataCenter(dataCenterUri);
            this.updateStepGetter = new DataUpdate(plugin_HistoryData, dataCenter, isFillUp);
            //this.stepGetter = new DataUpdate(plugin_HistoryData, dataCenter, isFillUp);

            args = new List<PreparerArgument>();
            args.Add(new PreparerArgument(SRCDATAPATH, "源数据目录", srcDataPath));
            args.Add(new PreparerArgument(DATACENTERURI, "数据中心地址", dataCenterUri));
            args.Add(new PreparerArgument(UPDATEFILLUP, "补充历史数据？", isFillUp.ToString()));
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