using com.wer.sc.data.datapackage;
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
            DataCenter dataCenter = DataCenter.Default;
            IDataReader dataReader = dataCenter.DataReader;
            //DataReaderFactory.CreateDataReader(@"e:\scdata\cnfutures\");
            IDataPackage_Code dataPackage = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, 20170301, 20170410);
            //DataPackageFactory.CreateDataPackage(dataReader, code, 20170301, 20170410);
            DataNavigate_Code_TimeLine navigate = new DataNavigate_Code_TimeLine(dataPackage, time);
            ITimeLineData timeLineData = navigate.GetTimeLineData();
            Console.WriteLine(timeLineData);

            string txt = "20170405.093001,3470,63,1.85,272,731414";
            Assert.AreEqual(txt, timeLineData.ToString());
        }
    }
}
