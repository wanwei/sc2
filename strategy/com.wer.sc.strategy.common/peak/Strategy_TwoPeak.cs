using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.peak
{
    public class Strategy_TwoPeak : StrategyAbstract
    {
        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
            
        }
    }
}
