using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestCsvUtils_KLineData
    {
        [TestMethod]
        public void TestKLineDataLoad()
        {
            string[] lines = TestCaseManager.LoadTestCaseFile(GetType(), "CsvUtils_KLineData").Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);
            Assert.AreEqual(lines.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), klineData.ToString());
            }
        }

        [TestMethod]
        public void TestKLineDataSaveLoad()
        {
            String filename = "KLineData_Output.csv";
            string[] lines = TestCaseManager.LoadTestCaseFile(GetType(), "CsvUtils_KLineData").Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);

            string testCasePath = TestCaseManager.GetTestCasePath(GetType(), filename);
            CsvUtils_KLineData.Save(testCasePath, klineData);
            IKLineData newklineData = CsvUtils_KLineData.Load(testCasePath);
            File.Delete(testCasePath);

            AssertUtils.AssertEqual_KLineData(klineData, newklineData);
        }
    }
}
