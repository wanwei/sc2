using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestTradingDayStore
    {
        [TestMethod]
        public void TestTradingDaySaveLoad()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestTradingDaySaveLoad(uri);
            }
        }

        private void TestTradingDaySaveLoad(string uri)
        {
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);

            ITradingDayStore store = dataStore.CreateTradingDayStore();
            List<int> codes = MockDataLoader.GetAllTradingDays();
            store.Save(codes);

            List<int> codes2 = store.Load();
            AssertUtils.AssertEqual_List_ToString(codes, codes2);
            store.Delete();
        }
    }
}