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

        private bool overwrite;

        private IPlugin_HistoryData historyData;

        private ITickDataStore tickDataStore;

        private UpdatedDataInfo updatedDataInfo;

        private IUpdateInfoStore updateInfoStore;

        public Step_UpdateTickData(string code, List<int> tradingDays, IPlugin_HistoryData historyData, ITickDataStore tickDataStore)
        {
            this.code = code;
            this.tradingDays = tradingDays;
            this.historyData = historyData;
            this.tickDataStore = tickDataStore;
        }

        public Step_UpdateTickData(string code, List<int> tradingDays, IPlugin_HistoryData historyData, ITickDataStore tickDataStore,bool overwrite)
        {
            this.code = code;
            this.tradingDays = tradingDays;
            this.historyData = historyData;
            this.tickDataStore = tickDataStore;
            this.overwrite = overwrite;
        }


        public Step_UpdateTickData(string code, List<int> tradingDays, IPlugin_HistoryData historyData, ITickDataStore tickDataStore, UpdatedDataInfo updatedDataInfo, IUpdateInfoStore updateInfoStore) : this(code, tradingDays, historyData, tickDataStore)
        {
            this.updatedDataInfo = updatedDataInfo;
            this.updateInfoStore = updateInfoStore;
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
            if (tradingDays == null || tradingDays.Count == 0)
                return "";
            int lastTradingDay = 0;
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                if (tickDataStore.Exist(code, tradingDay)) {
                    if (overwrite)
                    {
                        TickData wtickData = (TickData)historyData.GetTickData(code, tradingDay);
                        tickDataStore.Save(code, tradingDay, wtickData);
                        continue;
                    }
                    lastTradingDay = tradingDay;
                    continue;
                }
                TickData tickData = (TickData)historyData.GetTickData(code, tradingDay);
                if (tickData != null)
                {
                    tickDataStore.Save(code, tradingDay, tickData);
                    lastTradingDay = tradingDay;
                }
            }
            if (tradingDays.Count > 0 && updatedDataInfo != null)
            {
                updatedDataInfo.WriteUpdateInfo_Tick(code, lastTradingDay);
                updateInfoStore.Save(updatedDataInfo);
            }
            return StepDesc + "完成";
        }
    }
}