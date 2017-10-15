using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils.param;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy
{
    public abstract class StrategyAbstract : IStrategy
    {
        private KLinePeriod defaultMainPeriod = KLinePeriod.KLinePeriod_1Minute;

        private IStrategyHelper strategyHelper;

        private IParameters parameters = ParameterFactory.CreateParameters();

        public abstract StrategyReferedPeriods GetStrategyPeriods();

        public abstract void OnBar(IRealTimeDataReader_Code currentData);

        public abstract void OnTick(IRealTimeDataReader_Code currentData);

        public abstract void StrategyEnd();

        public abstract void StrategyStart();

        public IStrategyHelper StrategyHelper
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

        public virtual IList<IStrategy> GetReferedStrategies()
        {
            return null;
        }
    }
}