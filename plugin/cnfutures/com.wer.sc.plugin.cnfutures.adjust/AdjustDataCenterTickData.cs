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
    public class AdjustDataCenterTickData : IUpdateHelper
    {
        string targetPath = @"E:\SCDATA\CNFUTURES\instruments";
        private List<CodeInfo> codes;
        private List<IStep> steps = new List<IStep>();

        public AdjustDataCenterTickData()
        {
            //CodeInfoGenerator gen = new CodeInfoGenerator("");
            //codes = gen.GetAllInstruments();
            //CsvUtils_Code.Save(targetPath, codes);
            //for (int i = 0; i < codes.Count; i++)
            //{
            //    CodeInfo code = codes[i];
            //    AdjustDataCenterTickDataStep step = new AdjustDataCenterTickDataStep(code, gen.GetOldCodeInfo(code.Code).Code);
            //    steps.Add(step);
            //}
        }

        public List<IStep> GetSteps()
        {
            return steps;
        }
    }

    public class AdjustDataCenterTickDataStep : IStep
    {
        string srcPath = @"E:\SCDATA\CNFUTURES2\";
        string targetPath = @"E:\SCDATA\CNFUTURES\";

        private CodeInfo code;

        private string oldCodeId;

        public AdjustDataCenterTickDataStep(CodeInfo code, string oldCodeId)
        {
            this.code = code;
            this.oldCodeId = oldCodeId;
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
                return "移动品种" + code.Code;
            }
        }

        public string Proceed()
        {
            int startDate = code.Start;
            int endDate = code.End;
            if (startDate < 0 || endDate < 0)
            {
                MoveData_Index();
                return "";
            }
            for (int i = startDate; i <= endDate; i++)
            {
                MoveData(i);
            }
            return "";
        }

        private void MoveData_Index()
        {
            string path = GetSrcPath(oldCodeId);
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists) return;
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                int date = GetDate(file);
                if (date < 0)
                    continue;
                MoveData(date);
            }
        }

        private int GetDate(FileInfo file)
        {
            string fileName = file.Name;
            int index = fileName.IndexOf('_');
            string dateStr = fileName.Substring(index + 1, 8);
            int date = -1;
            int.TryParse(dateStr, out date);
            return date;
        }

        private void MoveData(int date)
        {
            string srcPath = GetSrcPath(code, oldCodeId, date);
            string targetPath = GetTargetPath(code, oldCodeId, date);

            if (File.Exists(srcPath))
            {
                FileInfo f = new FileInfo(targetPath);
                if (f.Exists)
                    return;
                DirectoryInfo dir = f.Directory;
                if (!dir.Exists)
                    dir.Create();
                File.Move(srcPath, targetPath);
            }
        }

        private string GetSrcPath(string oldCodeId)
        {
            return srcPath + oldCodeId + "\\tick\\";
        }

        private string GetSrcPath(CodeInfo code, string oldCodeId, int date)
        {
            return GetTickPath(srcPath, oldCodeId, date);
        }

        private string GetTargetPath(CodeInfo code, string oldCodeId, int date)
        {
            return GetTickPath(targetPath, code.Code, date);
        }

        private string GetTickPath(string path, string codeId, int date)
        {
            return path + codeId + "\\tick\\" + codeId + "_" + date + ".tick";
        }
    }
}
