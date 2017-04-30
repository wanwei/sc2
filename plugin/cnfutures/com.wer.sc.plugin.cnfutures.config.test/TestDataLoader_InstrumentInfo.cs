using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.mockdata;
using com.wer.sc.utils;

namespace com.wer.sc.plugin.cnfutures.config
{
    [TestClass]
    public class TestDataLoader_InstrumentInfo
    {
        [TestMethod]
        public void TestLoadInstruments()
        {
            string pluginPath = ScConfig.Instance.ScPath;
            DataLoader_InstrumentInfo dataLoader = new DataLoader_InstrumentInfo(pluginPath);
            List<CodeInfo> instruments = dataLoader.GetAllInstruments();
            AssertUtils.AssertEqual_List<CodeInfo>("instruments", GetType(), instruments);
        }
    }
}
