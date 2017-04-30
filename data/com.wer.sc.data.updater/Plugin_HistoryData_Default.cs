using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.data;

namespace com.wer.sc.data.updater
{
    /// <summary>
    /// 默认的数据更新插件
    /// 设置
    /// </summary>
    public class Plugin_HistoryData_Default : Plugin_HistoryData_Csv
    {
        private string csvDataPath;

        public Plugin_HistoryData_Default(string srcDataPath) : base(null)
        {
            this.csvDataPath = srcDataPath;
        }

        public override string GetCsvDataPath()
        {
            return csvDataPath;
        }
    }
}
