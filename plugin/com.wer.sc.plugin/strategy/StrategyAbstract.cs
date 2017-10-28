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
        public const string PARAMETER_PERIOD = "PARAMETER_PERIOD";

        private KLinePeriod defaultMainPeriod = KLinePeriod.KLinePeriod_1Minute;

        private IStrategyOperator strategyHelper;

        private IParameters parameters = ParameterFactory.CreateParameters();

        public StrategyAbstract()
        {
            this.Parameters.AddParameter(PARAMETER_PERIOD, "计算周期", "计算周期", utils.param.ParameterType.OBJECT, KLinePeriod.KLinePeriod_1Minute);
        }

        public abstract void OnStrategyStart(Object sender, StrategyOnStartArgument argument);

        public abstract void OnStrategyEnd(Object sender, StrategyOnEndArgument argument);

        public abstract void OnBar(Object sender, StrategyOnBarArgument currentData);

        public abstract void OnTick(Object sender, StrategyOnTickArgument currentData);

        public IStrategyOperator StrategyOperator
        {
            get { return strategyHelper; }
            set { strategyHelper = value; }
        }

        public KLinePeriod MainKLinePeriod
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

        public virtual StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public virtual IList<IStrategy> GetReferedStrategies()
        {
            return null;
        }
    }
}