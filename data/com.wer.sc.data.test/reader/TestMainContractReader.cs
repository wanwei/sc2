using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    [TestClass]
    public class TestMainContractReader
    {
        [TestMethod]
        public void TestReadMainContract()
        {
            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            IList<MainContractInfo> contracts = dataReader.MainContractReader.GetMainContractInfos("RB", 20160101, 20170101);
            //AssertUtils.PrintLineList((IList)contracts);
            AssertUtils.AssertEqual_List("MainContract", GetType(), contracts);
        }
    }
}
