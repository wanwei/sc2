using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestCsvUtils_TradingDay
    {
        [TestMethod]
        public void TestTradingDaySaveLoad()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "CsvUtils_TradingDay");
            List<int> tradingDay = CsvUtils_TradingDay.Load(path);

            string outputPath = TestCaseManager.GetTestCasePath(GetType(), "TradingDay_Output.csv");
            CsvUtils_TradingDay.Save(outputPath, tradingDay);

            List<int> newTradingDay = CsvUtils_TradingDay.Load(outputPath);
            AssertUtils.AssertEqual_List_ToString(tradingDay, newTradingDay);
        }
    }
}
