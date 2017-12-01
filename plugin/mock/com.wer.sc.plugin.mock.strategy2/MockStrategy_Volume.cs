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

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            throw new NotImplementedException();
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
