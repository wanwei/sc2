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
    /// 更新所有的股票或期货信息
    /// 股票或期货信息是放在Resources里
    /// </summary>
    public class Step_UpdateInstrument : IStep
    {
        private IInstrumentStore instrumentStore;

        private List<CodeInfo> codes;

        public Step_UpdateInstrument(IPlugin_HistoryData historyData, IDataStore dataStore)
        {
            this.codes = historyData.GetInstruments();
            this.instrumentStore = dataStore.CreateInstrumentStore();
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
                return "更新品种信息";
            }
        }

        public List<CodeInfo> Instruments
        {
            get
            {
                return codes;
            }
        }

        public string Proceed()
        {
            instrumentStore.Save(codes);
            return "期货信息更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}
