using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
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
    /// 国内期货市场历史数据的准备步骤
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private PluginHelper pluginHelper;

        private string srcDataPath;

        private string targetDataPath;

        private DataUpdateUtils preparer;

        private IDataLoader dataLoader;

        private bool updateFillUp;

        public StepPreparer(PluginHelper pluginHelper, IDataProvider dataProvider, string srcDataPath, string targetDataPath, bool updateFillUp)
        {
            this.pluginHelper = pluginHelper;
            this.srcDataPath = srcDataPath;
            this.targetDataPath = targetDataPath;
            this.updateFillUp = updateFillUp;
            this.dataLoader = DataLoaderFactory.CreateDataLoader(pluginHelper, dataProvider, srcDataPath, targetDataPath);
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();
            ITradingDayReader tradingDayReader = dataLoader.LoadTradingDayReader();
            Step_TradingDay step_TradingDay = new Step_TradingDay(dataLoader,tradingDayReader);
            steps.Add(step_TradingDay);

            DataLoader_InstrumentInfo dataLoader_InstrumentInfo = new DataLoader_InstrumentInfo(pluginHelper.PluginDirPath);
            Step_CodeInfo step_CodeInfo = new Step_CodeInfo(pluginHelper.PluginDirPath, targetDataPath, dataLoader_InstrumentInfo);            
            steps.Add(step_CodeInfo);

            this.preparer = new DataUpdateUtils(targetDataPath, tradingDayReader, dataLoader_InstrumentInfo, new UpdatedInfo_Csv(targetDataPath));

            GetDayStartTime(steps, dataLoader_InstrumentInfo.GetAllInstruments());
            GetTickSteps(steps);
            GetKLineDataSteps(steps);
            return steps;
        }

        private void GetDayStartTime(List<IStep> steps, List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_TradingSession(codes[i].Code, dataLoader));
            }
        }

        private void GetTickSteps(List<IStep> steps)
        {
            List<InstrumentDatesInfo> dataInfoList = preparer.GetTickNewData(updateFillUp);
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                GetTickSteps(steps, dataInfoList[i]);
            }
        }

        private void GetTickSteps(List<IStep> steps, InstrumentDatesInfo updateDataInfo)
        {
            int stepCount = updateDataInfo.dates.Count / DAYS_EVERYTICKSTEP;
            int lastStepUpdateCount = updateDataInfo.dates.Count % DAYS_EVERYTICKSTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYTICKSTEP;
            List<int> openDates = updateDataInfo.dates;
            for (int i = 0; i < stepCount; i++)
            {
                IStep step;
                if (i != stepCount - 1)
                    step = new Step_TickData(updateDataInfo.instrument, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataLoader);
                else
                    step = new Step_TickData(updateDataInfo.instrument, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataLoader);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps)
        {
            List<InstrumentDatesInfo> dataInfoList = preparer.GetKLineNewData(KLinePeriod.KLinePeriod_1Minute, updateFillUp);
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                GetKLineDataSteps(steps, dataInfoList[i]);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, InstrumentDatesInfo updateDataInfo)
        {
            int stepCount = updateDataInfo.dates.Count / DAYS_EVERYKLINESTEP;
            int lastStepUpdateCount = updateDataInfo.dates.Count % DAYS_EVERYKLINESTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYKLINESTEP;

            List<int> openDates = updateDataInfo.dates;
            string code = updateDataInfo.instrument;

            for (int i = 0; i < stepCount; i++)
            {
                Step_KLineData step;
                if (i != stepCount - 1)
                    step = new Step_KLineData(updateDataInfo.instrument, openDates.GetRange(i * DAYS_EVERYKLINESTEP, DAYS_EVERYKLINESTEP), dataLoader);
                else
                    step = new Step_KLineData(updateDataInfo.instrument, openDates.GetRange(i * DAYS_EVERYKLINESTEP, lastStepUpdateCount), dataLoader);
                steps.Add(step);
            }
        }
    }
}