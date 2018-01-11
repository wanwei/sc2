using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.mockdata;
using com.wer.sc.data;
using System.Collections.Generic;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDataTransfer_Tick2KLine
    {
        //private static KLineTimeListGetter klineTimeListGetter = GetTimeListGetter();

        //private static TradingTimeUtils t

        //private static KLineTimeListGetter GetTimeListGetter()
        //{
        //    List<int> openDates = MockDataLoader.GetAllTradingDays();
        //    ITradingDayReader opendatecache = new CacheUtils_TradingDay(openDates);
        //    ITradingTimeReader opentimereader = new MockTradingTimeReader();
        //    KLineTimeListGetter getter = new KLineTimeListGetter(opendatecache, opentimereader);
        //    return getter;
        //}

        [TestMethod]
        public void TestTransfer_M01_20131202()
        {
            string code = "m01";
            int date = 20131202;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            string time = "20131202,20131202.09-20131202.1015,20131202.103-20131202.113,20131202.133-20131202.15";
            List<double[]> tradingtime = ParseTradingTime(time);
            DataTransfer_Tick2KLine.Transfer(tickData, tradingtime, KLinePeriod.KLinePeriod_1Minute, 0, 0);
        }

        private List<double[]> ParseTradingTime(string str)
        {
            TradingTime time = new TradingTime();
            time.LoadFromString(str);
            return time.TradingPeriods;
        }

        [TestMethod]
        public void TestTransfer_M05_20040129()
        {
            string code = "m0405";
            int date = 20040129;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            string time = "20040129,20040129.09-20040129.1015,20040129.103-20040129.113,20040129.133-20040129.15";
            List<double[]> tradingtime = ParseTradingTime(time);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, tradingtime, KLinePeriod.KLinePeriod_1Minute, 0, 0);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20040129", GetType(), data);
        }

        [TestMethod]
        public void TestTransfer_M05_20040630()
        {
            string code = "m0505";
            int date = 20040630;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            string time = "20040630,20040630.09-20040630.1015,20040630.103-20040630.113,20040630.133-20040630.15";
            List<double[]> tradingtime = ParseTradingTime(time);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, tradingtime, KLinePeriod.KLinePeriod_1Minute, 2626, -1);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20040630", GetType(), data);
        }

        [TestMethod]
        public void TestTransfer_M05_20150504()
        {
            string code = "m1505";
            int date = 20150504;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            string time = "20150504,20150504.09-20150504.1015,20150504.103-20150504.113,20150504.133-20150504.15";
            List<double[]> tradingtime = ParseTradingTime(time);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(tickData, tradingtime, KLinePeriod.KLinePeriod_1Minute, 2626, -1);
            //AssertUtils.PrintKLineData(data);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20150504", GetType(), data);
        }

        [TestMethod]
        public void TestTransferNight()
        {
            string code = "m1505";
            int date = 20150107;
            ITickData tickData = MockDataLoader.GetTickData(code, date);
            string time = "20150107,20150106.21-20150107.023,20150107.09-20150107.1015,20150107.103-20150107.113,20150107.133-20150107.15";
            List<double[]> tradingtime = ParseTradingTime(time);
            IKLineData klineData = DataTransfer_Tick2KLine.Transfer(tickData, tradingtime,KLinePeriod.KLinePeriod_1Minute, -1, -1);
            AssertUtils.AssertEqual_KLineData("Tick2KLine_M05_20150107", GetType(), klineData);
        }

        //private void AssertTransferTickArray(string code, int startDate, int endDate, KLinePeriod klinePeriod, string result)
        //{
        //    CacheUtils_TradingDay tradingDayCache = new CacheUtils_TradingDay(MockDataLoader.GetAllTradingDays());
        //    IList<int> openDates = tradingDayCache.GetTradingDays(startDate, endDate);
        //    ITickData[] tickDataArray = new ITickData[openDates.Count];
        //    IList<double>[] klineTimeListArray = new IList<double>[openDates.Count];
        //    for (int i = 0; i < openDates.Count; i++)
        //    {
        //        int date = openDates[i];
        //        ITickData tickData = MockDataLoader.GetTickData(code, date);
        //        tickDataArray[i] = tickData;
        //        klineTimeListArray[i] = klineTimeListGetter.GetKLineTimeList(code, date, klinePeriod);
        //    }
        //    IKLineData data = DataTransfer_Tick2KLine.Transfer(tickDataArray, klineTimeListArray, -1, -1);
        //    AssertUtils.AssertEqual_KLineData(result, data);
        //}

        //[TestMethod]
        //public void TestTransfer_M01_20131202_20131213()
        //{
        //    AssertTransferTickArray("m01", 20131202, 20131213, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20131202_20131213"));
        //}

        //[TestMethod]
        //public void TestTransfer_M05_20131202_20131231()
        //{
        //    AssertTransferTickArray("m05", 20131202, 20131231, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M05_20131202_20131231"));
        //}

        //[TestMethod]
        //public void TestTransfer_M01_20131202_20131213_15Second()
        //{
        //    AssertTransferTickArray("m01", 20131202, 20131213, new KLinePeriod(KLineTimeType.SECOND, 15), TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20131202_20131213_15Second"));
        //}

        //[TestMethod]
        //public void TestTransfer_M01_20040102_20040301()
        //{
        //    AssertTransferTickArray("m01", 20040102, 20040301, KLinePeriod.KLinePeriod_1Minute, TestCaseManager.LoadTestCaseFile(GetType(), "Tick2Kline_M01_20040102_20040301"));
        //}
    }

    class MockTradingTimeReader : ITradingTimeReader_Code
    {
        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time)
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time, bool forward)
        {
            throw new NotImplementedException();
        }

        public double GetRecentTradingTime(double time, bool findForward)
        {
            throw new NotImplementedException();
        }

        public int GetTradingDay(double time)
        {
            throw new NotImplementedException();
        }

        public ITradingDayReader GetTradingDayReader()
        {
            throw new NotImplementedException();
        }

        public ITradingTime GetTradingTime(int date)
        {
            throw new NotImplementedException();
        }

        public IList<ITradingTime> GetTradingTime(int start, int end)
        {
            throw new NotImplementedException();
        }

        public List<double[]> GetTradingTime(string code, int date)
        {
            return MockDataLoader.GetTradingTime(code, date);
        }

        public bool IsStartTime(double time)
        {
            throw new NotImplementedException();
        }
    }
}
