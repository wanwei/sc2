using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
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

        private string csvDataPath;

        private string dataCenterUri;

        private DataUpdateUtils preparer;

        private IDataLoader dataLoader;

        private bool updateFillUp;

        public StepPreparer(PluginHelper pluginHelper, string srcDataPath, string csvDataPath, string dataCenterUri, bool updateFillUp)
        {
            this.pluginHelper = pluginHelper;
            this.srcDataPath = srcDataPath;
            this.csvDataPath = csvDataPath;
            this.dataCenterUri = dataCenterUri;
            this.dataLoader = DataLoaderFactory.CreateDataLoader(DataSourceType.TaoBao1, pluginHelper, srcDataPath, csvDataPath);//, dataCenterUri);
            this.updateFillUp = updateFillUp;
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();
            steps.Add(new Step_TradingDay(dataLoader));
            steps.Add(new Step_InstrumentInfo(pluginHelper.PluginDirPath, csvDataPath));
            this.preparer = new DataUpdateUtils(csvDataPath, dataLoader.LoadAllInstruments(), dataLoader.LoadTradingDayReader().GetAllTradingDays(), new UpdatedInfo_Csv(csvDataPath));

            GetDayStartTime(steps);
            GetTickSteps(steps);
            GetKLineDataSteps(steps);
            return steps;
        }

        private void GetDayStartTime(List<IStep> steps)
        {
            List<CodeInfo> codes = dataLoader.LoadAllInstruments();
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