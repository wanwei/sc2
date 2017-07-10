using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyRunner
    {
        [TestMethod]
        public void TestRunStrategy_Minute()
        {
            data.reader.IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            StrategyRunnerArguments args = new StrategyRunnerArguments();
            args.Code = code;
            args.StartDate = start;
            args.EndDate = endDate;
            args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;
            StrategyRunner_History runner = new StrategyRunner_History(dataReader, args);

            runner.SetStrategy(new MockStrategy(null));
            runner.Run();
        }

        [TestMethod]
        public void TestRunStrategy_Tick()
        {
            data.reader.IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            StrategyRunnerArguments args = new StrategyRunnerArguments();
            args.Code = code;
            args.StartDate = start;
            args.EndDate = endDate;
            args.IsTickForward = true;
            args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;
            StrategyRunner_History runner = new StrategyRunner_History(dataReader, args);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            runner.SetStrategy(new MockStrategy(referedPeriods));
            runner.Run();
        }
    }

    class MockStrategy : IStrategy
    {
        private StrategyReferedPeriods referedPeriods;

        public MockStrategy(StrategyReferedPeriods referedPeriods)
        {
            this.referedPeriods = referedPeriods;
        }

        public StrategyReferedPeriods GetStrategyPeriods()
        {
            return referedPeriods;
        }

        public void OnBar(IRealTimeDataReader currentData)
        {
            Console.WriteLine("bar:" + currentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        }

        public void OnTick(IRealTimeDataReader currentData)
        {
            Console.WriteLine("tick:" + currentData.GetTickData());
        }

        public void StrategyEnd()
        {
            Console.WriteLine("Strategy End");
        }

        public void StrategyStart()
        {
            Console.WriteLine("Strategy Start");
        }
    }
}
