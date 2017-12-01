using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    public class MockStrategy_ReferOtherCode : StrategyAbstract
    {
        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            //StrategyOperator.Trader.Open()
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
            
        }
    }
}
