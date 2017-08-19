using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils.param;

namespace com.wer.sc.strategy
{
    public abstract class StrategyAbstract : IStrategy
    {
        private KLinePeriod defaultMainPeriod;

        private StrategyHelper strategyHelper;

        private IParameters parameters = ParameterFactory.CreateParameters();

        public abstract StrategyReferedPeriods GetStrategyPeriods();

        public abstract void OnBar(IRealTimeDataReader currentData);

        public abstract void OnTick(IRealTimeDataReader currentData);

        public abstract void StrategyEnd();

        public abstract void StrategyStart();

        public StrategyHelper StrategyHelper
        {
            get { return strategyHelper; }
            set { strategyHelper = value; }
        }

        public KLinePeriod DefaultMainPeriod
        {
            get
            {
                return defaultMainPeriod;
            }

            set
            {
                defaultMainPeriod = value;
            }
        }        

        public virtual IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }
    }
}