using com.wer.sc.plugin.cnfutures.historydata.updater;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    [TestClass]
    public class TestStep_TickData
    {
        [TestMethod]
        public void TestProceed()
        {
            DataLoader dataLoader = new DataLoader(MockDataLoader.originalDataPath, @"D:\sctest\datatest\", "");
            List<int> openDates = dataLoader.DataLoader_OpenDate.GetTradingDayReader().GetAllOpenDates().GetRange(0, 20);
            Step_TickData step_tick = new Step_TickData("m05", openDates, dataLoader);

            Step_KLineData step_klineData = new Step_KLineData("m05", openDates, dataLoader);
            //step_tick.Proceed();
            step_klineData.Proceed();
        }
    }
}
