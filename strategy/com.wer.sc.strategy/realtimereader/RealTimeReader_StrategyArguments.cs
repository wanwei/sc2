using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    public class RealTimeReader_StrategyArguments
    {
        string code;

        int startDate;

        int endDate;

        ForwardReferedPeriods referedPeriods;

        bool isTickForward;

        KLinePeriod forwardKLinePeriod;

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