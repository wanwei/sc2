using com.wer.sc.data.utils;
using com.wer.sc.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.checker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            controlDataUpdate1.BeforeProceedStart += ControlDataUpdate1_BeforeProceedStart;
        }

        private void ControlDataUpdate1_BeforeProceedStart()
        {
            controlDataUpdate1.DataProceed = new UpdateStepGetter(this.tbPath.Text.Trim());
        }
    }

    class UpdateStepGetter : IUpdateHelper
    {
        private List<IStep> steps = new List<IStep>();

        private string path;

        public UpdateStepGetter(string path)
        {
            this.path = path;
            //this.steps.Add(new CheckStep_BiaoPuYongHua(path));
            //this.steps.Add(new CheckStep_JinShuYuan(path));
            //this.steps.Add(new CheckStep_BiaoPuYongHua(path));
            string fileName = "check\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".error";
            FileUtils.EnsureParentDirExist(fileName);
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            for (int i = 0; i < subDirs.Length; i++)
            {
                DirectoryInfo subDir = subDirs[i];
                string code = subDir.Name;
                GetUpdateSteps(subDir.FullName, code, fileName);
            }
            //this.steps.Add(GetStep("SR1505", 20141212, fileName));
        }

        private IStep GetStep(string code, int date, string errFileName)
        {
            string path = this.path + "\\" + code + "\\";
            CheckStep step = new CheckStep(path, code, date, errFileName);
            return step;
        }

        private void GetUpdateSteps(string path, string code, string errFileName)
        {
            string tickPath = path + "\\tick";
            DirectoryInfo dir = new DirectoryInfo(tickPath);
            if (!dir.Exists)
                return;
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Name;
                string dateStr = fileName.Substring(fileName.IndexOf('_') + 1, 8);
                int date = int.Parse(dateStr);
                CheckStep step = new CheckStep(path, code, date, errFileName);
                this.steps.Add(step);
            }
        }

        public List<IStep> GetSteps()
        {
            return steps;
        }
    }

    class CheckStep_BiaoPuYongHua : IStep
    {
        public CheckStep_BiaoPuYongHua(string path)
        {

        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新标普永华数据";
            }
        }

        public string Proceed()
        {
            return "";
        }
    }

    class CheckStep_JinShuYuan : IStep
    {
        private string path;

        public CheckStep_JinShuYuan(string path)
        {
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
                return "";
            }
        }

        public string Proceed()
        {
            return "";
        }
    }

    class CheckStep_Daily
    {

    }

    class CheckStep : IStep
    {
        public const int ERRORTYPE_NODATA = 0;

        public const int ERRORTYPE_LACKDATA = 1;

        public const int ERRORTYPE_LACKDATA_TICK = 2;

        public const int ERRORTYPE_ERRDATA = 3;

        public const int ERRORTYPE_DATANOTEXIST = 4;

        private string path;

        private string code;

        private int date;

        private string errFileName;

        public CheckStep(string path, string code, int date, string errFileName)
        {
            this.path = path;
            this.code = code;
            this.date = date;
            this.errFileName = errFileName;
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
                return "检测数据" + code + "-" + date;
            }
        }

        public string Proceed()
        {
            IKLineData klineData = ReadKLineData();
            if (klineData == null)
            {
                File.AppendAllLines(errFileName, new string[] { code + "," + date + "," + ERRORTYPE_NODATA + ",没有K现数据" });
                return "";
            }
            ITickData tickData = ReadTickData();
            //if (CheckTickData(tickData))
            //    return "";
            if (tickData == null)
                return "";
            if (tickData.Length < 1000)
                return "";
            CheckError(tickData, klineData);
            return "";
        }

        private bool CheckTickData(ITickData tickData)
        {
            if (!code.EndsWith("0000"))
                return false;
            for (int i = 50; i < tickData.Length; i++)
            {
                if (IsTheSame(tickData, i))
                {
                    File.AppendAllLines(errFileName, new string[] { code + "," + date + "," + ERRORTYPE_ERRDATA + "," + tickData.Arr_Time[i] });
                    return true;
                }
            }
            return false;
        }

        private bool IsTheSame(ITickData tickData, int index)
        {
            for (int i = index - 50; i < index; i++)
            {
                if (tickData.Arr_Price[i] != tickData.Arr_Price[i + 1]
                    || tickData.Arr_Hold[i] != tickData.Arr_Hold[i + 1]
                    || tickData.Arr_Add[i] != 0)
                    return false;
            }
            return true;
        }

        private void CheckError(ITickData tickData, IKLineData klineData)
        {
            List<int[]> indeies = new List<int[]>();
            for (int i = 0; i < klineData.Length; i++)
            {
                double startTime = klineData.Arr_Time[i];
                double endTime = TimeUtils.AddMinutes(startTime, 1);
                int startIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, startTime);
                int endIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, endTime);
                indeies.Add(new int[] { startIndex, endIndex });
            }
            CheckIndexArr(tickData, indeies);
        }

        private void CheckIndexArr(ITickData tickData, List<int[]> indeies)
        {
            //数据检查：
            //1.tick数据比较多，但是K线数据少了不少
            //2.tick数据少，K线数据多了不少
            int min = tickData.Length / 50;
            for (int i = 0; i < indeies.Count; i++)
            {
                if (CheckLackKLine(tickData, indeies, i, min))
                    return;
                if (i >= 0)
                {
                    if (CheckLackTick(tickData, indeies, i))
                        return;
                }
            }
        }

        private bool CheckLackKLine(ITickData tickData, List<int[]> indeies, int i, int min)
        {
            int[] index = indeies[i];
            if (i == 0)
            {
                if (index[0] > min)
                {
                    File.AppendAllLines(errFileName, new string[] { code + "," + date + "," + CheckStep.ERRORTYPE_LACKDATA + "," + tickData.Arr_Time[index[0]] });
                    return true;
                }
            }
            else
            {
                int between = index[0] - indeies[i - 1][1];
                if (between > min)
                {
                    File.AppendAllLines(errFileName, new string[] { code + "," + date + "," + CheckStep.ERRORTYPE_LACKDATA + "," + tickData.Arr_Time[index[0]] });
                    return true;
                }
            }
            return false;
        }

        private bool CheckLackTick(ITickData tickData, List<int[]> indeies, int i)
        {
            int[] index = indeies[i];
            if (index[0] < 0 && index[1] < 0)
            {
                File.AppendAllLines(errFileName, new string[] { code + "," + date + "," + CheckStep.ERRORTYPE_LACKDATA_TICK + ",tick缺数据" });
                return true;
            }
            return false;
        }

        private ITickData ReadTickData()
        {
            string tickpath = path + "\\tick\\" + code + "_" + date + ".csv";
            if (!File.Exists(tickpath))
                return null;
            return CsvUtils_TickData.Load(tickpath);
        }

        private IKLineData ReadKLineData()
        {
            string klinepath = path + "\\kline\\1minute\\" + code + "_1MINUTE_" + date + ".csv";
            if (!File.Exists(klinepath))
                return null;
            return CsvUtils_KLineData.Load(klinepath);
        }
    }
}