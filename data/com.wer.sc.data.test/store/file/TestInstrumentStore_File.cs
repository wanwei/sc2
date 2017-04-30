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
    public class TestInstrumentStore_File
    {
        [TestMethod]
        public void TestInstrumentsSaveLoad()
        {
            String outputPath = TestCaseManager.GetTestCasePath(GetType(), "codes");
            string instrumentPath = TestCaseManager.GetTestCasePath(GetType(), "Store_Code");

            InstrumentStore_File store = new InstrumentStore_File(instrumentPath);            
            List<CodeInfo> codes = store.Load();

            InstrumentStore_File store2 = new InstrumentStore_File(outputPath);
            store2.Save(codes);
            List<CodeInfo> codes2 = store2.Load();

            AssertUtils.AssertEqual_List_ToString(codes, codes2);
            File.Delete(outputPath);
        }
    }
}
