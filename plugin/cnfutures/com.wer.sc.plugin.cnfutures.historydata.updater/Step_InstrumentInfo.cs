using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.cnfutures.historydata.datapreparer.Properties;
using com.wer.sc.plugin.historydata;
using com.wer.sc.utils.update;
using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
{
    /// <summary>
    /// 更新所有的股票或期货信息
    /// </summary>
    public class Step_InstrumentInfo : IStep
    {
        private string csvDataPath;

        public Step_InstrumentInfo(string csvDataPath)
        {
            this.csvDataPath = csvDataPath;
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
            File.Copy(PathUtils.InstrumentPath, csvDataPath + "\\instruments.csv");
            return "期货信息更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}