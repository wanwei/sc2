using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 记录一支股票或期货的引用周期
    /// </summary>
    public class CodeReferedPeriods
    {
        private string code;

        private ForwardReferedPeriods forwardReferedPeriods;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public ForwardReferedPeriods ForwardReferedPeriods
        {
            get
            {
                return forwardReferedPeriods;
            }

            set
            {
                forwardReferedPeriods = value;
            }
        }
    }
}
