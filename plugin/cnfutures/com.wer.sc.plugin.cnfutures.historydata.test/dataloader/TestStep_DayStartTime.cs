using com.wer.sc.mockdata;
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
    public class TestStep_DayStartTime
    {
        [TestMethod]
        public void TestGenerateDayStartTime()
        {
            DataLoader dataLoader = MockDataLoader.DataLoader;
            Step_TradingSession step = new Step_TradingSession("m05", dataLoader);

            List<TradingSession> dayStartTimes = step.GetAllDayStartTimes();
            //for (int i = 0; i < dayStartTimes.Count; i++)
            //    Console.WriteLine(dayStartTimes[i]);
            //AssertUtils.AssertList(dayStartTimes, Resources.DayStartTime_M05);
            Assert.IsNull(dayStartTimes);
        }
    }
}
