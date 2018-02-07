using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.data;

namespace com.wer.sc.mockdata
{
    /// <summary>
    /// 
    /// </summary>
    public class Plugin_HistoryData_MockData : Plugin_HistoryData_Csv
    {
        private string csvDataPath;

        public Plugin_HistoryData_MockData() : base(null)
        {
        }

        public Plugin_HistoryData_MockData(string csvDataPath) : base(null)
        {
            this.csvDataPath = csvDataPath;
        }

        public string CsvDataPath
        {
            get
            {
                return csvDataPath;
            }
        }

        public override string GetCsvDataPath()
        {
            if (csvDataPath != null)
                return csvDataPath;
            //return @"E:\FUTURES\MOCKDATA\";
            return @"E:\FUTURES\CSV\DATACENTERSOURCE\";
        }

        public override ITradingTime GetDefaultTradingTime()
        {
            return null;
        }

        //public override IList<TradingTime> GetTradingTime(string code)
        //{
        //    return null;
        //}
    }
}
