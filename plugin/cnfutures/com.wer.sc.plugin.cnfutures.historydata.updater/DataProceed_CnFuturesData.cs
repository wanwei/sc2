using com.wer.sc.utils.update;
using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
{
    /// <summary>
    /// 中国期货数据的执行过程
    /// </summary>
    public class DataProceed_CnFuturesData : IUpdateStepGetter
    {
        private Boolean isCancel;
        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }

            set
            {
                this.isCancel = value;
            }
        }

        private StepPreparer stepPreparer;

        public DataProceed_CnFuturesData(string srcDataPath, string pluginSrcDataPath, string dataCenterUri, bool updateFillUp)
        {
            this.stepPreparer = new StepPreparer(srcDataPath, pluginSrcDataPath, dataCenterUri, updateFillUp);
        }

        public List<IStep> GetSteps()
        {
            return this.stepPreparer.GetAllSteps();
        }
    }
}