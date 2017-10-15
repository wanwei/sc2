using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.utils;
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
        private List<int> newTradingDays;
        private List<int> tradingDays;

        private ITradingDayStore tradingDayStore;

        public Step_UpdateTradingDays(IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.tradingDayStore = dataStore.CreateTradingDayStore();
            this.tradingDays = historyData.GetTradingDays();
            List<int> historyTradingDays = tradingDayStore.Load();

            this.newTradingDays = new List<int>();
            CacheUtils_TradingDay cache = new CacheUtils_TradingDay(historyTradingDays);
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                if (!cache.IsTrade(tradingDay))
                    newTradingDays.Add(tradingDay);
            }
            newTradingDays.Sort();
        }

        public List<int> NewTradingDays
        {
            get { return newTradingDays; }
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