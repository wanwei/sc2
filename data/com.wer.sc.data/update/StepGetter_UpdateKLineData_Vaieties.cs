using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.store;
using com.wer.sc.plugin;

namespace com.wer.sc.data.update
{
    public class StepGetter_UpdateKLineData_Vaieties
    {
        private IPlugin_HistoryData historyData;
        private IDataStore dataStore;
        private List<KLinePeriod> klinePeriods;
        private IKLineDataStore klineDataStore;

        public StepGetter_UpdateKLineData_Vaieties(IPlugin_HistoryData historyData, IDataStore dataStore, List<KLinePeriod> storeKLinePeriods) 
        {
            this.klineDataStore = dataStore.CreateKLineDataStore();
            this.historyData = historyData;
            this.dataStore = dataStore;
            this.klinePeriods = storeKLinePeriods;
        }

        public List<IStep> GetSteps()
        {
            List<IStep> steps = new List<IStep>();
            steps.AddRange(AddStep_Varieties());
            //AddSteps_KLineData(steps);
            return steps;
        }

        private List<IStep> AddStep_Varieties()
        {
            List<IStep> steps = new List<IStep>();
            List<CodeInfo> allCodes = historyData.GetInstruments();
            List<CodeInfo> updateCodes = FilterCodeInfo(allCodes, new string[] { "RB", "HC", "BU" });
            for (int i = 0; i < updateCodes.Count; i++)
            {
                CodeInfo codeInfo = updateCodes[i];
                AddStep(codeInfo, steps);
            }
            return steps;
        }

        private void AddStep(CodeInfo codeInfo, IList<IStep> steps)
        {
            IList<int> days = historyData.GetKLineDataDays(codeInfo.Code);
            int start = codeInfo.Start;
            if (start <= 0)
                start = days[0];

            int end = codeInfo.End;
            if (end <= 0)
                end = days[days.Count - 1];
            
            for (int i = 0; i < klinePeriods.Count; i++)
            {
                KLinePeriod period = klinePeriods[i];
                Step_UpdateKLineData step = new Step_UpdateKLineData(codeInfo.Code, start, end, period, historyData, klineDataStore);
                steps.Add(step);
            }
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

    }
}
