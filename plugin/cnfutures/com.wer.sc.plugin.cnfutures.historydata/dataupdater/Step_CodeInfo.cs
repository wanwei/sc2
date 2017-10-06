using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
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
        private DataUpdateHelper dataUpdateHelper;

        public Step_CodeInfo(DataUpdateHelper dataUpdateHelper)
        {
            this.dataUpdateHelper = dataUpdateHelper;
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

        public List<CodeInfo> GetAllCodes()
        {
            string path = this.dataUpdateHelper.GetPath_Code();

            //装载已更新好的股票
            Dictionary<string, CodeInfo> dic_Id_CodeInfo = new Dictionary<string, CodeInfo>();
            List<CodeInfo> updatedCodes = CsvUtils_Code.Load(path);
            if (File.Exists(path))
            {
                for (int i = 0; i < updatedCodes.Count; i++)
                {
                    dic_Id_CodeInfo.Add(updatedCodes[i].Code, updatedCodes[i]);
                }
            }

            //从新的合约中找到之前没有的合约
            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            List<CodeInfo> newcodes = new List<CodeInfo>(updatedCodes);
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                if (dic_Id_CodeInfo.ContainsKey(code.Code))
                {
                    CodeInfo codeInfo = dic_Id_CodeInfo[code.Code];
                    if (codeInfo.Start != 0 && codeInfo.Start < code.Start)
                        code.Start = codeInfo.Start;
                    if (codeInfo.Start != 0 && codeInfo.End > code.End)
                        code.End = codeInfo.End;
                }
                else
                    newcodes.Add(code);
            }

            newcodes.Sort(new CodeInfoComparer());
            return newcodes;
        }

        public string Proceed()
        {
            //string path = this.dataUpdateHelper.GetPath_Code();

            ////装载已更新好的股票
            //Dictionary<string, CodeInfo> dic_Id_CodeInfo = new Dictionary<string, CodeInfo>();
            //List<CodeInfo> updatedCodes = CsvUtils_Code.Load(path);
            //if (File.Exists(path))
            //{                
            //    for (int i = 0; i < updatedCodes.Count; i++)
            //    {
            //        dic_Id_CodeInfo.Add(updatedCodes[i].Code, updatedCodes[i]);
            //    }
            //}

            ////从新的合约中找到之前没有的合约
            //List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            //List<CodeInfo> newcodes = new List<CodeInfo>(updatedCodes);
            //for (int i = 0; i < codes.Count; i++)
            //{
            //    CodeInfo code = codes[i];
            //    if (dic_Id_CodeInfo.ContainsKey(code.Code))
            //        continue;
            //    newcodes.Add(code);
            //}

            //newcodes.Sort(new CodeInfoComparer());
            //CsvUtils_Code.Append(path, newcodes);

            string path = this.dataUpdateHelper.GetPath_Code();
            List<CodeInfo> newcodes = GetAllCodes();
            CsvUtils_Code.Save(path, newcodes);
            return "期货信息更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}