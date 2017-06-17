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
    public class TestTimeLineDataReader
    {
        [TestMethod]
        public void TestTimeLineData_M05_20100108()
        {
            TestGetTimeLineData("m1005", 20100108);
        }

        private void TestGetTimeLineData(string code, int date)
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            ITimeLineData timeLineData = dataReader.TimeLineDataReader.GetData(code, date);
            AssertUtils.PrintTimeLineData(timeLineData);
            AssertUtils.AssertEqual_TimeLineData("TimeLineData_M05_20100108", GetType(), timeLineData);
        }
    }
}
