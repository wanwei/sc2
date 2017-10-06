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
    public class TestDataNavigate
    {
        [TestMethod]
        public void TestNavigate()
        {
            string code = "rb1705";
            double time = 20170405.093001;

            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
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
        public void TestNavigate2()
        {
            string code = "rb1801";
            double time = 20170104.082800;
            IDataNavigate_Code nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            Assert.IsNull(nav);
            //double time = 20170929.150000;
            //double time = 20170929.102800;
            //double time = 20170930.102800;

            time = 20170929.082800;
            nav = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            //DataNavigateFactory.CreateDataNavigate(@"e:\scdata\cnfutures\", code, time);
            IKLineData klineData = nav.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            AssertUtils.PrintKLineData(klineData);
        }
    }
}