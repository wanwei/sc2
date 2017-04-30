using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.utils;
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
    /// 数据更新，更新开盘日期
    /// </summary>
    public class Step_TradingDay : IStep
    {
        private IDataLoader dataLoader;

        public Step_TradingDay(IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
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
            List<int> openDates = dataLoader.LoadTradingDayReader().GetAllTradingDays();
            String[] openDateStr = new String[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                openDateStr[i] = openDates[i].ToString(); ;
            }
            string fileName = dataLoader.CsvDataPath + "\\opendates.csv";
            FileUtils.EnsureParentDirExist(fileName);
            File.WriteAllLines(fileName, openDateStr);
            return "开盘日数据更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}