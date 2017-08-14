using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;
using com.wer.sc.data.reader;

namespace com.wer.sc.plugin.mock.strategy
{
    [Strategy("MOCK.STRATEGY.MA", "MA指标", "MA指标")]
    public class MockStrategy_Ma : StrategyAbstract
    {
        private StrategyReferedPeriods strategyPeriods;

        public MockStrategy_Ma()
        {
            strategyPeriods = new StrategyReferedPeriods();
            strategyPeriods.UseTickData = true;
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return strategyPeriods;
        }

        public override void StrategyEnd()
        {
            throw new NotImplementedException();
        }

        public override void StrategyStart()
        {
            throw new NotImplementedException();
        }

        public override void OnBar(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
