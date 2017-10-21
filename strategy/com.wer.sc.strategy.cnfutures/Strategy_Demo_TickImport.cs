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
        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {

        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {

        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {

        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {

        }
    }
}
