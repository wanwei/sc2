﻿using com.wer.sc.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.wer.sc.mockdata
{
    [TestClass]
    public class TestMockDataLoader
    {
        [TestMethod]
        public void TestGetInstruments()
        {
            List<CodeInfo> instruments = MockDataLoader.GetAllInstruments();
            Assert.IsTrue(instruments.Count > 200);
            //AssertUtils.PrintLineList(instruments);
            //AssertUtils.AssertEqual_List("Instruments", GetType(), instruments);
        }

        [TestMethod]
        public void TestGetAllTradingDays()
        {
            List<int> tradingDays = MockDataLoader.GetAllTradingDays();
            Assert.IsTrue(tradingDays.Count > 2500);
            //AssertUtils.AssertEqual_List("TradingDays", GetType(), tradingDays);
        }

        //[TestMethod]
        //public void TestGetTradingSessions()
        //{
        //    IList<TradingTime> tradingSessions = MockDataLoader.GetTradingTimeList("m05");
        //    AssertUtils.AssertEqual_List("TradingSession", GetType(), tradingSessions);

        //    List<CodeInfo> instruments = MockDataLoader.GetAllInstruments();
        //    for (int i = 0; i < instruments.Count; i++)
        //    {
        //        tradingSessions = MockDataLoader.GetTradingTimeList(instruments[i].Code);
        //        AssertUtils.AssertEqual_List("TradingSession", GetType(), tradingSessions);
        //    }
        //}

        [TestMethod]
        public void TestGetTradingTime()
        {
            List<double[]> tradingTime = MockDataLoader.GetTradingTime("m05", 20100105);
            AssertUtils.AssertEqual_List("TradingTime_Normal", GetType(), tradingTime);
        }

        [TestMethod]
        public void TestGetTickData()
        {
            ITickData tickData = MockDataLoader.GetTickData("m1401", 20131231);
            AssertUtils.AssertEqual_TickData("TickData_M01_20131231", GetType(), tickData);

            tickData = MockDataLoader.GetTickData("m1501", 20141223);
            AssertUtils.AssertEqual_TickData("TickData_M01_20141223", GetType(), tickData);

            tickData = MockDataLoader.GetTickData("m1505", 20150121);
            AssertUtils.AssertEqual_TickData("TickData_M05_20150121", GetType(), tickData);

            tickData = MockDataLoader.GetTickData("m1509", 20141223);
            AssertUtils.AssertEqual_TickData("TickData_M09_20141223", GetType(), tickData);
        }

        [TestMethod]
        public void TestGetKLineData()
        {
            //IKLineData klineData = MockDataLoader.GetKLineData("m05", 20130101, 20151231, KLinePeriod.KLinePeriod_1Minute);
            //AssertUtils.AssertEqual_KLineData("KLineData_M05_20130101_20151231_1Minute", GetType(), klineData);

            IKLineData klineData = MockDataLoader.GetKLineData("m1505", 20141215, 20150116, KLinePeriod.KLinePeriod_1Minute);
            AssertUtils.AssertEqual_KLineData("KLineData_M05_20141215_20150116_1Minute", GetType(), klineData);

            klineData = MockDataLoader.GetKLineData("m1505", 20141215, 20150116, KLinePeriod.KLinePeriod_15Minute);
            AssertUtils.AssertEqual_KLineData("KLineData_M05_20141215_20150116_15Minute", GetType(), klineData);

            klineData = MockDataLoader.GetKLineData("m1505", 20141215, 20150116, KLinePeriod.KLinePeriod_1Day);
            AssertUtils.AssertEqual_KLineData("KLineData_M05_20141215_20150116_Day", GetType(), klineData);
        }
    }
}
