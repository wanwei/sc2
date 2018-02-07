using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.sample.trade
{
    public class Strategy_Trade_MA : StrategyAbstract
    {
        private IStrategyTrader trader;

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            trader = this.StrategyOperator.Trader;            
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {

        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}
