using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_UpdateTradingTime_Code : IStep
    {
        private ITradingTimeStore store;

        private IPlugin_HistoryData historyData;

        private string code;

        public Step_UpdateTradingTime_Code(string code, IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.code = code;
            this.store = dataStore.CreateTradingTimeStore();
            this.historyData = historyData;
        }

        public int ProgressStep
        {
            get
            {
                return 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新交易时间" + code;
            }
        }

        public string Proceed()
        {
            this.store.Save(code, historyData.GetTradingTime(code));
            return "";
        }
    }
}
