using com.wer.sc.data;
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
    [TestClass]
    public class TestTradingTimeUtils
    {
        [TestMethod]
        public void TestGetKLineTimeList_Full()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { 20150107.210000, 20150108.023000 });
            openTime.Add(new double[] { 20150110.090000, 20150110.101500 });
            openTime.Add(new double[] { 20150110.103000, 20150110.113000 });
            openTime.Add(new double[] { 20150110.133000, 20150110.150000 });

            List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList(openTime, new KLinePeriod(KLineTimeType.SECOND, 1));
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_15Minute);
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_1Hour);
            for (int i = 0; i < klineTimeList.Count; i++)
            {
                double[] time = klineTimeList[i];
                Console.WriteLine(time[0] + "-" + time[1]);
            }
        }

        [TestMethod]
        public void TestGetKLineDataTimeInfo()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { 20150107.210000, 20150107.230000 });
            openTime.Add(new double[] { 20150110.090000, 20150110.101500 });
            openTime.Add(new double[] { 20150110.103000, 20150110.113000 });
            openTime.Add(new double[] { 20150110.133000, 20150110.150000 });

            KLineDataTimeInfo timeInfo = TradingTimeUtils.GetKLineDataTimeInfo(openTime, new KLinePeriod(KLineTimeType.MINUTE, 1));
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_15Minute);
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(timeInfo);
        }

        [TestMethod]
        public void TestGetKLineDataTimeInfo_Days()
        {
            IList<double[]>[] openTimeArr = new List<double[]>[2];
            openTimeArr[0] = GetDay1();
            openTimeArr[1] = GetDay2();

            KLineDataTimeInfo timeInfo = TradingTimeUtils.GetKLineDataTimeInfo(openTimeArr, new KLinePeriod(KLineTimeType.MINUTE, 1));
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_15Minute);
            //List<double[]> klineTimeList = TradingTimeUtils.GetKLineTimeList_Full(openTime, KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(timeInfo);
        }

        private static List<double[]> GetDay1()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { 20150107.210000, 20150108.023000 });
            openTime.Add(new double[] { 20150110.090000, 20150110.101500 });
            openTime.Add(new double[] { 20150110.103000, 20150110.113000 });
            openTime.Add(new double[] { 20150110.133000, 20150110.150000 });
            return openTime;
        }

        private static List<double[]> GetDay2()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { 20150110.210000, 20150111.023000 });
            openTime.Add(new double[] { 20150111.090000, 20150111.101500 });
            openTime.Add(new double[] { 20150111.103000, 20150111.113000 });
            openTime.Add(new double[] { 20150111.133000, 20150111.150000 });
            return openTime;
        }

        //[TestMethod]
        //public void TestGetKLineTimeList_DayOpenTime()
        //{
        //    List<double[]> openTime = new List<double[]>();
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });

        //    KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
        //    KLineOpenPeriods dayOpenTime = TradingTimeUtils.GetKLineTimeList_DayOpenTime(openTime, period);
        //    Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
        //    Assert.AreEqual(75, dayOpenTime.SplitIndeies[1]);
        //    Assert.AreEqual(135, dayOpenTime.SplitIndeies[2]);
        //    Assert.AreEqual(-1, dayOpenTime.OverNightIndex);
        //    AssertOpenTime(dayOpenTime.KlineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "OpenTime_Normal_1Minute"));
        //}

        //[TestMethod]
        //public void TestGetKLineTimeList_DayOpenTime_Night_Normal()
        //{
        //    List<double[]> openTime = new List<double[]>();
        //    openTime.Add(new double[] { .210000, .233000 });
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });

        //    KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
        //    KLineOpenPeriods dayOpenTime = TradingTimeUtils.GetKLineTimeList_DayOpenTime(openTime, period);

        //    //AssertOpenTime(dayOpenTime.KlineTimes, Resources.OpenTime_OverNight_1Minute);
        //    Assert.AreEqual(150, dayOpenTime.OverNightIndex);

        //    Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
        //    Assert.AreEqual(150, dayOpenTime.SplitIndeies[1]);
        //    Assert.AreEqual(225, dayOpenTime.SplitIndeies[2]);
        //    Assert.AreEqual(285, dayOpenTime.SplitIndeies[3]);
        //}

        //[TestMethod]
        //public void TestGetKLineTimeList_DayOpenTime_OverNight()
        //{
        //    List<double[]> openTime = new List<double[]>();
        //    openTime.Add(new double[] { .210000, .023000 });
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });

        //    KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
        //    KLineOpenPeriods dayOpenTime = TradingTimeUtils.GetKLineTimeList_DayOpenTime(openTime, period);

        //    AssertOpenTime(dayOpenTime.KlineTimes, TestCaseManager.LoadTestCaseFile(GetType(), "OpenTime_OverNight_1Minute.txt"));
        //    Assert.AreEqual(180, dayOpenTime.OverNightIndex);

        //    //for (int i = 0; i < dayOpenTime.SplitIndeies.Count; i++)
        //    //    Console.WriteLine(dayOpenTime.SplitIndeies[i]);

        //    Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
        //    Assert.AreEqual(330, dayOpenTime.SplitIndeies[1]);
        //    Assert.AreEqual(405, dayOpenTime.SplitIndeies[2]);
        //    Assert.AreEqual(465, dayOpenTime.SplitIndeies[3]);
        //}

        //private void AssertOpenTime(List<double> openTime, String result)
        //{
        //    string[] lines = result.Split('\r');
        //    Assert.AreEqual(lines.Length, openTime.Count);
        //    for (int i = 0; i < openTime.Count; i++)
        //    {
        //        Assert.AreEqual(double.Parse(lines[i]), openTime[i]);
        //    }
        //}

        //[TestMethod]
        //public void TestGetKLineTimeList()
        //{
        //    List<double[]> openTime = new List<double[]>();
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });

        //    KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
        //    List<double> klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(225, klineTimes.Count);
        //    Assert.AreEqual(0.09, klineTimes[0]);
        //    Assert.AreEqual(0.103, klineTimes[75]);
        //    Assert.AreEqual(0.133, klineTimes[135]);

        //    period = new KLinePeriod(KLineTimeType.SECOND, 5);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(2700, klineTimes.Count);

        //    period = new KLinePeriod(KLineTimeType.MINUTE, 5);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(45, klineTimes.Count);
        //    //for (int i = 0; i < klineTimes.Count; i++)
        //    //    Console.WriteLine(klineTimes[i]);

        //    period = new KLinePeriod(KLineTimeType.MINUTE, 15);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(15, klineTimes.Count);
        //    //for (int i = 0; i < klineTimes.Count; i++)
        //    //    Console.WriteLine(klineTimes[i]);

        //    period = new KLinePeriod(KLineTimeType.HOUR, 1);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(4, klineTimes.Count);
        //    Assert.AreEqual(0.09, klineTimes[0]);
        //    Assert.AreEqual(0.1, klineTimes[1]);
        //    Assert.AreEqual(0.1115, klineTimes[2]);
        //    Assert.AreEqual(0.1415, klineTimes[3]);

        //    openTime = new List<double[]>();
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });
        //    openTime.Add(new double[] { .210000, .233000 });
        //    period = new KLinePeriod(KLineTimeType.HOUR, 1);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    Assert.AreEqual(7, klineTimes.Count);
        //    Assert.AreEqual(0.09, klineTimes[0]);
        //    Assert.AreEqual(0.1, klineTimes[1]);
        //    Assert.AreEqual(0.1115, klineTimes[2]);
        //    Assert.AreEqual(0.1415, klineTimes[3]);
        //    Assert.AreEqual(0.21, klineTimes[4]);
        //    Assert.AreEqual(0.22, klineTimes[5]);
        //    Assert.AreEqual(0.23, klineTimes[6]);

        //    openTime = new List<double[]>();
        //    openTime.Add(new double[] { .210000, .023000 });
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });
        //    period = new KLinePeriod(KLineTimeType.HOUR, 1);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    //for(int i = 0; i < klineTimes.Count; i++)            
        //    //    Console.WriteLine(klineTimes[i]);
        //    Assert.AreEqual(10, klineTimes.Count);
        //    Assert.AreEqual(0.21, klineTimes[0]);
        //    Assert.AreEqual(0.22, klineTimes[1]);
        //    Assert.AreEqual(0.23, klineTimes[2]);
        //    Assert.AreEqual(0, klineTimes[3]);
        //    Assert.AreEqual(0.01, klineTimes[4]);
        //    Assert.AreEqual(0.02, klineTimes[5]);
        //    Assert.AreEqual(0.09, klineTimes[6]);

        //    openTime = new List<double[]>();
        //    openTime.Add(new double[] { .210000, .230000 });
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });
        //    period = new KLinePeriod(KLineTimeType.MINUTE, 1);
        //    klineTimes = TradingTimeUtils.GetKLineTimeList(openTime, period);
        //    for (int i = 0; i < klineTimes.Count; i++)
        //        Console.WriteLine(klineTimes[i]);
        //    Assert.AreEqual(345, klineTimes.Count);
        //    //Assert.AreEqual(0.21, klineTimes[0]);
        //    //Assert.AreEqual(0.22, klineTimes[1]);
        //    //Assert.AreEqual(0.23, klineTimes[2]);
        //    //Assert.AreEqual(0, klineTimes[3]);
        //    //Assert.AreEqual(0.01, klineTimes[4]);
        //    //Assert.AreEqual(0.02, klineTimes[5]);
        //    //Assert.AreEqual(0.09, klineTimes[6]);
        //}
    }
}
