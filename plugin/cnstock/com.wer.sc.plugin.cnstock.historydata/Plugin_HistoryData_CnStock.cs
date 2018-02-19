using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata
{
    [Plugin("HISTORYDATA.CNSTOCK", "中国股票市场", "提供中国期货市场的各种数据，包括大连、上期、郑州、中金四个期货交易所", MarketType.CnStock)]
    public class Plugin_HistoryData_CnStock : Plugin_HistoryData_Csv
    {
        public const string DATAPATH = @"C:\DEMO\DATASRC\";

        public Plugin_HistoryData_CnStock(PluginHelper pluginHelper) : base(pluginHelper)
        {
        }

        public override string GetCsvDataPath()
        {
            return DATAPATH;
        }
    }
}
