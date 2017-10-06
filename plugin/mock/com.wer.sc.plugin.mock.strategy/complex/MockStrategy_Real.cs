using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;

namespace com.wer.sc.plugin.mock.strategy.complex
{
    [Strategy("MOCK.STRATEGY.COMPLEX.REAL", "复杂策略", "复杂策略，测试专用")]
    public class MockStrategy_Real : StrategyAbstract
    {

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public override void StrategyEnd()
        {
            throw new NotImplementedException();
        }

        public override void StrategyStart()
        {
            throw new NotImplementedException();
        }

        public override void OnBar(IRealTimeDataReader_Code currentData)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(IRealTimeDataReader_Code currentData)
        {
            throw new NotImplementedException();
        }
    }
}
