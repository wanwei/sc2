using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy.cnfutures
{
    [TestClass]
    public class Strategy_Demo_TickImport :StrategyAbstract, IStrategy
    {
        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {

        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {

        }
    }
}
