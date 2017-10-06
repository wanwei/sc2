using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestTickDataStore
    {
        [TestMethod]
        public void TestTickDataStore_SaveLoad()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestTickDataStore_SaveLoad(uri);
            }
        }

        public void TestTickDataStore_SaveLoad(string uri)
        {
            string code = "m1005";
            int day = 20100108;

            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            ITickDataStore tickDataStore = dataStore.CreateTickDataStore();
            try
            {
                TickData data = (TickData)MockDataLoader.GetTickData(code, day);
                tickDataStore.Save(code, day, data);

                TickData data2 = tickDataStore.Load(code, day);
                AssertUtils.AssertEqual_TickData(data, data2);
            }
            finally
            {
                tickDataStore.Delete(code, day);
            }
        }

        [TestMethod]
        public void TestTickDataStore_Append()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestTickDataStore_Append(uri);
            }
        }

        public void TestTickDataStore_Append(string uri)
        {
            string code = "m1005";
            int day = 20100108;
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            ITickDataStore tickDataStore = dataStore.CreateTickDataStore();
            try
            {
                TickData data = (TickData)MockDataLoader.GetTickData(code, day);

                TickData d1 = data.SubData(0, 100);
                TickData d2 = data.SubData(101, data.Length - 1);

                tickDataStore.Save(code, day, d1);
                tickDataStore.Append(code, day, d2);

                TickData data2 = tickDataStore.Load(code, day);

                AssertUtils.AssertEqual_TickData(data, data2);
            }
            finally
            {
                tickDataStore.Delete(code, day);
            }
        }
    }
}
