using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_UpdateTradingSession_Instrument : IStep
    {
        private string code;

        private IPlugin_HistoryData historyData;

        private ITradingSessionStore tradingSessionStore;

        public Step_UpdateTradingSession_Instrument(string code, IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.code = code;
            this.historyData = historyData;
            this.tradingSessionStore = dataStore.CreateTradingSessionStore();
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的交易时间";
            }
        }

        public override string ToString()
        {
            return StepDesc;
        }

        public string Proceed()
        {
            List<TradingSession> dayStartTimes = historyData.GetTradingSessions(code);
            tradingSessionStore.Save(code, dayStartTimes);
            return "更新" + code + "的交易时间完毕";
        }
    }
}