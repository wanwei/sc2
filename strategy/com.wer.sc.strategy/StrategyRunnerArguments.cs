using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyRunnerArguments
    {
        private string code;

        private int startDate;

        private int endDate;

        private bool isTickForward;

        private KLinePeriod forwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;

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

        public int StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public bool IsTickForward
        {
            get
            {
                return isTickForward;
            }

            set
            {
                isTickForward = value;
            }
        }

        public KLinePeriod ForwardKLinePeriod
        {
            get
            {
                return forwardKLinePeriod;
            }

            set
            {
                forwardKLinePeriod = value;
            }
        }
    }
}
