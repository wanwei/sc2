using com.wer.sc.data.reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate.impl
{
    [TestClass]
    public class TestDataNavigate_Code_TimeLine
    {
        [TestMethod]
        public void TestNavigate_TimeLine()
        {
            string code = "rb1705";
            double time = 20170405.093001;
            IDataReader dataReader = DataReaderFactory.CreateDataReader(@"e:\scdata\cnfutures\");
            DataNavigate_Code_TimeLine navigate = new DataNavigate_Code_TimeLine(dataReader, code, time);
            ITimeLineData timeLineData = navigate.GetTimeLineData();
            Console.WriteLine(timeLineData);
        }
    }
}
