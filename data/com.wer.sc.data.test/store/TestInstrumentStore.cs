using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestInstrumentStore
    {
        [TestMethod]
        public void TestInstrumentsSaveLoad()
        {
            List<string> uris = UriGetter.GetAllTestUris();
            foreach (string uri in uris)
            {
                TestInstrumentsSaveLoad(uri);
            }
        }

        private void TestInstrumentsSaveLoad(string uri)
        {            
            IDataStore dataStore = DataStoreFactory.CreateDataStore(uri);

            ICodeStore store = dataStore.CreateInstrumentStore();

            List<CodeInfo> codes = MockDataLoader.GetAllInstruments();
            store.Save(codes);

            List<CodeInfo> codes2 = store.Load();
            AssertUtils.AssertEqual_List_ToString(codes, codes2);
            store.Delete();
        }
    }
}
