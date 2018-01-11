using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 测试数据导航
    /// 1.
    /// 2.
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class TestDataNavigate_Code
    {
        [TestMethod]
        public void TestNavigate_Code()
        {
            string code = "rb1705";
            double time = 20170405.093001;

            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time);
            //DataNavigateFactory.CreateDataNavigate(@"e:\scdata\cnfutures\", code, time);
            ITickData tickData = nav.GetTickData();
            Console.WriteLine(tickData);
            Console.WriteLine(nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine(nav.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            //DataReaderFactory fac = new DataReaderFactory(@"d:\scdata\cnfutures\");
            //IDataNavigate2 navigate2 = fac.DataNavigateMgr.CreateNavigate("m05", 20141229.093100, 20150101, 20160101);

            //KLinePeriod period_day = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            //KLinePeriod period_15minute = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15);
            //KLinePeriod period_minute = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1);

            //IKLineData klineData = navigate2.GetKLineData(period_day);
            //Console.WriteLine(klineData);
            //IKLineData klineData_15minute = navigate2.GetKLineData(period_15minute);
            //Console.WriteLine(klineData_15minute);
            //IKLineData klineData_minute = navigate2.GetKLineData(period_minute);
            //Console.WriteLine(klineData_minute);
            //ITimeLineData realData = navigate2.GetRealData();
            //Console.WriteLine(realData);
            //ITickData tickData = navigate2.GetTickData();
            //Console.WriteLine(tickData);

            //navigate2.ChangeTime(20141229.213508);
            //klineData = navigate2.GetKLineData(period_day);
            //Console.WriteLine(klineData);
            //klineData_15minute = navigate2.GetKLineData(period_15minute);
            //Console.WriteLine(klineData_15minute);
            //klineData_minute = navigate2.GetKLineData(period_minute);
            //Console.WriteLine(klineData_minute);
            //realData = navigate2.GetRealData();
            //Console.WriteLine(realData);
            //tickData = navigate2.GetTickData();
            //Console.WriteLine(tickData);
        }

        [TestMethod]
        public void TestNavigate_Code2()
        {
            string code = "rb1801";
            double time = 20170104.082800;
            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time);
            Assert.IsNull(nav);
            //double time = 20170929.150000;
            //double time = 20170929.102800;
            //double time = 20170930.102800;

            time = 20170929.082800;
            nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time);
            //DataNavigateFactory.CreateDataNavigate(@"e:\scdata\cnfutures\", code, time);
            IKLineData klineData = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            AssertUtils.PrintKLineData(klineData);
        }

        [TestMethod]
        public void TestNavigate_Code_Forward()
        {
            string code = "rb1801";
            double time = 20170929.145900;
            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time, 100, 0);
            bool canForward = nav.Forward(KLinePeriod.KLinePeriod_1Minute);
            Assert.IsTrue(canForward);

            canForward = nav.Forward(KLinePeriod.KLinePeriod_1Minute);
            Assert.IsFalse(canForward);
        }

        [TestMethod]
        public void TestNavigate_Code_RB1805()
        {
            string code = "rb1805";
            double time = 20171226.2100;
            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time, 100, 0);

            time = 20171221.2100;
            nav.NavigateTo(time);

            IKLineData klineData_1 = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            IKLineData klineData_5 = nav.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            IKLineData klineData_15 = nav.GetKLineData(KLinePeriod.KLinePeriod_15Minute);

            Assert.AreEqual(klineData_1.End, klineData_15.End);

            nav.Forward(KLinePeriod.KLinePeriod_1Minute);
            klineData_1 = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            klineData_5 = nav.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            klineData_15 = nav.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Assert.AreEqual(klineData_1.End, klineData_5.End);
            Assert.AreEqual(klineData_1.End, klineData_15.End);

            nav.NavigateTo(20171001);
            klineData_1 = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            klineData_5 = nav.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            klineData_15 = nav.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Assert.AreEqual(klineData_1.End, klineData_5.End);
            Assert.AreEqual(klineData_1.End, klineData_15.End);
            Console.WriteLine(klineData_15);
        }

        [TestMethod]
        public void TestNavigate_Code_Day()
        {
            string code = "rb1805";
            double time = 20171226.2100;
            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate_Code(code, time, 100, 0);
            IKLineData klineData = nav.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            //for (int i = 0; i < klineData.Length; i++)
            //{
            //    Console.WriteLine(klineData.GetBar(i));
            //}

            time = 20171002.09;
            nav.NavigateTo(time);
            klineData = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData);
            Assert.AreEqual("20170929.1459,3524,3540,3522,3538,7648,0,487716", klineData.ToString());


            //for (int i = 0; i < klineData.Length; i++)
            //{
            //    Console.WriteLine(klineData.GetBar(i));
            //}
        }
    }
}