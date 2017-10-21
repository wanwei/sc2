using com.wer.sc.data;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    public class MockStrategy_Simple : StrategyAbstract
    {
        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            List<ForwardOnbar_Info> bars = currentData.StrategyOnBarInfos;
            for (int i = 0; i < bars.Count; i++)
            {
                Console.WriteLine(bars[i].KLinePeriod + ":" + bars[i].KLineBar);
            }
        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {
            Console.WriteLine("tick:" + currentData.GetTickData());
        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            Console.WriteLine("Strategy End");
        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            Console.WriteLine("Strategy Start");
        }

    }
}