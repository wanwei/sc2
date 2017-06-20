using com.wer.sc.data.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data.test
{
    public class DataTestUtils
    {
        public static IKLineData GetKLineData(string code, int start, int end, KLinePeriod period, List<double[]> openTime)
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            ITickDataReader tickReader = fac.TickDataReader;
            IList<int> dates = fac.OpenDateReader.GetOpenDates(start, end);
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                TickData tickData = tickReader.GetTickData(code, date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }
            return DataTransfer_Tick2KLine.Transfer(dataList, period, openTime);
        }

        public static List<double[]> GetOpenTimeOverNight()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        public static List<double[]> GetOpenTimeNight()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }
        public static List<double[]> GetOpenTimeNormal()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        public static void AssertKLineDataResult(IKLineData klineData, String txt)
        {
            string[] periodArr = txt.Split('\r');
            Assert.AreEqual(periodArr.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, klineData.ToString());
            }
        }

        public static void AssertTickDataResult(ITickData tickData, string txt)
        {
            string[] periodArr = txt.Split('\r');
            Assert.AreEqual(periodArr.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, tickData.ToString());
            }
        }

        public static void AssertDates(List<int> dates, string txt)
        {
            string[] periodArr = txt.Split('\r');
            for (int i = 0; i < dates.Count; i++)
            {
                Assert.AreEqual(int.Parse(periodArr[i]), dates[i]);
            }
        }
    }
}
