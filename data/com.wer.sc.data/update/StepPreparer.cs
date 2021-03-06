﻿using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 数据更新的准备步骤，将所有步骤准备乘IStep的形式
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private IPlugin_HistoryData historyData;

        //private DataUpdateUtils dataUpdateUtils;

        private IDataStore dataStore;

        private bool isFillUp;

        private DataCenterInfo dataCenterConfig;

        public StepPreparer(IPlugin_HistoryData plugin_HistoryData, DataCenter dataCenter, bool isFillUp)
        {
            this.dataCenterConfig = dataCenter.Config;
            this.historyData = plugin_HistoryData;
            this.dataStore = dataCenter.DataStore;
            this.isFillUp = isFillUp;
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();

            IUpdateInfoStore updateInfoStore = dataStore.CreateUpdateInfoStore();
            UpdatedDataInfo updateDataInfo = updateInfoStore.Load();

            Step_UpdateTradingDays step_UpdateTradingDays = new Step_UpdateTradingDays(historyData, dataStore);
            IList<int> newTradingDays = step_UpdateTradingDays.NewTradingDays;
            steps.Add(step_UpdateTradingDays);
            steps.Add(new Step_UpdateCode(historyData, dataStore));

            //确定是否保存每个品种的交易时间
            if (dataCenterConfig.StoredDataTypes.IsStoreTradingSession)
                AddSteps_TradingSessions(steps);
            //steps.AddRange(new StepGetter_UpdateKLineData_Vaieties(historyData, dataStore, dataCenterConfig.StoredDataTypes.StoreKLinePeriods).GetSteps());
            //确定是否保存tick数据
            // if (dataCenterConfig.StoredDataTypes.IsStoreTick)
            steps.AddRange(new StepGetter_UpdateTickData(historyData, dataStore, isFillUp, updateDataInfo, updateInfoStore, newTradingDays).GetSteps());
            //增加k线数据保存步骤
            steps.AddRange(new StepGetter_UpdateKLineData(historyData, dataStore, dataCenterConfig.StoredDataTypes.StoreKLinePeriods, isFillUp, updateDataInfo, updateInfoStore, newTradingDays).GetSteps());
            steps.Add(new Step_UpdateMainFutures(historyData, dataStore));
            return steps;
        }

        private void AddSteps_TradingSessions(List<IStep> steps)
        {
            List<CodeInfo> instruments = historyData.GetInstruments();
            for (int i = 0; i < instruments.Count; i++)
            {
                Step_UpdateTradingTime_Code step = new Step_UpdateTradingTime_Code(instruments[i].Code, historyData, dataStore);
                steps.Add(step);
            }
        }
    }
}