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
    public class Strategy_Demo_TickImport : IStrategy
    {
        public StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public void OnBar(IRealTimeDataReader currentData)
        {

        }

        public void OnTick(IRealTimeDataReader currentData)
        {

        }

        public void StrategyEnd()
        {

        }

        public void StrategyStart()
        {

        }
    }
}
