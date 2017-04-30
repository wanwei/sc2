using com.wer.sc.data;
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
    public class AdjustTickStepGetter : IUpdateStepGetter
    {

        private List<CodeInfo> codes;
        private List<IStep> steps = new List<IStep>();

        public AdjustTickStepGetter()
        {
            CodeInfoGenerator gen = new CodeInfoGenerator("");
            codes = gen.GenerateCodes();

            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                AdjustIndexTickStep step = new AdjustIndexTickStep(code, gen.GetOldCodeInfo(code.Code).Code);
                steps.Add(step);
            }
        }

        public List<IStep> GetSteps()
        {
            return steps;
        }
    }

    public class AdjustIndexTickStep : IStep
    {
        string srcPath = @"E:\FUTURES\CSV\DATACENTERSOURCE\";
        string targetPath = @"E:\FUTURES\CSV\DATACENTERSOURCE\";

        private CodeInfo code;

        private string oldCodeId;

        public AdjustIndexTickStep(CodeInfo code, string oldCodeId)
        {
            this.code = code;
            this.oldCodeId = oldCodeId;
        }

        public AdjustIndexTickStep(CodeInfo code, string oldCodeId, string srcPath, string targetPath)
        {
            this.code = code;
            this.oldCodeId = oldCodeId;
            this.srcPath = srcPath;
            this.targetPath = targetPath;
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
                return "移动" + code.Code + "tick数据";
            }
        }

        public string Proceed()
        {
            if (code.Code.EndsWith("0000"))
            {
                AdjustIndex(code, oldCodeId);
            }
            return "";
        }

        private void AdjustIndex(CodeInfo code, string oldCodeId)
        {
            string path = GetSrcPath(code.Code);
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists) return;
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                int date = GetDate(file);
                if (date < 0)
                    continue;
                AdjustIndex(code, oldCodeId, date);
            }
        }
        private string GetSrcPath(string codeId)
        {
            return srcPath + codeId + "\\tick\\";
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

        private void AdjustIndex(CodeInfo code, string oldCodeId, int date)
        {
            string codeId = code.Code;
            string variety = codeId.Substring(0, codeId.Length - 4);
            string srcPath = this.srcPath + codeId + "\\tick\\" + variety + "13_" + date + ".csv";
            string targetPath = this.targetPath + codeId + "\\tick\\" + variety + "0000_" + date + ".csv";
            if (File.Exists(srcPath))
            {
                FileInfo f = new FileInfo(targetPath);
                if (f.Exists)
                    return;       
                File.Move(srcPath, targetPath);
            }
        }


        private string GetSrcPath(CodeInfo code, string oldCodeId, int date)
        {
            //return srcPath + oldCodeId + "\\tick\\" + oldCodeId + "_" + date + ".csv";
            return GetTickPath(srcPath, oldCodeId, date);
        }

        private string GetTargetPath(CodeInfo code, string oldCodeId, int date)
        {
            return GetTickPath(targetPath, code.Code, date);
        }

        private string GetTickPath(string path, string codeId, int date)
        {
            return path + codeId + "\\tick\\" + codeId + "_" + date + ".csv";
        }
    }

    public class AdjustTickStep : IStep
    {
        string srcPath = @"E:\FUTURES\CSV\TICKADJUSTED\";
        string targetPath = @"E:\FUTURES\CSV\DATACENTERSOURCE\";

        private CodeInfo code;

        private string oldCodeId;

        public AdjustTickStep(CodeInfo code, string oldCodeId)
        {
            this.code = code;
            this.oldCodeId = oldCodeId;
        }

        public AdjustTickStep(CodeInfo code, string oldCodeId, string srcPath, string targetPath)
        {
            this.code = code;
            this.oldCodeId = oldCodeId;
            this.srcPath = srcPath;
            this.targetPath = targetPath;
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
                return "移动" + code.Code + "tick数据";
            }
        }

        public string Proceed()
        {
            if (code.Code.EndsWith("MI"))
            {
                return "";
            }
            if (code.Code.EndsWith("0000"))
            {
                AdjustIndex(code, oldCodeId);
                return "";
            }
            int startDate = code.Start;
            int endDate = code.End;
            for (int i = startDate; i <= endDate; i++)
            {
                string srcPath = GetSrcPath(code, oldCodeId, i);
                string targetPath = GetTargetPath(code, oldCodeId, i);
                //Console.WriteLine(srcPath + "|" + targetPath);

                if (File.Exists(srcPath))
                {
                    FileInfo f = new FileInfo(targetPath);
                    if (f.Exists)
                        continue;
                    DirectoryInfo dir = f.Directory;
                    if (!dir.Exists)
                        dir.Create();
                    File.Move(srcPath, targetPath);
                }
            }
            return "";
        }

        private void AdjustIndex(CodeInfo code, string oldCodeId)
        {
            string srcPath = this.srcPath + oldCodeId + "\\tick\\";
            string targetPath = this.targetPath + code.Code + "\\tick\\";
            //Console.WriteLine(srcPath + "|" + targetPath);
            if (Directory.Exists(srcPath))
            {
                DirectoryInfo dir = new DirectoryInfo(targetPath);
                if (!dir.Parent.Exists)
                {
                    dir.Parent.Create();
                }
                Directory.Move(srcPath, targetPath);
            }
        }

        private string GetSrcPath(CodeInfo code, string oldCodeId, int date)
        {
            //return srcPath + oldCodeId + "\\tick\\" + oldCodeId + "_" + date + ".csv";
            return GetTickPath(srcPath, oldCodeId, date);
        }

        private string GetTargetPath(CodeInfo code, string oldCodeId, int date)
        {
            return GetTickPath(targetPath, code.Code, date);
        }

        private string GetTickPath(string path, string codeId, int date)
        {
            return path + codeId + "\\tick\\" + codeId + "_" + date + ".csv";
        }
    }
}
