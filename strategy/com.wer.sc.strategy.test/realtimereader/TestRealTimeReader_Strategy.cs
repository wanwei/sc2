﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using com.wer.sc.data;

namespace com.wer.sc.strategy.realtimereader
{
    [TestClass]
    public class TestRealTimeReader_Strategy
    {
        [TestMethod]
        public void TestRealTimeReader()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170610;
            RealTimeReader_Strategy realTimeReader = GetRealTimeReader(code, start, endDate, false);
            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
                IKLineData klineData = realTimeReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
                Console.WriteLine(klineData);
            }
        }

        [TestMethod]
        public void TestRealTimeReader_OnTick()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170610;
            RealTimeReader_Strategy realTimeReader = GetRealTimeReader(code, start, endDate, true);
            realTimeReader.OnTick += RealTimeReader_OnTick;
            realTimeReader.OnBar += RealTimeReader_OnBar;
            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
            }
        }

        private void RealTimeReader_OnBar(object sender, IKLineData klineData, int index)
        {
            if (index == 0)
                return;
            Console.WriteLine("kline:" + klineData.GetBar(index - 1));
        }

        private void RealTimeReader_OnTick(object sender, ITickData tickData, int index)
        {
            Console.WriteLine("tick:" + tickData);
        }

        private static RealTimeReader_Strategy GetRealTimeReader(string code, int start, int endDate, bool useTickData)
        {
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = useTickData;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);

            RealTimeReader_StrategyArguments args = new RealTimeReader_StrategyArguments();
            args.Code = code;
            args.StartDate = start;
            args.EndDate = endDate;
            args.ReferedPeriods = referedPeriods;
            args.IsTickForward = useTickData;
            args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;

            RealTimeReader_Strategy realTimeReader = new RealTimeReader_Strategy(CommonData.GetDataReader(), args);
            return realTimeReader;
        }
    }
}
