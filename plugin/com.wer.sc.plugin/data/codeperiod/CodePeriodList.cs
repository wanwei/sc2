using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.codeperiod
{
    /// <summary>
    /// 数据包
    /// </summary>
    public class CodePeriodList : ICodePeriodList
    {
        private List<ICodePeriod> codePeriods = new List<ICodePeriod>();

        public List<ICodePeriod> CodePeriods
        {
            get
            {
                return codePeriods;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < codePeriods.Count; i++)
            {
                sb.Append(codePeriods[i]).Append("\r\n");
            }
            return sb.ToString();
        }
    }
}