using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;

namespace com.wer.sc.plugin.historydata
{
    public abstract class Plugin_DataUpdater : IPlugin_DataUpdater
    {        

        public abstract IUpdateHelper PluginHelper { get; }

        public abstract List<PreparerArgument> GetAllArguments();

        public void SetValue(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
