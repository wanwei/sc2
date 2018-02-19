using com.wer.sc.plugin.cnstock.historydata.download.sina;
using com.wer.sc.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_TickData_Code : IStep
    {
        private string code;

        private Download_Sina downloader = new Download_Sina(DataConst.SINAPATH);

        public Step_TickData_Code(string code)
        {
            this.code = code;
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
                return "更新tick数据";
            }
        }

        public string Proceed()
        {
            HashSet<int> days = GetTickDays(code);

            List<int> sinadays = downloader.GetTickDayList(code);
            for (int i = 0; i < sinadays.Count; i++)
            {
                int sinaDay = sinadays[i];
                if (days.Contains(sinaDay))
                    continue;
                Proceed_CodeDate(code, sinaDay);
            }
            return "完成" + code + "的tick更新";
        }

        private void Proceed_CodeDate(string code, int date)
        {
            List<string[]> sinaTickData = downloader.GetTickData(code, date);
            List<string[]> tickData = new List<string[]>(sinaTickData.Count);
            int totalmount = 0;
            for (int i = sinaTickData.Count - 1; i > 0; i--)
            {
                string[] sinaTick = sinaTickData[i];
                if (sinaTick.Length < 6)
                    continue;
                string[] tick = new string[10];
                string time = sinaTick[0];
                if (time.Length < 8)
                    time = time + "0";
                tick[0] = date + "." + time.Substring(0, 2) + time.Substring(3, 2) + time.Substring(6, 2);
                tick[1] = sinaTick[1];
                tick[2] = sinaTick[3];
                totalmount += int.Parse(tick[2]);
                tick[3] = totalmount.ToString();
                tick[4] = "0";
                tick[5] = sinaTick[1];
                tick[6] = "0";
                tick[7] = sinaTick[1];
                tick[8] = "0";
                tick[9] = "0";
                tickData.Add(tick);
            }

            string path = GetTickPath(code, date);
            Download_Sina.SaveData(path, tickData);
        }

        public static string GetTickPath(string code, int date)
        {
            return DataConst.CSVPATH + code + "\\tick\\" + code + "_" + date + ".csv";
        }

        public HashSet<int> GetTickDays(string code)
        {
            string tickPath = DataConst.CSVPATH + code + "\\tick\\";
            if (!Directory.Exists(tickPath))
                return new HashSet<int>();
            string[] files = Directory.GetFiles(tickPath, "*.csv");
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startDateIndex = file.IndexOf('_') + 1;
                int date = int.Parse(file.Substring(startDateIndex, 8));
                set.Add(date);
            }
            return set;
        }

        public List<int> GetTickDayList(string code)
        {
            string tickPath = DataConst.CSVPATH + code + "\\tick\\";
            if (!Directory.Exists(tickPath))
                return new List<int>();
            string[] files = Directory.GetFiles(tickPath, "*.csv");
            List<int> set = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startDateIndex = file.IndexOf('_') + 1;
                int date = int.Parse(file.Substring(startDateIndex, 8));
                set.Add(date);
            }
            return set;
        }
    }
}