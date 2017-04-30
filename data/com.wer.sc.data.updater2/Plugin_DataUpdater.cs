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

        private Dictionary<string, object> dic = new Dictionary<string, object>(); 

        public Plugin_DataUpdater()
        {
            IPlugin_HistoryData plugin_HistoryData = null;
            DataCenter dataCenter = null;
            bool isFillUp = false;
            this.updateStepGetter = new DataUpdate(plugin_HistoryData, dataCenter, isFillUp);
        }

        public IUpdateStepGetter UpdateStepGetter
        {
            get
            {
                return null;
            }
        }

        public List<PreparerArgument> GetAllArguments()
        {
            return null;
        }

        public void SetValue(string key, object value)
        {

        }
    }
}