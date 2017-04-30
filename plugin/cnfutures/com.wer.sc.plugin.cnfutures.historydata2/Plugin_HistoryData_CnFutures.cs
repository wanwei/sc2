using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.data;

namespace com.wer.sc.plugin.cnfutures.historydata
{
    /// <summary>
    /// 中国期货市场历史数据提供插件
    /// </summary>
    [Plugin("HISTORYDATA.CNFUTURES", "中国期货市场", "提供中国期货市场的各种数据，包括大连、上期、郑州、中金四个期货交易所", MarketType.CnFutures)]
    public class Plugin_HistoryData_CnFutures : Plugin_HistoryData_Csv
    {
        private string csvDataPath;

        //private string dataCenterUri;

        //private NeedsToUpdate needsToUpdate;

        public Plugin_HistoryData_CnFutures(PluginHelper pluginHelper) : base(pluginHelper)
        {
            //this.dataCenterUri = dataCenterUri;
            //this.needsToUpdate = new NeedsToUpdate();
            //needsToUpdate.IsTickUpdate = true;
            //needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            //needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            //needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            //needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
        }

        public string CsvDataPath
        {
            get
            {
                return csvDataPath;
            }

            set
            {
                csvDataPath = value;
            }
        }

        public override string GetCsvDataPath()
        {
            //return csvDataPath;
            return @"E:\FUTURES\CSV\TICKADJUSTED\";
        }

        //public override string GetDataCenterUri()
        //{
        //    return dataCenterUri;
        //    //return @"file:D:\SCDATA\CNFUTURES\";
        //}

        //public NeedsToUpdate GetNeedsToUpdate()
        //{
        //    return needsToUpdate;
        //}        
    }
}
