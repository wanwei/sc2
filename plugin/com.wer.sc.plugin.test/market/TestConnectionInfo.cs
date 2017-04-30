using com.wer.sc.data.market;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.Properties;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    [TestClass]
    public class TestConnectionInfo
    {
        [TestMethod]
        public void TestLoadConnectionInfo()
        {
            string connectInfoStr = TestCaseManager.LoadTestCaseFile(GetType(), "ConnectionInfo");//,Encoding.GetEncoding("GBK"));
            //Console.WriteLine(connectInfoStr);
            //ConnectionInfo c = new ConnectionInfo();
            //c.AddValue("12", "22");
            //c.AddValue("22", "33");
            //Console.WriteLine(c.ToJsJson());

            ConnectionInfo connectInfo = ConnectionInfo.LoadFrom(connectInfoStr);
            //Console.WriteLine(connectInfo.ToJsJson());
            Assert.AreEqual("SIMNOW1", connectInfo.Id);
            Assert.AreEqual("SimuNow模拟CTP1", connectInfo.Name);
            Assert.AreEqual("SimuNow模拟CTP，一号线", connectInfo.Description);           
        }
    }
}