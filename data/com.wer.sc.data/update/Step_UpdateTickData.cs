using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_UpdateTickData : IStep
    {
        private string code;

        private List<int> tradingDays;

        private IPlugin_HistoryData historyData;

        private ITickDataStore tickDataStore;

        public Step_UpdateTickData(string code, List<int> openDates, IPlugin_HistoryData historyData, ITickDataStore tickDataStore)
        {
            this.code = code;
            this.tradingDays = openDates;
            this.historyData = historyData;
            this.tickDataStore = tickDataStore;
        }

        public int ProgressStep
        {
            get
            {
                return 5 * tradingDays.Count;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的" + tradingDays[0] + "-" + tradingDays[tradingDays.Count - 1] + "的Tick数据";
            }
        }

        public override string ToString()
        {
            return StepDesc;
        }

        public string Proceed()
        {
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                TickData tickData = (TickData)historyData.GetTickData(code, tradingDay);
                if (tickData != null)
                    tickDataStore.Save(code, tradingDay, tickData);
            }
            return StepDesc + "完成";
        }
    }
}