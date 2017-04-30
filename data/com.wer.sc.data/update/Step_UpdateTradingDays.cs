using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 数据更新，更新开盘日期
    /// </summary>
    public class Step_UpdateTradingDays : IStep
    {
        private List<int> tradingDays;

        private ITradingDayStore tradingDayStore;

        public Step_UpdateTradingDays(IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.tradingDayStore = dataStore.CreateTradingDayStore();
            this.tradingDays = historyData.GetTradingDays();
        }

        public List<int> TradingDays
        {
            get
            {
                return tradingDays;
            }
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
                return "更新开盘日数据";
            }
        }

        public string Proceed()
        {
            tradingDayStore.Save(tradingDays);
            return "开盘日数据更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}