using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestKLineData
    {
        [TestMethod]
        public void TestToString()
        {
            string res = TestCaseManager.LoadTestCaseFile(GetType(), "KLineData_M01_1Minute");
            string[] lines = res.Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);
            for (int i = 0; i < lines.Length; i++)
            {
                klineData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), klineData.ToString());
            }
        }

        [TestMethod]
        public void TestKLineData_Sub()
        {
            KLineData data = GetKLineData_1Min();
            IKLineData data_sub = data.Sub(100, 200);
            for (int i = 100; i <= 200; i++)
            {
                data.BarPos = i;
                data_sub.BarPos = i - 100;
                Assert.AreEqual(data.ToString(), data_sub.ToString());
            }
            Assert.AreEqual(101, data_sub.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestKLineData_Sub_Exception()
        {
            KLineData data = GetKLineData_1Min();
            IKLineData data_sub = data.Sub(100, 200);
            float start = data_sub.Arr_Start[102];
        }

        [TestMethod]
        public void TestKLineData_GetRange()
        {
            KLineData data = GetKLineData_1Min();
            IKLineData data_sub = data.GetRange(100, 200);
            for (int i = 100; i <= 200; i++)
            {
                data.BarPos = i;
                data_sub.BarPos = i - 100;
                Assert.AreEqual(data.ToString(), data_sub.ToString());
            }
        }


        [TestMethod]
        public void TestKLineMerge()
        {
            KLineData data = GetKLineData_1Min();
            IKLineData d1 = data.GetRange(0, 99);
            IKLineData d2 = data.GetRange(100, 199);
            IKLineData d3 = data.GetRange(200, 299);
            IKLineData d4 = data.GetRange(300, data.Length - 1);

            List<IKLineData> dataList = new List<IKLineData>();
            dataList.Add(d1);
            dataList.Add(d2);
            dataList.Add(d3);
            dataList.Add(d4);
            IKLineData dataResult = KLineData.Merge(dataList);
            Assert.AreEqual(dataResult.Length, data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                dataResult.BarPos = i;
                Assert.AreEqual(dataResult.ToString(), data.ToString());
            }
        }

        [TestMethod]
        public void TestGetPeriod()
        {
            KLineData data = GetKLineData_1Min();
            KLinePeriod period = data.Period;
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Minute, period);
        }

        [TestMethod]
        public void TestIndexOfTime()
        {
            KLineData data = GetKLineData_1Min();
            Assert.AreEqual(42, data.IndexOfTime(20131202.094255));
            Assert.AreEqual(43, data.IndexOfTime(20131202.0943));
            Assert.AreEqual(43, data.IndexOfTime(20131202.094305));
        }

        private KLineData GetKLineData_1Min()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "KLineData_M01_1Minute");
            return (KLineData)CsvUtils_KLineData.Load(path);
        }
    }
}
