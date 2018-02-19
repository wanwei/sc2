using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class UpdateStepGetter_CnStock : IUpdateHelper
    {
        private PluginHelper pluginHelper;
        private string pluginSrcDataPath;
        private string srcDataPath;
        private bool updateFillUp;
        private StepPreparer stepPreparer;

        public UpdateStepGetter_CnStock(PluginHelper pluginHelper, string srcDataPath, string pluginSrcDataPath, bool updateFillUp)
        {
            this.pluginHelper = pluginHelper;
            this.srcDataPath = srcDataPath;
            this.pluginSrcDataPath = pluginSrcDataPath;
            this.updateFillUp = updateFillUp;
            this.stepPreparer = new StepPreparer(this, pluginHelper);
        }

        public List<IStep> GetSteps()
        {
            return stepPreparer.GetAllSteps();
        }
    }
}
