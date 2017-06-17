using com.wer.sc.data.reader.cache;
using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 获得更新tick数据的所有步骤
    /// 每个步骤更新一个合约10天的tick数据
    /// </summary>
    public class StepGetter_UpdateTickData
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private IPlugin_HistoryData historyData;

        private IDataStore dataStore;

        private bool isFillUp;

        public StepGetter_UpdateTickData(IPlugin_HistoryData historyData, IDataStore dataStore, bool isFillUp)
        {
            this.historyData = historyData;
            this.dataStore = dataStore;
            this.isFillUp = isFillUp;
        }

        public List<IStep> GetSteps()
        {
            List<IStep> steps = new List<IStep>();
            AddSteps_TickData(steps);
            return steps;
        }

        private void AddSteps_TickData(List<IStep> steps)
        {
            ITickDataStore tickDataStore = dataStore.CreateTickDataStore();

            List<CodeInfo> allInstruments = historyData.GetInstruments();
            List<int> allTradingDays = historyData.GetTradingDays();

            TradingDayCache tradingDayCache = new TradingDayCache(allTradingDays);

            for (int i = 0; i < allInstruments.Count; i++)
            {
                CodeInfo instrument = allInstruments[i];
                List<int> storedAllDays = tickDataStore.GetAllDays(instrument.Code);
                List<int> allDays = GetAllNotUpdateTickData(instrument, tradingDayCache, storedAllDays, isFillUp);
                if (allDays == null)
                    continue;
                AddSteps_TickData_Instrument(steps, instrument.Code, allDays);
            }
        }

        private List<int> GetAllNotUpdateTickData(CodeInfo codeInfo, TradingDayCache allTradingDayCache, List<int> storedDays, bool isFillUp)
        {
            if (isFillUp)
            {
                HashSet<int> set = new HashSet<int>(storedDays);
                IList<int> codeAllTradingDays = GetCodeTradingDays(codeInfo, allTradingDayCache);
                List<int> allNotUpdateTickData = new List<int>(set.Count);
                for (int i = 0; i < codeAllTradingDays.Count; i++)
                {
                    int day = codeAllTradingDays[i];
                    if (!set.Contains(day))
                        allNotUpdateTickData.Add(day);
                }
                return allNotUpdateTickData;
            }
            else
            {
                int startIndex;
                if (storedDays.Count == 0)
                {
                    startIndex = allTradingDayCache.GetTradingDayIndex(codeInfo.Start, false);
                }
                else
                {
                    storedDays.Sort();
                    int lastUpdateIndex = allTradingDayCache.GetTradingDayIndex(storedDays[storedDays.Count - 1]);
                    if (lastUpdateIndex < 0)
                    {
                        //TODO 不应该出现这种情况，TODO 写日志
                    }
                    startIndex = lastUpdateIndex + 1;
                }
                if (startIndex < 0 || startIndex >= allTradingDayCache.GetAllTradingDays().Count)
                    return null;

                int endIndex;
                if (codeInfo.End <= 0)
                    endIndex = allTradingDayCache.GetAllTradingDays().Count - 1;
                else
                    endIndex = allTradingDayCache.GetTradingDayIndex(codeInfo.End, true);
                if (endIndex < startIndex)
                    return null;
                return allTradingDayCache.GetAllTradingDays().GetRange(startIndex, endIndex - startIndex + 1);
            }
        }

        private IList<int> GetCodeTradingDays(CodeInfo codeInfo, TradingDayCache allTradingCache)
        {
            return allTradingCache.GetTradingDays(codeInfo.Start, codeInfo.End);
        }

        private void AddSteps_TickData_Instrument(List<IStep> steps, string code, List<int> updateDays)
        {
            int stepCount = updateDays.Count / DAYS_EVERYTICKSTEP;
            int lastStepUpdateCount = updateDays.Count % DAYS_EVERYTICKSTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYTICKSTEP;
            List<int> openDates = updateDays;
            ITickDataStore tickDataStore = dataStore.CreateTickDataStore();
            for (int i = 0; i < stepCount; i++)
            {
                IStep step;
                if (i != stepCount - 1)
                    step = new Step_UpdateTickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), historyData, tickDataStore);
                else
                    step = new Step_UpdateTickData(code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), historyData, tickDataStore);
                steps.Add(step);
            }
        }
    }
}
