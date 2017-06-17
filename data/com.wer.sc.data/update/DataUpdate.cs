using com.wer.sc.data.datacenter;
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
    /// 数据更新进程
    /// </summary>
    public class DataUpdate : IUpdateHelper
    {
        private bool isCancel;
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }

            set
            {
                this.isCancel = value;
            }
        }

        private StepPreparer preparer;

        public DataUpdate(IPlugin_HistoryData plugin_HistoryData, DataCenter dateCenter, bool isFillUp)
        {
            this.preparer = new StepPreparer(plugin_HistoryData, dateCenter, isFillUp);
        }

        public List<IStep> GetSteps()
        {
            return preparer.GetAllSteps();
        }
    }
}