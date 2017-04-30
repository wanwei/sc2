using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using System.Collections.Generic;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestKLineDataStore
    {
        [TestMethod]
        public void TestKLineDataStore_SaveLoad()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestKLineDataStore_SaveLoad(uri);
            }
        }

        private void TestKLineDataStore_SaveLoad(string uri)
        {
            string code = "m05";
            KLinePeriod period = KLinePeriod.KLinePeriod_1Minute;
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            IKLineDataStore klineDataStore = dataStore.CreateKLineDataStore();

            try
            {
                IKLineData klineData = MockDataLoader.GetKLineData(code, 20100107, 20100120, period);

                klineDataStore.Save(code, period, klineData);

                KLineData klineData2 = klineDataStore.LoadAll(code, period);
                AssertUtils.AssertEqual_KLineData(klineData, klineData2);

                Assert.AreEqual(20100107, klineDataStore.GetFirstTradingDay(code, period));
                Assert.AreEqual(20100120, klineDataStore.GetLastTradingDay(code, period));
                Console.WriteLine(klineDataStore.GetLastTradingTime(code, period));
            }
            finally
            {
                klineDataStore.Delete(code, period);
            }
        }

        [TestMethod]
        public void TestKLineDataStore_Append()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestKLineDataStore_Append(uri);
            }
        }

        private void TestKLineDataStore_Append(string uri)
        {
            string code = "m05";
            KLinePeriod period = KLinePeriod.KLinePeriod_1Minute;
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            IKLineDataStore klineDataStore = dataStore.CreateKLineDataStore();
            try
            {
                IKLineData klineData = MockDataLoader.GetKLineData(code, 20100107, 20100114, period);
                IKLineData klineData2 = MockDataLoader.GetKLineData(code, 20100115, 20100120, period);

                List<IKLineData> ks = new List<IKLineData>();
                ks.Add(klineData);
                ks.Add(klineData2);
                IKLineData klineData_Merge = KLineData.Merge(ks);


                klineDataStore.Save(code, period, klineData);
                klineDataStore.Append(code, period, klineData2);

                IKLineData klineData_Merge2 = klineDataStore.LoadAll(code, period);
                AssertUtils.AssertEqual_KLineData(klineData_Merge, klineData_Merge2);
            }
            finally
            {
                klineDataStore.Delete(code, period);
            }
        }

        [TestMethod]
        public void TestKLineDataStore_LoadByDate()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestKLineDataStore_LoadByDate(uri);
            }
        }

        private void TestKLineDataStore_LoadByDate(string uri)
        {
            string code = "m05";
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            IKLineDataStore klineDataStore = dataStore.CreateKLineDataStore();
            try
            {
                IKLineData data = MockDataLoader.GetKLineData(code, 20100107, 20100120, klinePeriod);
                klineDataStore.Save(code, klinePeriod, data);

                IKLineData klineData = klineDataStore.Load(code, 20100107, 20100111, klinePeriod);
                IKLineData klineData2 = klineDataStore.Load(code, 20100112, 20100120, klinePeriod);
                List<IKLineData> ks = new List<IKLineData>();
                ks.Add(klineData);
                ks.Add(klineData2);
                IKLineData klineData_Merge = KLineData.Merge(ks);

                //AssertUtils.PrintKLineData(data);
                //AssertUtils.PrintKLineData(klineData_Merge);
                AssertUtils.PrintKLineData(klineData);
                //AssertUtils.PrintKLineData(klineData_Merge);
                AssertUtils.AssertEqual_KLineData(data, klineData_Merge);
            }
            finally
            {
                klineDataStore.Delete(code, klinePeriod);
            }
        }
    }
}
