using com.wer.sc.data.reader;
using com.wer.sc.utils;
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
    /// 数据更新，更新开盘日期
    /// </summary>
    public class Step_TradingDay : IStep
    {
        private DataUpdateHelper dataUpdateHelper;

        public Step_TradingDay(DataUpdateHelper dataUpdateHelper)
        {
            this.dataUpdateHelper = dataUpdateHelper;
        }

        public int ProgressStep
        {
            get
            {
                return 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新开盘日数据";
            }
        }

        public string Proceed()
        {
            string fileName = dataUpdateHelper.GetPath_TradingDays();             

            HashSet<string> set_oldTradingDays = new HashSet<string>();
            if (File.Exists(fileName))
            {
                String[] oldTradingDays = File.ReadAllLines(fileName);
                for (int i = 0; i < oldTradingDays.Length; i++)
                    set_oldTradingDays.Add(oldTradingDays[i]);
            }

            List<int> openDates = dataUpdateHelper.GetNewTradingDays();
            List<string> newOpenDates = new List<string>();
            for (int i = 0; i < openDates.Count; i++)
            {
                string newOpenDateStr = openDates[i].ToString();
                if (set_oldTradingDays.Contains(newOpenDateStr))
                    continue;
                newOpenDates.Add(newOpenDateStr);
            }

            String[] newOpenDateStrArr = new String[newOpenDates.Count];
            for (int i = 0; i < newOpenDates.Count; i++)
            {
                newOpenDateStrArr[i] = newOpenDates[i].ToString(); ;
            }

            FileUtils.EnsureParentDirExist(fileName);
            File.AppendAllLines(fileName, newOpenDateStrArr);

            return "开盘日数据更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}