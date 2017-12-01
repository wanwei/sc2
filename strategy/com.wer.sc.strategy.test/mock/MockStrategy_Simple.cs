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
        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IList<IStrategyOnBarInfo> bars = currentData.FinishedBars;
            for (int i = 0; i < bars.Count; i++)
            {
                Console.WriteLine(bars[i].KLinePeriod + ":" + bars[i].KLineBar);
            }
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {
            Console.WriteLine("tick:" + currentData.CurrentData.GetTickData());
        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            Console.WriteLine("Strategy End");
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            Console.WriteLine("Strategy Start");
        }

    }
}