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
    /// <summary>
    /// 更新主合约信息
    /// </summary>
    public class Step_UpdateMainFutures : IStep
    {
        private IMainContractStore store;

        private IPlugin_HistoryData historyData;

        public Step_UpdateMainFutures(IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.store = dataStore.CreateMainContractStore();
            this.historyData = historyData;
        }

        public int ProgressStep
        {
            get
            {
                return 1;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新主力合约信息";
            }
        }

        public string Proceed()
        {
            this.store.Save(this.historyData.GetMainContractInfos());
            return "";
        }
    }
}
