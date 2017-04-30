using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.transfer;

namespace com.wer.sc.mockdata
{
    [TestClass]
    public class TestTestCaseManager
    {
        [TestMethod]
        public void TestLoadTestCaseFile()
        {
            string str = TestCaseManager.LoadTestCaseFile(GetType(), "TestCaseLoader");
            Assert.AreEqual("1234567890", str);

            str = TestCaseManager.LoadTestCaseFile("com.wer.sc.mockdata", "TestCaseLoader");
            Assert.AreEqual("1234567890", str);
        }

        [TestMethod]
        public void TestSaveTestCaseFile()
        {
            TestCaseManager.SaveTestCaseFile(GetType(), "TestCaseSaver", "0987654321");
            string str = TestCaseManager.LoadTestCaseFile(GetType(), "TestCaseSaver");
            Assert.AreEqual("0987654321", str);
        }
    }
}
