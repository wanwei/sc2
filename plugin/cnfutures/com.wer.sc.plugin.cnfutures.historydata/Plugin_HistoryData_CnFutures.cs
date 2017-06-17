using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.data;
using com.wer.sc.utils;
using System.IO;

namespace com.wer.sc.plugin.cnfutures.historydata
{
    /// <summary>
    /// 中国期货市场历史数据提供插件
    /// </summary>
    [Plugin("HISTORYDATA.CNFUTURES", "中国期货市场", "提供中国期货市场的各种数据，包括大连、上期、郑州、中金四个期货交易所", MarketType.CnFutures)]
    public class Plugin_HistoryData_CnFutures : Plugin_HistoryData_Csv
    {
        public const string DATAPATH = @"E:\FUTURES\CSV\DATACENTERSOURCE\";

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

        public Plugin_HistoryData_CnFutures(PluginHelper pluginHelper, string dataPath) : this(pluginHelper)
        {
            this.csvDataPath = dataPath;
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
            if (!StringUtils.IsEmpty(csvDataPath))
                return csvDataPath;
            //return csvDataPath;
            //return @"E:\FUTURES\CSV\TICKADJUSTED\";            

            string configPath = this.pluginHelper.PluginDirPath + "\\config\\cnfuturespath";
            if (File.Exists(configPath))
            {
                string path = File.ReadAllText(configPath);
                if (!StringUtils.IsEmpty(path))
                {
                    this.csvDataPath = path;
                }
            }
            this.csvDataPath = DATAPATH;

            //string path = pluginPath + "\\config\\biaopuyonghua.instruments.csv";
            return csvDataPath;
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
