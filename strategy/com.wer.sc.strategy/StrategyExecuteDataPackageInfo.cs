using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略要执行的数据信息
    /// </summary>
    public class StrategyExecuteDataPackageInfo
    {
        private bool choosedByMainContract;        

        private IList<string> codes;

        private int start;

        private int end;

        public bool ChoosedByMainContract
        {
            get
            {
                return choosedByMainContract;
            }

            set
            {
                choosedByMainContract = value;
            }
        }

        public IList<string> Codes
        {
            get
            {
                return codes;
            }

            set
            {
                codes = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }
    }
}
