using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Plugin_History_MockUpdate : Plugin_HistoryData_Csv
    {
        private string csvDataPath;

        public Plugin_History_MockUpdate(string csvDataPath) : base(null)
        {

        }

        public Plugin_History_MockUpdate(PluginHelper pluginHelper, string csvDataPath) : base(pluginHelper)
        {
            this.csvDataPath = csvDataPath;
        }

        public override string GetCsvDataPath()
        {
            return csvDataPath;
        }

        public override TradingTime GetDefaultTradingTime()
        {
            throw new NotImplementedException();
        }
    }
}
