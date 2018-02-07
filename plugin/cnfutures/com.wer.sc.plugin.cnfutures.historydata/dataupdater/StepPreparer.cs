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
            //return GetAllStep_Varieties();
            if (dataProvider.GetAppointUpdate() != null)
                return GetAllStep_UpdateAppoint();
            List<IStep> steps = new List<IStep>();

            UpdatedDataInfo updatedDataInfo = new UpdatedDataInfo(targetDataPath);

            Step_CodeInfo step_CodeInfo = new Step_CodeInfo(dataUpdateHelper);
            List<CodeInfo> allCodes = step_CodeInfo.GetAllCodes();
            steps.Add(step_CodeInfo);
            steps.Add(new Step_TradingDay(dataUpdateHelper));
            GetTradingTime(steps, allCodes, false);
            //GetTradingTime(steps, allCodes, true);
            GetTickSteps(steps, updatedDataInfo, allCodes);
            GetKLineDataSteps(steps, updatedDataInfo, allCodes);

            //GetTickDataSteps_Day_All(steps, 20180102, allCodes);
            //GetTickDataSteps_Day_All(steps, 20170719, allCodes);
            //GetTickDataSteps_Day_All(steps, 20170720, allCodes);
            //GetTickDataSteps_Day_All(steps, 20170721, allCodes);

            //GetKLineDataSteps_Day(steps, 20180102, allCodes);
            //GetKLineDataSteps_Day(steps, 20170719, allCodes);
            //GetKLineDataSteps_Day(steps, 20170720, allCodes);
            //GetKLineDataSteps_Day(steps, 20170721, allCodes);


            //更新主力合约信息
            Step_MainFutures step_MainFutures = new Step_MainFutures(this.dataUpdateHelper);
            steps.Add(step_MainFutures);
            ///*
            // * 在准备更新的时候会将所有更新信息索引一次
            // * 所以在准备完更新后保存一次
            // */
            //updatedDataInfo.Save();
            return steps;
        }

        private void GetKLineDataSteps_Day(List<IStep> steps, int tradingDay, List<CodeInfo> allCodes)
        {
            for (int i = 0; i < allCodes.Count; i++)
            {
                CodeInfo codeInfo = allCodes[i];
                if (codeInfo.End == 0 || codeInfo.End >= tradingDay)
                {
                    List<int> tradingDays = new List<int>();
                    tradingDays.Add(tradingDay);
                    Step_KLineData step = new Step_KLineData(codeInfo, tradingDays, true, dataUpdateHelper);
                    steps.Add(step);
                }
            }
        }

        private void GetTickDataSteps_Day_All(List<IStep> steps, int tradingDay, List<CodeInfo> allCodes)
        {
            List<CodeInfo> notIndexCodes = GetNotIndexCodes(allCodes);
            List<CodeInfo> indexCodes = GetIndexCodes(allCodes);
            GetTickDataSteps_Day(steps, tradingDay, notIndexCodes);
            GetTickDataSteps_Day(steps, tradingDay, indexCodes);
        }

        private void GetTickDataSteps_Day(List<IStep> steps, int tradingDay, List<CodeInfo> allCodes)
        {
            for (int i = 0; i < allCodes.Count; i++)
            {
                CodeInfo codeInfo = allCodes[i];
                if (codeInfo.End == 0 || codeInfo.End >= tradingDay)
                {
                    Step_TickData_CodeDate step = new Step_TickData_CodeDate(this.dataUpdateHelper, codeInfo, tradingDay, true);
                    steps.Add(step);
                }
            }
        }

        private List<IStep> GetAllStep_Varieties()
        {
            List<IStep> steps = new List<IStep>();
            Step_CodeInfo step_CodeInfo = new Step_CodeInfo(dataUpdateHelper);
            List<CodeInfo> allCodes = step_CodeInfo.GetAllCodes();
            allCodes = FilterCodeInfo(allCodes, new string[] { "RB", "HC", "BU" });
            GetTradingTime(steps, allCodes, true);
            UpdatedDataInfo updatedDataInfo = new UpdatedDataInfo(targetDataPath);

            ITradingDayReader tradingDayReader = dataUpdateHelper.GetAllTradingDayReader();
            for (int i = 0; i < allCodes.Count; i++)
            {
                CodeInfo codeInfo = allCodes[i];
                int startDate = codeInfo.Start;
                if (startDate == 0)
                    startDate = tradingDayReader.FirstTradingDay;

                int endDate = codeInfo.End;
                if (endDate <= 0)
                    endDate = tradingDayReader.LastTradingDay;
                List<int> tradingDays = new List<int>();
                tradingDays.AddRange(tradingDayReader.GetTradingDays(startDate, endDate));
                GetKLineDataSteps(steps, codeInfo, tradingDays, updatedDataInfo);
            }
            return steps;
        }

        private List<CodeInfo> FilterCodeInfo(List<CodeInfo> codes, string[] varieties)
        {
            List<CodeInfo> filteredCodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                if (IsBelongVarieties(code, varieties))
                    filteredCodes.Add(code);
            }
            return filteredCodes;
        }

        private bool IsBelongVarieties(CodeInfo code, string[] varieties)
        {
            for (int i = 0; i < varieties.Length; i++)
            {
                string variety = varieties[i];
                if (code.Code.StartsWith(variety))
                    return true;
            }
            return false;
        }

        private List<IStep> GetAllStep_UpdateAppoint()
        {
            List<IStep> steps = new List<IStep>();

            //更新一次所有开盘时间
            //List<CodeInfo> allCodes = dataUpdateHelper.GetAllCodes();
            //GetTradingTime(steps, allCodes, true);

            List<AppointUpdate> aps = dataProvider.GetAppointUpdate();
            for (int i = 0; i < aps.Count; i++)
            {
                AppointUpdate ap = aps[i];
                string code = ap.Code;
                int date = ap.Date;
                CodeInfo codeInfo = dataUpdateHelper.GetCodeInfo(code);
                if (ap.UpdateTick)
                {
                    Step_TickData_CodeDate step = new Step_TickData_CodeDate(dataUpdateHelper, codeInfo, date, true);
                    steps.Add(step);
                }
                if (ap.UpdateKLine)
                {
                    //int prevDate = dataUpdateHelper.GetNewTradingDayCache().GetPrevTradingDay(date);
                    //ITickData newTickData = dataUpdateHelper.GetNewTickData(code, prevDate);
                    //if (newTickData == null) {                        
                    //    continue;
                    //}
                    //float lastEndPrice = newTickData.Arr_Price[newTickData.Length - 1];
                    //int lastEndHold = newTickData.Arr_Hold[newTickData.Length - 1];
                    float lastEndPrice = -1;
                    int lastEndHold = -1;
                    Step_KLineData_OneDay step = new Step_KLineData_OneDay(dataUpdateHelper, codeInfo, ap.Date, KLinePeriod.KLinePeriod_1Minute, lastEndPrice, lastEndHold, true);
                    steps.Add(step);
                }
            }
            return steps;
        }

        private void GetTradingSession(List<IStep> steps, List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_TradingSession(codes[i].Code, dataUpdateHelper));
            }
        }

        private void GetTradingTime(List<IStep> steps, List<CodeInfo> codes, bool forceUpdate)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_TradingTime(codes[i].Code, dataUpdateHelper, forceUpdate));
            }
        }

        private void GetTickSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo, List<CodeInfo> allCodes)
        {
            //List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            //必须要先更新合约，再更新指数
            GetTickSteps(steps, GetNotIndexCodes(allCodes), updatedDataInfo);
            GetTickSteps(steps, GetIndexCodes(allCodes), updatedDataInfo);
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
                        //updatedDataInfo.WriteUpdateInfo_Tick(code, tradingDayReader.LastTradingDay);
                        continue;
                    }

                    int startDate = notUpdatedTradingDays[0];
                    int lastUpdateDate = tradingDayReader.GetPrevTradingDay(startDate);
                    //updatedDataInfo.WriteUpdateInfo_Tick(code, lastUpdateDate);
                }
                GetTickSteps(steps, codeInfo, notUpdatedTradingDays, updatedDataInfo);
            }
        }

        private void GetTickSteps(List<IStep> steps, CodeInfo codeInfo, List<int> tradingDays, UpdatedDataInfo updatedDataInfo)
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
                    step = new Step_TickData(codeInfo, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataUpdateHelper, null, updateFillUp);
                else
                    step = new Step_TickData(codeInfo, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataUpdateHelper, updatedDataInfo, updateFillUp);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo, List<CodeInfo> allCodes)
        {
            List<CodeInfo> codes = allCodes;
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                string code = codeInfo.Code;
                //List<int> notUpdatedTradingDays = new List<int>();
                //notUpdatedTradingDays.AddRange(this.dataUpdateHelper.GetAllHolidays());
                //notUpdatedTradingDays.Remove(20171009);
                //Delete(code, notUpdatedTradingDays);
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
                GetKLineDataSteps(steps, codeInfo, notUpdatedTradingDays, updatedDataInfo);
            }
        }

        private void Delete(string code, List<int> dates)
        {
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                string path = dataUpdateHelper.GetPath_KLineData(code, date, KLinePeriod.KLinePeriod_1Minute);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, CodeInfo codeInfo, List<int> tradingDays, UpdatedDataInfo updatedDataInfo)
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
                    step = new Step_KLineData(codeInfo, openDates.GetRange(i * DAYS_EVERYKLINESTEP, DAYS_EVERYKLINESTEP), dataUpdateHelper, null, updateFillUp);
                else
                    step = new Step_KLineData(codeInfo, openDates.GetRange(i * DAYS_EVERYKLINESTEP, lastStepUpdateCount), dataUpdateHelper, updatedDataInfo, updateFillUp);
                steps.Add(step);
            }
        }
    }
}