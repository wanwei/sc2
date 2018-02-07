using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestCsvUtils_TradingTime
    {
        [TestMethod]
        public void TestTradingTimeSaveLoad()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "CsvUtils_TradingTime");
            List<ITradingTime> TradingTime = CsvUtils_TradingTime.Load(path);

            string outputPath = TestCaseManager.GetTestCasePath(GetType(), "TradingTime_Output.csv");
            CsvUtils_TradingTime.Save(outputPath, TradingTime);

            List<ITradingTime> newTradingTime = CsvUtils_TradingTime.Load(outputPath);
            AssertUtils.AssertEqual_List_ToString(TradingTime, newTradingTime);
        }
    }
}
