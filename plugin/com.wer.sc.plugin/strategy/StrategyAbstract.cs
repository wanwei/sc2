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
            //this.Parameters.AddParameter(PARAMETER_PERIOD, "计算周期", "计算周期", utils.param.ParameterType.OBJECT, KLinePeriod.KLinePeriod_1Minute);
        }

        public virtual void OnStart(Object sender, IStrategyOnStartArgument argument)
        {

        }

        public virtual void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {

        }

        public abstract void OnBar(Object sender, IStrategyOnBarArgument currentData);

        public abstract void OnTick(Object sender, IStrategyOnTickArgument currentData);

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
        protected Dictionary<string, object> dic_Key_Data = new Dictionary<string, object>();

        protected void AddData(String key, object obj)
        {
            this.dic_Key_Data.Add(key, obj);
        }

        public Object GetData(string key)
        {
            if (dic_Key_Data.ContainsKey(key))
                return dic_Key_Data[key];
            return null;
        }

        public object GetParameter(string parameterName)
        {
            return this.parameters.GetParameterValue(parameterName);
        }

        public virtual StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public List<string> GetStrategyReferedCodes(string code)
        {
            return null;
        }

        public virtual IList<IStrategy> GetReferedStrategies()
        {
            return null;
        }
    }
}