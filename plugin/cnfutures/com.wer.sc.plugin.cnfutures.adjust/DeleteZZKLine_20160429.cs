using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.adjust
{
    /// <summary>
    /// 删掉郑州交易所20160429以后的K线数据：
    /// E:\FUTURES\CSV\DATACENTERSOURCE
    /// 
    /// 删掉郑州交易所所有K线数据：
    /// E:\SCDATA\CNFUTURES
    /// </summary>
    public class DeleteZZKLine_20160429 : IUpdateHelper
    {
        public const string DATAPATH_CNFUTURES = @"E:\FUTURES\CSV\DATACENTERSOURCE";

        public const string DATAPATH_DATACENTER = @"E:\SCDATA\CNFUTURES";

        public const string PLUGINPATH = @"D:\SCWORK\DEV\SC2\bin\Debug\plugin\cnfutures\";

        private List<IStep> steps = new List<IStep>();

       // private DataLoader_Variety dataLoader_Variety;

        public DeleteZZKLine_20160429()
        {
            //this.dataLoader_Variety = new DataLoader_Variety(DeleteZZKLine_20160429.PLUGINPATH);
            //List<VarietyInfo> varieties = this.dataLoader_Variety.GetVarieties(CodeInfoUtils.EXCHANGE_ZZ);
            //HashSet<string> set_ZZCode = new HashSet<string>();
            //for (int i = 0; i < varieties.Count; i++)
            //{
            //    set_ZZCode.Add(varieties[i].Code);
            //}

            List<CodeInfo> codes = CsvUtils_Code.Load(DATAPATH_DATACENTER + "\\instruments");

            CacheUtils_CodeInfo cache = new CacheUtils_CodeInfo(codes);
            List<CodeInfo> zzcodes = cache.GetCodesByExchange(CodeInfoUtils.EXCHANGE_ZZ);


            for (int i = 0; i < zzcodes.Count; i++)
            {
                CodeInfo code = zzcodes[i];
                Step_DeleteKLine_CnFutures step = new Step_DeleteKLine_CnFutures(code.Code, DATAPATH_CNFUTURES);
                steps.Add(step);
            }

            for (int i = 0; i < zzcodes.Count; i++)
            {
                CodeInfo code = zzcodes[i];
                Step_DeleteKLine_DataCenter step = new Step_DeleteKLine_DataCenter(code.Code, DATAPATH_DATACENTER);
                steps.Add(step);
            }
        }

        public List<IStep> GetSteps()
        {
            return steps;
        }
    }

    public class Step_DeleteKLine_CnFutures : IStep
    {
        private string code;

        private string path;

        public Step_DeleteKLine_CnFutures(string code, string path)
        {
            this.code = code;
            this.path = path;
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
                return "删除" + code + "的期货数据";
            }
        }

        public string Proceed()
        {
            string klinePath = path + "\\" + code + "\\kline\\1MINUTE\\";
            if (!Directory.Exists(klinePath))
                return "";
            string[] files = Directory.GetFiles(klinePath);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string name = new FileInfo(file).Name;
                int index = name.LastIndexOf('.');
                int date = int.Parse(name.Substring(index - 8, 8));
                if (date > 20160429)
                {
                    File.Delete(file);
                }
            }
            return "";
        }
    }

    public class Step_DeleteKLine_DataCenter : IStep
    {
        private string code;

        private string path;
        public Step_DeleteKLine_DataCenter(string code, string path)
        {
            this.code = code;
            this.path = path;
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
                return "删除" + code + "的数据中心数据";
            }
        }

        public string Proceed()
        {
            string klinePath = path + "\\" + code + "\\";
            if (!Directory.Exists(klinePath))
                return "";
            DirectoryInfo dir = new DirectoryInfo(klinePath);
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = files[i];
                string name = f.Name.ToLower();
                if (name.EndsWith("kline"))
                    File.Delete(f.FullName);
                else if (name.EndsWith("index"))
                    File.Delete(f.FullName);
            }
            return "";
        }
    }
}
