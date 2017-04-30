using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestCsvUtils_TradingSession
    {
        [TestMethod]
        public void TestTradingSessionSaveLoad()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "CsvUtils_TradingSession");
            List<TradingSession> tradingSession = CsvUtils_TradingSession.Load(path);

            string outputPath = TestCaseManager.GetTestCasePath(GetType(), "TradingSession_Output.csv");
            CsvUtils_TradingSession.Save(outputPath, tradingSession);

            List<TradingSession> newTradingSession = CsvUtils_TradingSession.Load(outputPath);
            AssertUtils.AssertEqual_List_ToString(tradingSession, newTradingSession);
        }
    }
}
