using com.wer.sc.data;
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
    public class TestCsvUtils_Instruments
    {
        [TestMethod]
        public void TestInstrumentsSaveLoad()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "CsvUtils_Instruments");
            List<CodeInfo> instruments = CsvUtils_Code.Load(path);

            string outputPath = TestCaseManager.GetTestCasePath(GetType(), "Instruments_Output.csv");
            CsvUtils_Code.Save(outputPath, instruments);

            List<CodeInfo> newInstruments = CsvUtils_Code.Load(outputPath);
            AssertUtils.AssertEqual_List(instruments, newInstruments);
            //TestCaseManager.SaveTestCaseFile(GetType(),)
            //List<InstrumentInfo> instruments = MockDataLoader.GetAllInstruments();
            //AssertUtils.AssertEqual_List("CsvUtils_Instruments", GetType(), instruments);
        }
    }
}
