using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 中国期货数据的执行过程
    /// </summary>
    public class UpdateStepGetter_CnFutures : IUpdateStepGetter
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

        public UpdateStepGetter_CnFutures(PluginHelper pluginHelper, string srcDataPath, string pluginSrcDataPath, string dataCenterUri, bool updateFillUp)
        {
            this.stepPreparer = new StepPreparer(pluginHelper, srcDataPath, pluginSrcDataPath, dataCenterUri, updateFillUp);
        }

        public List<IStep> GetSteps()
        {
            return this.stepPreparer.GetAllSteps();
        }
    }
}