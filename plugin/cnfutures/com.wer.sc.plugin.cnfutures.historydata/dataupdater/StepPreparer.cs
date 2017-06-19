using com.wer.sc.data;
using com.wer.sc.data.reader;
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

            steps.Add(new Step_TradingDay(dataUpdateHelper));
            steps.Add(new Step_CodeInfo(dataUpdateHelper));

            GetTradingSession(steps, dataUpdateHelper.GetNewCodes());
            GetTickSteps(steps);
            GetKLineDataSteps(steps);
            return steps;
        }

        private void GetTradingSession(List<IStep> steps, List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_TradingSession(codes[i].Code, dataUpdateHelper));
            }
        }

        private void GetTickSteps(List<IStep> steps)
        {
            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            //必须要先更新合约，再更新指数
            GetTickSteps(steps, GetNotIndexCodes(codes));
            GetTickSteps(steps, GetIndexCodes(codes));
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

        private void GetTickSteps(List<IStep> steps, List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i].Code;
                List<int> notUpdatedTradingDays = dataUpdateHelper.GetNotUpdateTradingDays_TickData(code, updateFillUp);
                GetTickSteps(steps, code, notUpdatedTradingDays);
            }
        }

        private void GetTickSteps(List<IStep> steps, string code, List<int> tradingDays)
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
                    step = new Step_TickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataUpdateHelper);
                else
                    step = new Step_TickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataUpdateHelper);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps)
        {
            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i].Code;
                List<int> notUpdatedTradingDays = dataUpdateHelper.GetNotUpdateTradingDays_KLineData(code, updateFillUp);
                GetKLineDataSteps(steps, code, notUpdatedTradingDays);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, string code, List<int> tradingDays)
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
                    step = new Step_KLineData(code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, DAYS_EVERYKLINESTEP), dataUpdateHelper);
                else
                    step = new Step_KLineData(code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, lastStepUpdateCount), dataUpdateHelper);
                steps.Add(step);
            }
        }
    }
}