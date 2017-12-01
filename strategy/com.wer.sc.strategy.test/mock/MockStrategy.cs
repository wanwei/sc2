using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{

    public class MockStrategy : StrategyAbstract
    {
        private List<string> printData = new List<string>();

        private StrategyReferedPeriods referedPeriods;

        public List<string> PrintData
        {
            get
            {
                return printData;
            }            
        }

        public MockStrategy()
        {

        }

        public MockStrategy(StrategyReferedPeriods referedPeriods)
        {
            this.referedPeriods = referedPeriods;
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return referedPeriods;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IList<IStrategyOnBarInfo> bars = currentData.FinishedBars;
            for (int i = 0; i < bars.Count; i++)
            {
                IStrategyOnBarInfo barInfo = bars[i];
                Console.WriteLine(barInfo.KLinePeriod + ":" + barInfo.KLineBar);
                printData.Add(barInfo.KLinePeriod + ":" + barInfo.KLineBar);
            }
            //Console.WriteLine("bar:" + currentData.CurrentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {
            Console.WriteLine("tick:" + currentData.Tick.TickBar);
            printData.Add("tick:" + currentData.Tick.TickBar);
        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            Console.WriteLine("Strategy End");
            printData.Add("Strategy End");
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            Console.WriteLine("Strategy Start");
            printData.Add("Strategy Start");
        }
    }
}
