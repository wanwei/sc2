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
    public class TestCsvUtils_TickData
    {
        [TestMethod]
        public void TestTickDataLoad()
        {
            string[] lines = TestCaseManager.LoadTestCaseFile(GetType(), "CsvUtils_TickData").Split('\r');
            ITickData tickData = CsvUtils_TickData.LoadByLines(lines);
            Assert.AreEqual(lines.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), tickData.ToString());
            }
        }

        [TestMethod]
        public void TestTickDataLoadSave()
        {
            String path = TestCaseManager.GetTestCasePath(GetType(), "TickData_Output.csv");
            string[] lines = TestCaseManager.LoadTestCaseFile(GetType(), "CsvUtils_TickData").Split('\r');
            ITickData tickData = CsvUtils_TickData.LoadByLines(lines);

            CsvUtils_TickData.Save(path, tickData);

            ITickData newtickData = CsvUtils_TickData.Load(path);
            AssertUtils.AssertEqual_TickData(newtickData, tickData);
        }
    }
}