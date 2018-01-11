using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.navigate;
using com.wer.sc.data;
using com.wer.sc.data.reader;

namespace com.wer.sc.ui.comp
{
    [TestClass]
    public class TestCompChartController
    {
        [TestMethod]
        public void TestController()
        {
            string code = "rb1710";
            double time = 20170601.093055;
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            IDataNavigate dataNavigater = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            int showKLineIndex = klineData.BarPos;
            ChartComponentData compData = new ChartComponentData(code, time, klinePeriod, showKLineIndex);
            ChartComponentController controller = new ChartComponentController(dataNavigater, compData);

            IRealTimeData_Code reader = controller.CurrentRealTimeDataReader;
            IKLineData currentKLine = reader.GetKLineData(klinePeriod);
            Assert.AreEqual(compData.Code, currentKLine.Code);
            Assert.AreEqual(compData.Time, currentKLine.Time);
            Assert.AreEqual(compData.ShowKLineIndex, currentKLine.BarPos);
            Console.WriteLine(currentKLine);

            controller.Change("rb1801");
            currentKLine = reader.GetKLineData(klinePeriod);
            Assert.AreEqual(compData.Code, currentKLine.Code);
            Assert.AreEqual(compData.Time, currentKLine.Time);
            Assert.AreEqual(compData.ShowKLineIndex, currentKLine.BarPos);
            Console.WriteLine(currentKLine);

            controller.Change(code);
            currentKLine = reader.GetKLineData(klinePeriod);
            Assert.AreEqual(compData.Code, currentKLine.Code);
            Assert.AreEqual(compData.Time, currentKLine.Time);
            Assert.AreEqual(compData.ShowKLineIndex, currentKLine.BarPos);
            Console.WriteLine(currentKLine);
            controller.ChangeChartType(ChartType.TimeLine);
            Console.WriteLine(compData);

            controller.Change(20170601.100531);
            controller.ChangeChartType(ChartType.KLine);
            Console.WriteLine(reader.GetKLineData(compData.KlinePeriod));
            Console.WriteLine(compData);

            controller.Change("rb1801", 20171014.093000, KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(compData);
        }

        [TestMethod]
        public void TestDataChanged()
        {
            //string code = "rb1710";
            //double time = 20170601.093055;
            //KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            //IDataNavigate dataNavigater = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            //IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            //int showKLineIndex = klineData.BarPos;
            //CompData compData = new CompData(code, time, klinePeriod, showKLineIndex);
            //CompDataController controller = new CompDataController(dataNavigater, compData);

        }

        [TestMethod]
        public void TestPlay()
        {
            string code = "rb1710";
            double time = 20170601.093055;
            IDataNavigate dataNavigater = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            int showKLineIndex = klineData.BarPos;
            ChartComponentData compData = new ChartComponentData(code, time, klinePeriod, showKLineIndex);
            ChartComponentController controller = new ChartComponentController(dataNavigater, compData);
            controller.OnDataChanged += Controller_OnDataChanged;
            //controller.Play();
            //while (controller.IsPlayIng)
            //{
                
            //}

            //while(time<2017)
        }

        private void Controller_OnDataChanged(object sender, ChartComponentDataChangeArgument arg)
        {
            Console.WriteLine(arg.CurrentChartComponentData);
            if (arg.CurrentChartComponentData.Time > 20170601.093059)
                ((ChartComponentController)sender).Pause();
        }

        //[TestMethod]
        //public void TestController2()
        //{
        //    string code = "rb1710";
        //    double time = 20170601.093055;
        //    KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
        //    IDataNavigate dataNavigater = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
        //    IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
        //    int showKLineIndex = klineData.BarPos;
        //    CompData compData = new CompData(code, time, klinePeriod, showKLineIndex);
        //    CompDataController controller = new CompDataController(dataNavigater, compData);

        //    controller.Change("rb1801", 20171014.093000, KLinePeriod.KLinePeriod_1Minute);
        //}
    }
}
