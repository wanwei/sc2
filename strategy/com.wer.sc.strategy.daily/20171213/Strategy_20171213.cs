using com.wer.sc.strategy;
using com.wer.sc.strategy.common;
using com.wer.sc.strategy.common.zigzag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.daily._20171213
{
    public class Strategy_20171213 : StrategyAbstract
    {
        private List<IStrategy> referedStrategies = new List<IStrategy>();
        private Strategy_Zigzag strategy;
        public Strategy_20171213()
        {            
            strategy = new Strategy_Zigzag();
            referedStrategies.Add(strategy);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {

        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
        }
    }
}