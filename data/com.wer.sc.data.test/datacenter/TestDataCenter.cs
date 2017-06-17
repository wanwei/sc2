using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datacenter
{
    [TestClass]
    public class TestDataCenter
    {
        [TestMethod]
        public void TestGetDataCenter()
        {
            string filePath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            DataCenter dataCenter = DataCenterManager.Create(filePath).GetDataCenter("file:/E:/FUTURES/MOCKDATACENTER/");
            List<CodeInfo> instruments = dataCenter.DataReader.CodeReader.GetAllCodes();
            AssertUtils.PrintLineList(instruments);
            AssertUtils.AssertEqual_List("instruments", GetType(), instruments);
        }
    }
}
