using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.test
{
    [TestClass]
    public class TestLogHelper
    {
        [TestMethod]
        public void TestPrint()
        {
            LogHelper.Info(GetType(), "LogInfo");
            LogHelper.Info(GetType(), new ApplicationException("LogInfo_Exception"));
            LogHelper.Warn(GetType(), "WarnInfo");
            LogHelper.Warn(GetType(), new ApplicationException("WarnInfo_Exception"));
            LogHelper.Error(GetType(), "ErrorInfo");
            LogHelper.Error(GetType(), new ApplicationException("ErrorInfo_Exception"));
            LogHelper.Fatal(GetType(), "FatalInfo");
            LogHelper.Fatal(GetType(), new ApplicationException("FatalInfo_Exception"));
        }
    }
}
