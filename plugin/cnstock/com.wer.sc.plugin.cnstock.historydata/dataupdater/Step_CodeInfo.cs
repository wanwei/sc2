using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_CodeInfo : IStep
    {
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
                return "更新所有股票信息";
            }
        }

        public List<CodeInfo> GetAllCodes()
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            codes.Add(new CodeInfo("sh600516", "方大炭素", "sh"));
            codes.Add(new CodeInfo("sh600019", "宝钢股份", "sh"));
            codes.Add(new CodeInfo("sh601155", "新城控股", "sh"));
            codes.Add(new CodeInfo("sz002110", "三钢闽光", "sz"));
            codes.Add(new CodeInfo("sz000830", "鲁西化工", "sz"));
            codes.Add(new CodeInfo("sh601318", "中国平安", "sh"));
            codes.Add(new CodeInfo("sz000932", "ST华菱", "sz"));
            return codes;
        }

        public string Proceed()
        {
            string targetPath = DataConst.CSVPATH + @"instruments.csv";
            CsvUtils_Code.Save(targetPath, GetAllCodes());
            return "股票信息更新完成";
        }
    }
}