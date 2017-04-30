using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;

namespace com.wer.sc.mockdata
{
    /// <summary>
    /// 
    /// </summary>
    public class Plugin_HistoryData_MockData : Plugin_HistoryData_Csv
    {
        public Plugin_HistoryData_MockData() : base(null)
        {
        }

        public override string GetCsvDataPath()
        {
            return @"E:\FUTURES\MOCKDATA\";
        }
    }
}
