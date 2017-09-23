using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy.common.sample
{
    [Strategy("STRATEGY.SAMPLE.TRADER2", "测试交易策略2", "测试交易策略2", "例子")]
    public class Strategy_Trader2 : StrategyAbstract
    {
        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public override void OnBar(IRealTimeDataReader currentData)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(IRealTimeDataReader currentData)
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
    }
}
