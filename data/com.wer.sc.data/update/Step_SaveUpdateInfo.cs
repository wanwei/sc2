using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_SaveUpdateInfo : IStep
    {
        private UpdatedDataInfo updatedDataInfo;

        public Step_SaveUpdateInfo(UpdatedDataInfo updatedDataInfo)
        {
            this.updatedDataInfo = updatedDataInfo;
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
                return "保存更新信息";
            }
        }

        public string Proceed()
        {
            updatedDataInfo.Save();
            return "";
        }
    }
}
