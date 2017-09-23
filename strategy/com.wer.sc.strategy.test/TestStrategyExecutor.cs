using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;

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
            IDataPackage dataPackage = CommonData.GetDataPackage(code, start, endDate);

            //StrategyRunnerArguments args = new StrategyRunnerArguments();
            //args.Code = code;
            //args.StartDate = start;
            //args.EndDate = endDate;
            //args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;
            //StrategyExecutor_History runner = new StrategyExecutor_History(dataReader, args);
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            runner.SetStrategy(new MockStrategy(null));
            runner.Run();
        }

        [TestMethod]
        public void TestRunStrategy_Tick()
        {
            //data.reader.IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataPackage dataPackage = CommonData.GetDataPackage(code, start, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            runner.SetStrategy(new MockStrategy(referedPeriods));
            runner.Run();
        }

        [TestMethod]
        public void TestExecuteStrategy()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;
            IDataPackage dataPackage = CommonData.GetDataPackage(code, start, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);

            runner.SetStrategy(new MockStrategy(null));
            runner.ExecuteFinished += Runner_ExecuteFinished;
            runner.Execute();
            while (!isFinished)
            {

            }
        }

        private bool isFinished = false;

        private void Runner_ExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments arg)
        {
            Console.WriteLine("ExecuteFinished:");
            isFinished = true;
        }
    }

    class MockStrategy : StrategyAbstract, IStrategy
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

        public override void OnBar(IRealTimeDataReader currentData)
        {
            Console.WriteLine("bar:" + currentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        }

        public override void OnTick(IRealTimeDataReader currentData)
        {
            Console.WriteLine("tick:" + currentData.GetTickData());
        }

        public override void StrategyEnd()
        {
            Console.WriteLine("Strategy End");
        }

        public override void StrategyStart()
        {
            Console.WriteLine("Strategy Start");
        }
    }
}
