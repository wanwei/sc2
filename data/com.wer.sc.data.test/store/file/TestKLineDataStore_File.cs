using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using System.Collections.Generic;

namespace com.wer.sc.data.store.file
{
    [TestClass]
    public class TestKLineDataStore_File
    {
        [TestMethod]
        public void TestKLineDataStore_SaveLoad()
        {
            IKLineData klineData = MockDataLoader.GetKLineData("m05", 20100107, 20100120, KLinePeriod.KLinePeriod_1Minute);

            String path = TestCaseManager.GetTestCasePath(GetType(), "output_20100107_20100120");
            KLineDataStore_File_Single store = new KLineDataStore_File_Single(path);
            store.Save(klineData);

            KLineDataStore_File_Single store2 = new KLineDataStore_File_Single(path);
            KLineData klineData2 = store.LoadAll();
            AssertUtils.AssertEqual_KLineData(klineData, klineData2);
            File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_Append()
        {
            IKLineData klineData = MockDataLoader.GetKLineData("m05", 20100107, 20100114, KLinePeriod.KLinePeriod_1Minute);
            IKLineData klineData2 = MockDataLoader.GetKLineData("m05", 20100115, 20100120, KLinePeriod.KLinePeriod_1Minute);

            List<IKLineData> ks = new List<IKLineData>();
            ks.Add(klineData);
            ks.Add(klineData2);
            IKLineData klineData_Merge = KLineData.Merge(ks);

            String path = TestCaseManager.GetTestCasePath(GetType(), "output_append");
            KLineDataStore_File_Single store = new KLineDataStore_File_Single(path);
            store.Save(klineData);
            store.Append(klineData2);

            IKLineData klineData_Merge2 = store.LoadAll();
            AssertUtils.AssertEqual_KLineData(klineData_Merge, klineData_Merge2);

            File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_LoadByIndex()
        {
            //String path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.kline");
            //IKLineData data = LoadKLineData();

            //KLineDataStore store = new KLineDataStore(path);
            //store.Save(data);

            //KLineDataStore store2 = new KLineDataStore(path);
            //IKLineData data2 = store2.LoadByIndex(50, 100);

            //for (int i = 50; i <= 100; i++)
            //{
            //    data.BarPos = i;
            //    data2.BarPos = i - 50;
            //    Assert.AreEqual(data.ToString(), data2.ToString());
            //}

            //File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_LoadByDate()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "output_20100107_20100120");

            IKLineData data = MockDataLoader.GetKLineData("m05", 20100107, 20100120, KLinePeriod.KLinePeriod_1Minute);
            KLineDataStore_File_Single store = new KLineDataStore_File_Single(path);
            store.Save(data);

            KLineDataStore_File_Single store2 = new KLineDataStore_File_Single(path);
            IKLineData data2 = store2.Load(20100107, 20120111);
            AssertUtils.PrintKLineData(data2);
            File.Delete(path);
        }        
    }
}
