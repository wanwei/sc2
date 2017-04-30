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
    public class TestTradingSessionStore
    {
        [TestMethod]
        public void TestTradingSessionSaveLoad()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestTradingSessionSaveLoad(uri);
            }
        }

        private void TestTradingSessionSaveLoad(string uri)
        {
            string code = "M05";
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);
            ITradingSessionStore store = dataStore.CreateTradingSessionStore();

            try
            {
                List<TradingSession> codes = MockDataLoader.GetTradingSessions(code);
                store.Save(code, codes);
                List<TradingSession> codes2 = store.Load(code);
                AssertUtils.AssertEqual_List_ToString(codes, codes2);
            }
            finally
            {
                store.Delete(code);
            }
        }
    }
}
