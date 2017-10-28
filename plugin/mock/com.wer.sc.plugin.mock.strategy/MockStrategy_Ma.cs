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
    [Strategy("MOCK.STRATEGY.MA", "MA指标", "MA指标，测试专用")]
    public class MockStrategy_Ma : StrategyAbstract
    {
        private StrategyReferedPeriods strategyPeriods;

        public MockStrategy_Ma()
        {
            strategyPeriods = new StrategyReferedPeriods();
            strategyPeriods.UseTickData = false;
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return strategyPeriods;
        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            throw new NotImplementedException();
        }

        public override void OnBar(Object sender, StrategyOnBarArgument dataReader)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(Object sender, StrategyOnTickArgument dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
