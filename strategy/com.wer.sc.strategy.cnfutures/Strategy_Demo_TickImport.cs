using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy.cnfutures
{
    [TestClass]
    public class Strategy_Demo_TickImport :StrategyAbstract, IStrategy
    {
        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public override void OnBar(IRealTimeDataReader_Code currentData)
        {

        }

        public override void OnTick(IRealTimeDataReader_Code currentData)
        {

        }

        public override void StrategyEnd()
        {

        }

        public override void StrategyStart()
        {

        }
    }
}
