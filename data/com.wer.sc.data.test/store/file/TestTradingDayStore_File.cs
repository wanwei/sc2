using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    [TestClass]
    public class TestTradingDayStore_File
    {
        [TestMethod]
        public void TestTradingDaySaveLoad()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "output_TradingDay");
            List<int> tradingDays = MockDataLoader.GetAllTradingDays();

            TradingDayStore_File store = new TradingDayStore_File(path);
            store.Save(tradingDays);

            TradingDayStore_File newstore = new TradingDayStore_File(path);
            List<int> tradingDays2 = newstore.Load();
            AssertUtils.AssertEqual_List(tradingDays, tradingDays2);
            File.Delete(path);
        }
    }
}