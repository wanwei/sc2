using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata
{
    public class Plugin_HistoryData_CnStock : Plugin_HistoryData_Csv
    {
        public override string GetCsvDataPath()
        {
            return "";
        }
    }
}
