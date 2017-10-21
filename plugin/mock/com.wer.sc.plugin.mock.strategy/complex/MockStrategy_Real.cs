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
    [Strategy("MOCK.STRATEGY.COMPLEX.REAL", "复杂策略", "复杂策略，测试专用")]
    public class MockStrategy_Real : StrategyAbstract
    {

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {
            throw new NotImplementedException();
        }
    }
}
