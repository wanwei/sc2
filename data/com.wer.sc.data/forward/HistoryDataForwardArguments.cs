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

        private ForwardReferedPeriods referedPeriods;

        private bool isTickForward;

        private KLinePeriod forwardKLinePeriod;

        public HistoryDataForwardArguments()
        {
        }

        public HistoryDataForwardArguments(int startDate, int endDate, ForwardReferedPeriods referedPeriods, bool isTickForward, KLinePeriod forwardKLinePeriod)
        {
            this.startDate = startDate;
            this.endDate = endDate;
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

        public ForwardReferedPeriods ReferedPeriods
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
