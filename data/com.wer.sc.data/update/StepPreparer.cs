using com.wer.sc.data.datacenter;
using com.wer.sc.data.store;
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

        private DataCenterConfig dataCenterConfig;

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

            UpdatedDataInfo updateDataInfo = dataStore.CreateUpdateInfoStore().Load();

            steps.Add(new Step_UpdateTradingDays(historyData, dataStore));
            steps.Add(new Step_UpdateInstrument(historyData, dataStore));

            //确定是否保存每个品种的交易时间
            if (dataCenterConfig.StoredDataTypes.IsStoreTradingSession)
                AddSteps_TradingSessions(steps);

            //确定是否保存tick数据
            // if (dataCenterConfig.StoredDataTypes.IsStoreTick)
            steps.AddRange(new StepGetter_UpdateTickData(historyData, dataStore, isFillUp, updateDataInfo).GetSteps());
            //增加k线数据保存步骤
            steps.AddRange(new StepGetter_UpdateKLineData(historyData, dataStore, dataCenterConfig.StoredDataTypes.StoreKLinePeriods, isFillUp, updateDataInfo).GetSteps());

            return steps;
        }

        private void AddSteps_TradingSessions(List<IStep> steps)
        {
            List<CodeInfo> instruments = historyData.GetInstruments();
            for (int i = 0; i < instruments.Count; i++)
            {
                Step_UpdateTradingSession_Instrument step = new Step_UpdateTradingSession_Instrument(instruments[i].Code, historyData, dataStore);
                steps.Add(step);
            }
        }
    }
}