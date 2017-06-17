using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.adjust
{
    public class AdjustIndexTick : IUpdateHelper
    {

        private List<IStep> steps = new List<IStep>();

        public AdjustIndexTick()
        {
            string path = @"E:\SCDATA\CNFUTURES";
            //string path = @"E:\FUTURES\CSV\DATACENTERSOURCE";
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] subdirs = dir.GetDirectories();
            for (int i = 0; i < subdirs.Length; i++)
            {
                DirectoryInfo subDir = subdirs[i];
                if (subDir.Name.EndsWith("0000"))
                {
                    steps.Add(GetStep(subDir));
                }
            }
        }

        private IStep GetStep(DirectoryInfo dir)
        {
            return new AdjustIndexTickSteps(dir);
        }

        public List<IStep> GetSteps()
        {
            return steps;
        }
    }

    class AdjustIndexTickSteps : IStep
    {
        private DirectoryInfo dir;

        public AdjustIndexTickSteps(DirectoryInfo dir)
        {
            this.dir = dir;
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
                return "更新" + dir.Name;
            }
        }

        public string Proceed()
        {
            string fullName = dir.FullName;// + "\\kline\\";
            DirectoryInfo tickDir = new DirectoryInfo(fullName);
            tickDir.Delete(true);
            //FileInfo[] files = tickDir.GetFiles();
            //for (int i = 0; i < files.Length; i++)
            //{
            //    FileInfo f = files[i];
            //    //ModifyFile(f);
            //    //f.Delete()
            //}

            return "";
        }

        private void ModifyFile(FileInfo f)
        {
            int startDate = -1;
            int realStartDate = -1;
            int between = 0;

            string[] lines = File.ReadAllLines(f.FullName);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int index = line.IndexOf('.');

                int currentStartDate = int.Parse(line.Substring(0, index));
                if (startDate < 0) { 
                    startDate = currentStartDate;
                    realStartDate = startDate / 2;
                }
                else
                {

                }

                //lines[i] = realStartDate
            }

            File.WriteAllLines(f.FullName, lines);
        }
    }
}
