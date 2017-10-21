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
    [Strategy("MOCK.STRATEGY.VOLUME","量能过滤", "量能过滤")]
    public class MockStrategy_Volume : StrategyAbstract
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
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
