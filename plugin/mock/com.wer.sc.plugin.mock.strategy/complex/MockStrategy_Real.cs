using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;

namespace com.wer.sc.plugin.mock.strategy.complex
{
    //[Strategy("MOCK.STRATEGY.COMPLEX.REAL", "复杂策略", "复杂策略，测试专用")]
    public class MockStrategy_Real : StrategyAbstract
    {

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            throw new NotImplementedException();
        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {
            throw new NotImplementedException();
        }
    }
}
