using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.mockdata;
using com.wer.sc.data;
using System.Collections.Generic;
using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using com.wer.sc.data.utils;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDataTransfer_Tick2KLine
    {
        private static KLineTimeListGetter klineTimeListGetter = GetTimeListGetter();

        private static KLineTimeListGetter GetTimeListGetter()
        {
            List<int> openDates = MockDataLoader.GetAllTradingDays();
            ITradingDayReader opendatecache = new TradingDayCache(openDates);
            ITradingTimeReader opentimereader = new MockTradingTimeReader();
            KLineTimeListGetter getter = new KLineTimeListGetter(opendatecache, opentimereader);
            return getter;
        }

        [TestMethod]
        public void TestTransfer_M01_20131202()
        {
            string code = "m01";
            int date = 20131202;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            List<double> klineTimeList = klineTimeListGetter.GetKLineTimeList(code, date, KLinePeriod.KLinePeriod_1Minute);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, klineTimeList, -1, -1);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M01_20131202", GetType(), data);
        }

        [TestMethod]
        public void TestTransfer_M05_20040129()
        {
            string code = "m05";
            int date = 20040129;
            ITickData tickData = MockDataLoader.GetTickData("m05", date);
            List<double> klineTimeList = klineTimeListGetter.GetKLineTimeList(code, date, KLinePeriod.KLinePeriod_1Minute);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, klineTimeList, -1, -1);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20040129", GetType(), data);
        }

        [TestMethod]
        public void TestTransfer_M05_20040630()
        {
            string code = "m05";
            int date = 20040630;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            List<double> klineTimeList = klineTimeListGetter.GetKLineTimeList(code, date, KLinePeriod.KLinePeriod_1Minute);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, klineTimeList, 2626, -1);
            //AssertUtils.PrintKLineData(data);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20040630", GetType(), data);
        }

        [TestMethod]
        public void TestTransfer_M05_20150504()
        {
            string code = "m05";
            int date = 20150504;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            List<double> klineTimeList = klineTimeListGetter.GetKLineTimeList(code, date, KLinePeriod.KLinePeriod_1Minute);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, klineTimeList, 2626, -1);
            //AssertUtils.PrintKLineData(data);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20150504", GetType(), data);
        }

        [TestMethod]
        public void TestTransferNight()
        {
            string code = "m05";
            int date = 20150107;
            ITickData tickData = MockDataLoader.GetTickData(code, date);

            List<double[]> tradingTime = MockDataLoader.GetTradingTime(code, date);
            List<double> klineTimeList = KLineTimeListUtils.GetKLineTimeList(20150107, 20150106, tradingTime, KLinePeriod.KLinePeriod_1Minute);
            AssertUtils.PrintLineList(klineTimeList);
            IKLineData klineData = DataTransfer_Tick2KLine.Transfer(tickData, klineTimeList, -1, -1);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20150107", GetType(), klineData);
        }        

        private void AssertTransferTickArray(string code, int startDate, int endDate, KLinePeriod klinePeriod, string result)
        {
            TradingDayCache tradingDayCache = new TradingDayCache(MockDataLoader.GetAllTradingDays());
            IList<int> openDates = tradingDayCache.GetTradingDays(startDate, endDate);
            ITickData[] tickDataArray = new ITickData[openDates.Count];
            IList<double>[] klineTimeListArray = new IList<double>[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                ITickData tickData = MockDataLoader.GetTickData(code, date);
                tickDataArray[i] = tickData;
                klineTimeListArray[i] = klineTimeListGetter.GetKLineTimeList(code, date, klinePeriod);
            }
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickDataArray, klineTimeListArray, -1, -1);
            AssertUtils.AssertEqual_KLineData(result, data);
        }

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213()
        {
            AssertTransferTickArray("m01", 20131202, 20131213, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20131202_20131213"));
        }

        [TestMethod]
        public void TestTransfer_M05_20131202_20131231()
        {
            AssertTransferTickArray("m05", 20131202, 20131231, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M05_20131202_20131231"));
        }

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213_15Second()
        {
            AssertTransferTickArray("m01", 20131202, 20131213, new KLinePeriod(KLineTimeType.SECOND, 15), TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20131202_20131213_15Second"));
        }

        //[TestMethod]
        //public void TestTransfer_M01_20040102_20040301()
        //{
        //    AssertTransferTickArray("m01", 20040102, 20040301, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20040102_20040301"));
        //}
    }

    class MockTradingTimeReader : ITradingTimeReader
    {
        public List<double[]> GetTradingTime(string code, int date)
        {
            return MockDataLoader.GetTradingTime(code, date);
        }
    }
}
