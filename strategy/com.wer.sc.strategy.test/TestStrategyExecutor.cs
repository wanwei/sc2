using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.strategy.mock;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyExecutor
    {
        [TestMethod]
        public void TestRunStrategy_Minute()
        {
            //data.reader.IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;
            IDataPackage_Code dataPackage = CommonData.GetDataPackage(code, start, endDate);

            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Simple));
            runner.SetStrategy(strategy);
            runner.Run();
        }

        [TestMethod]
        public void TestRunStrategy_Tick()
        {
            //data.reader.IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataPackage_Code dataPackage = CommonData.GetDataPackage(code, start, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            DateTime prevtime = DateTime.Now;

            runner.SetStrategy(new MockStrategy(referedPeriods));
            runner.Run();

            DateTime time = DateTime.Now;
            TimeSpan span = time.Subtract(prevtime);
            Console.WriteLine(span.Minutes * 60 * 1000 + span.Seconds * 1000 + span.Milliseconds);
        }

        [TestMethod]
        public void TestExecuteStrategy()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;
            IDataPackage_Code dataPackage = CommonData.GetDataPackage(code, start, endDate);

            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            DateTime prevtime = DateTime.Now;
            runner.SetStrategy(new MockStrategy(null));
            runner.ExecuteFinished += Runner_ExecuteFinished;
            runner.Execute();
            while (!isFinished)
            {

            }
            DateTime time = DateTime.Now;
            TimeSpan span = time.Subtract(prevtime);
            Console.WriteLine(span.Minutes * 60 + span.Seconds);
        }

        private bool isFinished = false;

        private void Runner_ExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments arg)
        {
            Console.WriteLine("ExecuteFinished:");
            isFinished = true;
        }
    }

    class MockStrategy : StrategyAbstract
    {
        private StrategyReferedPeriods referedPeriods;

        public MockStrategy(StrategyReferedPeriods referedPeriods)
        {
            this.referedPeriods = referedPeriods;
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return referedPeriods;
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            Console.WriteLine("bar:" + currentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {
            Console.WriteLine("tick:" + currentData.GetTickData());
        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            Console.WriteLine("Strategy End");
        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            Console.WriteLine("Strategy Start");
        }
    }
}
