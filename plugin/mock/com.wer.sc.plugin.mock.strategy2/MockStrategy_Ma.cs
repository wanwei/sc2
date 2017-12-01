using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;

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

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return strategyPeriods;
        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument dataReader)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
