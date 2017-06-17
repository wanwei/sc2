using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.ana;
using com.wer.sc.ana.test.model;
using com.wer.sc.data;

namespace com.wer.sc.ana.test
{
    [TestClass]
    public class TestKLineModelRunner
    {
        [TestMethod]
        public void TestModel_Simple()
        {
            KLineModelRunner runner = new KLineModelRunner(@"D:\SCDATA\CNFUTURES");
            runner.Code = "m05";
            runner.StartDate = 20100725;
            runner.EndDate = 20111125;
            runner.Period = new data.KLinePeriod(KLineTimeType.DAY, 1);

            KLineModel_Simple model = new KLineModel_Simple();
            model.HLLen = 5;
            //model.HLLen = 25;
            //model.ZZLen = 6;
            runner.Model = model;
            runner.run();

            int size = model.Arr_RealDD.Count < model.Arr_RealGD.Count ? model.Arr_RealDD.Count
                    : model.Arr_RealGD.Count;
            Assert.AreEqual(34, size);

            Assert.AreEqual(20100802, model.Arr_Time[model.Arr_PosRealGD[0]]);
            Assert.AreEqual(20100806, model.Arr_Time[model.Arr_PosRealDD[0]]);

            Assert.AreEqual(20111104, model.Arr_Time[model.Arr_PosRealGD[33]]);
            Assert.AreEqual(20111110, model.Arr_Time[model.Arr_PosRealDD[33]]);
        }

        [TestMethod]
        public void TestModel_Compound()
        {
            KLineModelRunner runner = new KLineModelRunner(@"D:\SCDATA\CNFUTURES");
            runner.Code = "m05";
            runner.StartDate = 20100725;
            runner.EndDate = 20111125;
            runner.Period = new data.KLinePeriod(KLineTimeType.DAY, 1);
            KLineModel_Compound model = new KLineModel_Compound();
            runner.Model = model;
            runner.run();

            Assert.AreEqual(-493.0f, model.earn);
        }

        [TestMethod]
        public void Test_Import_Period()
        {
            KLineModelRunner runner = new KLineModelRunner(@"D:\SCDATA\CNFUTURES");
            runner.Code = "m05";
            runner.StartDate = 20100725;
            runner.EndDate = 20111125;
            runner.Period = new data.KLinePeriod(KLineTimeType.MINUTE, 1);
            KLineModel_ImportPeriod model = new KLineModel_ImportPeriod();
            runner.Model = model;
            runner.run();

            Assert.AreEqual(419.0f, model.earn);
        }

        public void Test_Import_Contract()
        {
            //TODO 暂不支持
            //		ScRunner runner = new ScRunner();
            //		runner.setCode("m05");
            //		runner.setStart("20100701");
            //		runner.setEnd("20111126");
            //		runner.setTimeType(KLineTimeType.TYPE_MINUTE);
            //		KLineModel_ImportPeriod model = new KLineModel_ImportPeriod();
            //		runner.setModel(model);
            //		runner.run();
        }
    }
}
