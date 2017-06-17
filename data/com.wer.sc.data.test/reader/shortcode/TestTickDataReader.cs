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
    public class TestTickDataReader
    {
        [TestMethod]
        public void TestTickData_M01_20131231()
        {
            TestLoadTickData("m01", 20131231, "TickData_M01_20131231");
        }

        [TestMethod]
        public void TestTickData_M01_20141223()
        {
            TestLoadTickData("m01", 20141223, "TickData_M01_20141223");
        }

        [TestMethod]
        public void TestTickData_M05_20150121()
        {
            TestLoadTickData("m05", 20150121, "TickData_M05_20150121");
        }

        [TestMethod]
        public void TestTickData_M09_20141223()
        {
            TestLoadTickData("m09", 20141223, "TickData_M09_20141223");
        }

        private void TestLoadTickData(string code, int date, string fileName)
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            ITickData klineData = dataReader.TickDataReader.GetTickData(code, date);
            AssertUtils.AssertEqual_TickData(fileName, GetType(), klineData);
        }
    }
}
