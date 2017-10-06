using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    [TestClass]
    public class TestCodeReader
    {
        [TestMethod]
        public void TestGetAllInstruments()
        {
            //IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            IDataReader dataReader = GetDataReader();
            List<CodeInfo> instruments = dataReader.CodeReader.GetAllCodes();
            AssertUtils.PrintLineList(instruments);
            //dataReader.CodeReader.GetCodesByCatelog("M");
            //AssertUtils.AssertEqual_List("instruments", GetType(), instruments);

            CodeInfo instrument = dataReader.CodeReader.GetCodeInfo("m1005");
            Assert.AreEqual("M1005", instrument.Code);
            Assert.AreEqual("豆粕1005", instrument.Name);
            Assert.AreEqual("M", instrument.Catelog);
        }

        private static IDataReader GetDataReader()
        {
            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            return dataReader;
        }

        [TestMethod]
        public void TestGetAllCatelogs()
        {
            IDataReader dataReader = GetDataReader();
                //DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            List<string> catelogs = dataReader.CodeReader.GetAllCatelogs();
            Assert.AreEqual(true, catelogs.Count>=53);
            Assert.AreEqual("A", catelogs[0]);
        }
    }
}
