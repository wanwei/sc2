using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.param;
using com.wer.sc.data;

namespace com.wer.sc.strategy.mock
{
    /// <summary>
    /// 引用其它策略
    /// </summary>
    public class MockStrategy_ReferOtherStrategy : StrategyAbstract
    {
        private MockStrategy_Ma referedStrategy_MA5;

        private MockStrategy_Ma referedStrategy_MA20;

        private List<IStrategy> referedStrategies = new List<IStrategy>();

        public MockStrategy_ReferOtherStrategy()
        {
            this.referedStrategy_MA5 = new MockStrategy_Ma();
            this.referedStrategy_MA20 = new MockStrategy_Ma();
            this.referedStrategy_MA5.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 5);
            this.referedStrategy_MA20.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 20);
            this.referedStrategies.Add(referedStrategy_MA5);
            this.referedStrategies.Add(referedStrategy_MA20);
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            Console.WriteLine("MA05:" + referedStrategy_MA5.MA + ";" + "MA20:" + referedStrategy_MA20.MA);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }
    }
}