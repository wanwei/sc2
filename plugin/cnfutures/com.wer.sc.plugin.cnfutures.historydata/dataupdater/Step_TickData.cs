using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_TickData : IStep
    {
        private string code;

        private List<int> dates;

        private DataUpdateHelper dataLoader;

        public Step_TickData(string code, List<int> dates, DataUpdateHelper dataLoader)
        {
            this.code = code;
            this.dates = dates;
            this.dataLoader = dataLoader;
        }

        public int ProgressStep
        {
            get
            {
                return dates.Count * 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + GetDesc();
            }
        }

        private string GetDesc()
        {
            return code + "的Tick数据：" + dates[0] + "-" + dates[dates.Count - 1];
        }

        public string Proceed()
        {
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                Step_TickData_CodeDate step_tickData = new Step_TickData_CodeDate(dataLoader, code, date);
                step_tickData.Proceed();
            }
            return "更新完毕" + GetDesc();
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}
