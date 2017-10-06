using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_MainContract : IStep
    {
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
                return "更新主力合约";
            }
        }

        public string Proceed()
        {
            throw new NotImplementedException();
        }
    }
}
