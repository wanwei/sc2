using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class HistoryDataForwardArguments
    {
        private int startDate;

        private int endDate;

        private StrategyReferedPeriods referedPeriods;

        private bool isTickForward;

        private KLinePeriod forwardKLinePeriod;

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

        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }

            set
            {
                referedPeriods = value;
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
