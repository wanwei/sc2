using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.historydata;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 更新所有的股票或期货信息
    /// </summary>
    public class Step_CodeInfo : IStep
    {
        private string pluginPath;
        private string csvDataPath;
        private List<CodeInfo> codes;

        public Step_CodeInfo(string pluginPath, string csvDataPath, DataLoader_InstrumentInfo dataLoader_InstrumentInfo)
        {
            this.pluginPath = pluginPath;
            this.csvDataPath = csvDataPath;
            this.codes = dataLoader_InstrumentInfo.GetAllInstruments();
        }

        public int ProgressStep
        {
            get
            {
                return 1;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新期货信息";
            }
        }

        public string Proceed()
        {
            CsvUtils_Code.Save(csvDataPath + "\\instruments.csv", codes);
            return "期货信息更新完成";
        }        

        public override string ToString()
        {
            return StepDesc;
        }
    }
}