using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.update;
using com.wer.sc.plugin.cnfutures.config;
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

        //private PluginHelper pluginHelper;

        private string srcDataPath;

        private string targetDataPath;

        private DataUpdateHelper dataUpdateHelper;

        private IDataProvider dataProvider;

        private bool updateFillUp;

        /// <summary>
        /// 创建一个更新准备器
        /// </summary>
        /// <param name="pluginHelper"></param>
        /// <param name="dataProvider"></param>
        /// <param name="srcDataPath"></param>
        /// <param name="targetDataPath"></param>
        /// <param name="updateFillUp"></param>
        public StepPreparer(string pluginPath, string srcDataPath, string targetDataPath, bool updateFillUp, IDataProvider dataProvider)
        {
            this.srcDataPath = srcDataPath;
            this.targetDataPath = targetDataPath;
            this.updateFillUp = updateFillUp;
            this.dataProvider = dataProvider;
            this.dataUpdateHelper = new DataUpdateHelper(pluginPath, new UpdatedDataLoader(pluginPath), dataProvider);
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();

            UpdatedDataInfo updatedDataInfo = new UpdatedDataInfo(targetDataPath);

            steps.Add(new Step_TradingDay(dataUpdateHelper));
            steps.Add(new Step_CodeInfo(dataUpdateHelper));

            GetTradingSession(steps, dataUpdateHelper.GetNewCodes());
            GetTickSteps(steps, updatedDataInfo);
            GetKLineDataSteps(steps, updatedDataInfo);

            /*
             * 在准备更新的时候会将所有更新信息索引一次
             * 所以在准备完更新后保存一次
             */
            updatedDataInfo.Save();
            return steps;
        }

        private void GetTradingSession(List<IStep> steps, List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_TradingSession(codes[i].Code, dataUpdateHelper));
            }
        }

        private void GetTickSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo)
        {
            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            //必须要先更新合约，再更新指数
            GetTickSteps(steps, GetNotIndexCodes(codes), updatedDataInfo);
            GetTickSteps(steps, GetIndexCodes(codes), updatedDataInfo);
        }

        private List<CodeInfo> GetIndexCodes(List<CodeInfo> codes)
        {
            List<CodeInfo> indexCodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                CodeIdParser parser = new CodeIdParser(codeInfo.Code);
                if (parser.IsIndex)
                    indexCodes.Add(codeInfo);
            }
            return indexCodes;
        }

        private List<CodeInfo> GetNotIndexCodes(List<CodeInfo> codes)
        {
            List<CodeInfo> notIndexCodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                CodeIdParser parser = new CodeIdParser(codeInfo.Code);
                if (!parser.IsIndex)
                    notIndexCodes.Add(codeInfo);
            }
            return notIndexCodes;
        }

        private void GetTickSteps(List<IStep> steps, List<CodeInfo> codes, UpdatedDataInfo updatedDataInfo)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                string code = codeInfo.Code;
                List<int> notUpdatedTradingDays;

                ITradingDayReader tradingDayReader = dataUpdateHelper.GetAllTradingDayReader();
                int lastUpdatedTickDate = updatedDataInfo.GetLastUpdatedTickData(code);
                if (!updateFillUp && lastUpdatedTickDate >= 0)
                {
                    int endDate;
                    if (codeInfo.End <= 0)
                        endDate = tradingDayReader.LastTradingDay;
                    else if (codeInfo.End < tradingDayReader.LastTradingDay)
                        endDate = codeInfo.End;
                    else
                        endDate = tradingDayReader.LastTradingDay;
                    
                    if (lastUpdatedTickDate >= endDate)
                        continue;
                    int startDate = tradingDayReader.GetNextTradingDay(lastUpdatedTickDate);
                    if (startDate < 0)
                        continue;
                    IList<int> days = tradingDayReader.GetTradingDays(startDate, endDate);
                    if (days.Count == 0)
                        continue;
                    notUpdatedTradingDays = new List<int>(days);
                }
                else
                {
                    notUpdatedTradingDays = dataUpdateHelper.GetNotUpdateTradingDays_TickData(code, updateFillUp);
                    if (notUpdatedTradingDays == null || notUpdatedTradingDays.Count == 0)
                    {
                        updatedDataInfo.WriteUpdateInfo_Tick(code, tradingDayReader.LastTradingDay);
                        continue;
                    }

                    int startDate = notUpdatedTradingDays[0];
                    int lastUpdateDate = tradingDayReader.GetPrevTradingDay(startDate);
                    updatedDataInfo.WriteUpdateInfo_Tick(code, lastUpdateDate);
                }
                GetTickSteps(steps, code, notUpdatedTradingDays, updatedDataInfo);
            }
        }

        private void GetTickSteps(List<IStep> steps, string code, List<int> tradingDays, UpdatedDataInfo updatedDataInfo)
        {
            int stepCount = tradingDays.Count / DAYS_EVERYTICKSTEP;
            int lastStepUpdateCount = tradingDays.Count % DAYS_EVERYTICKSTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYTICKSTEP;
            List<int> openDates = tradingDays;
            for (int i = 0; i < stepCount; i++)
            {
                IStep step;
                if (i != stepCount - 1)
                    step = new Step_TickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataUpdateHelper, null, updateFillUp);
                else
                    step = new Step_TickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataUpdateHelper, updatedDataInfo, updateFillUp);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo)
        {
            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                string code = codeInfo.Code;
                List<int> notUpdatedTradingDays;

                ITradingDayReader tradingDayReader = dataUpdateHelper.GetAllTradingDayReader();
                int lastUpdatedKLineDate = updatedDataInfo.GetLastUpdatedKLineData(code, KLinePeriod.KLinePeriod_1Minute);
                if (!updateFillUp && lastUpdatedKLineDate >= 0)
                {                    
                    int endDate;
                    if (codeInfo.End <= 0)
                        endDate = tradingDayReader.LastTradingDay;
                    else if (codeInfo.End < tradingDayReader.LastTradingDay)
                        endDate = codeInfo.End;
                    else
                        endDate = tradingDayReader.LastTradingDay;

                    if (lastUpdatedKLineDate >= endDate)
                        continue;
                    int startDate = tradingDayReader.GetNextTradingDay(lastUpdatedKLineDate);
                    if (startDate < 0)
                        continue;
                    IList<int> days = tradingDayReader.GetTradingDays(startDate, endDate);
                    if (days.Count == 0)
                        continue;
                    notUpdatedTradingDays = new List<int>(days);
                }
                else
                {
                    notUpdatedTradingDays = dataUpdateHelper.GetNotUpdateTradingDays_KLineData(code, updateFillUp);
                    if (notUpdatedTradingDays == null || notUpdatedTradingDays.Count == 0)
                    {
                        updatedDataInfo.WriteUpdateInfo_KLine(code, KLinePeriod.KLinePeriod_1Minute, tradingDayReader.LastTradingDay);
                        continue;
                    }
                    int startDate = notUpdatedTradingDays[0];
                    int lastUpdateDate = tradingDayReader.GetPrevTradingDay(startDate);
                    updatedDataInfo.WriteUpdateInfo_KLine(code, KLinePeriod.KLinePeriod_1Minute, lastUpdateDate);
                }
                GetKLineDataSteps(steps, code, notUpdatedTradingDays, updatedDataInfo);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, string code, List<int> tradingDays, UpdatedDataInfo updatedDataInfo)
        {
            int stepCount = tradingDays.Count / DAYS_EVERYKLINESTEP;
            int lastStepUpdateCount = tradingDays.Count % DAYS_EVERYKLINESTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYKLINESTEP;

            List<int> openDates = tradingDays;

            for (int i = 0; i < stepCount; i++)
            {
                Step_KLineData step;
                if (i != stepCount - 1)
                    step = new Step_KLineData(code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, DAYS_EVERYKLINESTEP), dataUpdateHelper, null, updateFillUp);
                else
                    step = new Step_KLineData(code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, lastStepUpdateCount), dataUpdateHelper, updatedDataInfo, updateFillUp);
                steps.Add(step);
            }
        }
    }
}