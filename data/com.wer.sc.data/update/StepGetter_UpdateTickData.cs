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

            for (int i = 0; i < allInstruments.Count; i++)
            {
                CodeInfo instrument = allInstruments[i];
                List<int> storedAllDays = tickDataStore.GetAllDays(instrument.Code);
                storedAllDays.Sort();
                List<int> allDays = GetAllUpdateTickData(allTradingDays, storedAllDays, isFillUp);
                AddSteps_TickData_Instrument(steps, instrument.Code, allDays);
            }
        }

        private List<int> GetAllUpdateTickData(List<int> allTradingDays, List<int> storedDays, bool isFillUp)
        {
            HashSet<int> set = new HashSet<int>(storedDays);
            List<int> allUpdateTickData = new List<int>(set.Count);
            for (int i = 0; i < allTradingDays.Count; i++)
            {
                int day = allTradingDays[i];
                if (!set.Contains(day))
                    allUpdateTickData.Add(day);
            }
            return allUpdateTickData;
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
