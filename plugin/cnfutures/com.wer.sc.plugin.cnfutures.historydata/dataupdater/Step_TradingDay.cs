﻿using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
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
        private IDataLoader dataLoader;
        private List<int> openDates;

        public Step_TradingDay(IDataLoader dataLoader, ITradingDayReader tradingDayReader)
        {
            this.dataLoader = dataLoader;
            this.openDates = tradingDayReader.GetAllTradingDays();
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
            String[] openDateStr = new String[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                openDateStr[i] = openDates[i].ToString(); ;
            }
            string fileName = dataLoader.GetTargetDataPath() + "\\tradingdays.csv";
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