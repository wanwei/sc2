using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class DataUpdater_CnStock : IPlugin_DataUpdater
    {
        private PluginHelper pluginHelper;

        public DataUpdater_CnStock(PluginHelper pluginHelper)
        {
            this.pluginHelper = pluginHelper;
        }

        public IUpdateHelper PluginHelper
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
