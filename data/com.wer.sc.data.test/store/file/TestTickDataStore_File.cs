using com.wer.sc.data.store;
using com.wer.sc.data.update;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    [TestClass]
    public class TestTickDataStore_File
    {
        [TestMethod]
        public void TestTickDataStore_FromBytes()
        {
            TickData data = (TickData)MockDataLoader.GetTickData("m05", 20100108);
            byte[] bs = TickDataStore_File_Single.GetBytes(data);
            TickData data2 = TickDataStore_File_Single.FromBytes(bs, 0, bs.Length);
            AssertUtils.AssertEqual_TickData(data, data2);
        }

        [TestMethod]
        public void TestTickDataStore_SaveLoad()
        {
            TickData data = (TickData)MockDataLoader.GetTickData("m05", 20100108);
            String path = TestCaseManager.GetTestCasePath(GetType(), "output_tick_saveload");
            TickDataStore_File_Single store = new TickDataStore_File_Single(path);
            store.Save(data);

            TickDataStore_File_Single store2 = new TickDataStore_File_Single(path);
            TickData data2 = store2.Load();
            AssertUtils.AssertEqual_TickData(data, data2);
            File.Delete(path);
        }

        [TestMethod]
        public void TestTickDataStore_Append()
        {
            TickData data = (TickData)MockDataLoader.GetTickData("m05", 20100108);

            String path = TestCaseManager.GetTestCasePath(GetType(), "output_");
            TickData d1 = data.SubData(0, 100);
            TickData d2 = data.SubData(101, data.Length - 1);

            TickDataStore_File_Single store = new TickDataStore_File_Single(path);
            store.Save(d1);

            TickDataStore_File_Single store2 = new TickDataStore_File_Single(path);
            store2.Append(d2);

            TickDataStore_File_Single store3 = new TickDataStore_File_Single(path);
            TickData data2 = store3.Load();

            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }        
    }
}
