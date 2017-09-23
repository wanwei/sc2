using com.wer.sc.data.forward;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略报告
    /// </summary>
    public class StrategyReport : IStrategyReport
    {
        internal string code;

        internal int startDate;

        internal int endDate;

        internal ForwardPeriod forwardPeriod;

        internal IParameters parameters;

        internal IStrategyResult strategyResult;

        internal IStrategyTrader strategyTrader;

        public string Code
        {
            get
            {
                return code;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }
        }

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public IStrategyResult StrategyResult
        {
            get
            {
                return strategyResult;
            }
        }

        public IStrategyTrader StrategyTrader
        {
            get
            {
                return strategyTrader;
            }
        }
    }
}
