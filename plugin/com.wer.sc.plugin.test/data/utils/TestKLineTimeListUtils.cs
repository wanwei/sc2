using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 测试用例：
    /// 1.普通交易日
    /// 2.有夜盘无过夜交易日
    /// 3.有夜盘过夜交易日
    /// 4.有夜盘跨周无过夜交易日
    /// 5.有夜盘扩周有过夜交易日
    /// </summary>
    [TestClass]
    public class TestKLineTimeListUtils
    {
        [TestMethod]
        public void TestGetKLineTimeList_M01_20131202_1Minute()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20131202, OpenDateReader, OpenTime_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M01_20131202"));            
        }

        [TestMethod]
        public void TestGetKLineTimeList_M01_20131202_1Minute_2()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20131202, 20131129, OpenTime_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M01_20131202"));
        }

        [TestMethod]
        public void TestGetKLineTimeList_M01_20141229_5Second()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20141229, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_5Second);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M05_20141229_5Second"));
        }

        [TestMethod]
        public void TestGetKLineTimeList_M05_20141229_1Minute()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20141229, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M05_20141229"));
        }

        [TestMethod]
        public void TestGetKLineTimeList_M05_20141230_1Minute()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20141230, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M05_20141230"));
        }

        [TestMethod]
        public void TestGetKLineTimeList_M05_20150624_1Minute()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150624, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M05_20150624"));
        }

        [TestMethod]
        public void TestGetKLineTimeList_M05_20150629_1Minute()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150629, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_M05_20150629"));
        }

        [TestMethod]
        public void TestGetKLineTimeLists_Normal()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20100105, OpenDateReader, OpenTime_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_Normal"));
        }

        [TestMethod]
        public void TestGetKLineTimeLists_Night_Normal()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150701, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_Night_Normal"));
        }

        [TestMethod]
        public void TestGetKLineTimeLists_Night_OverNight()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150106, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_OverNight"));
        }

        [TestMethod]
        public void TestGetKLineTimeLists_NightNormal_WeekStart()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150727, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_NightNormal_WeekStart"));
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);
        }

        [TestMethod]
        public void TestGetKLineTimeLists_NightOverNight_WeekStart()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150112, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_OverNight_WeekStart"));
        }

        [TestMethod]
        public void TestGetKLineTimeLists_NightOverNight_WeekStart_2()
        {
            List<double> klineTimes = KLineTimeListUtils.GetKLineTimeList(20150112, 20150109, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "KLineTimeList_OverNight_WeekStart"));
        }

        //[TestMethod]
        //public void TestGetKLineTimeLists_Future()
        //{
        //    List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20280112, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
        //    for (int i = 0; i < klineTimes.Count; i++)
        //        Console.WriteLine(klineTimes[i]);
        //    //AssertOpenTime(klineTimes, TestCaseLoader.LoadTestCaseFile(GetType(), "KLineTimeList_OverNight_WeekStart);
        //}

        private void AssertOpenTime(List<double> klineTimes, String resource)
        {
            string[] lines = resource.Split('\r');
            Assert.AreEqual(lines.Length, klineTimes.Count);
            for (int i = 0; i < klineTimes.Count; i++)
            {
                Assert.AreEqual(double.Parse(lines[i]), klineTimes[i]);
            }
        }

        private List<double[]> OpenTime_Normal
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private List<double[]> OpenTime_Night_Normal
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .210000, .233000 });
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private List<double[]> OpenTime_Night_OverNight
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .210000, .023000 });
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private TradingDayCache opendateCache = new TradingDayCache(MockDataLoader.GetAllTradingDays());

        private ITradingDayReader OpenDateReader
        {
            get
            {
                return opendateCache;
            }
        }
    }
}
