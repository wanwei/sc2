using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    public class MockStrategy_Parameter : StrategyAbstract
    {
        private const string PARAMKEY_PRINTPERIOD = "PERIOD";

        private const string PARAMKEY_RESULT = "RESULT";

        private List<string> results = new List<string>();

        public MockStrategy_Parameter()
        {
            this.Parameters.AddParameter(PARAMKEY_PRINTPERIOD, "输出周期", "", utils.param.ParameterType.OBJECT, KLinePeriod.KLinePeriod_1Minute);
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            this.results.Clear();
        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {
            this.AddData(PARAMKEY_RESULT, results);
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            for (int i = 0; i < currentData.FinishedBars.Count; i++)
            {
                if (currentData.FinishedBars[i].KLinePeriod.Equals(GetParameter(PARAMKEY_PRINTPERIOD)))
                {
                    IStrategyOnBarInfo bar = currentData.FinishedBars[i];
                    Console.WriteLine(bar.KLinePeriod + ":" + bar.KLineBar);
                    results.Add(bar.KLinePeriod + ":" + bar.KLineBar);
                }
            }
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}
