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
        //[TestMethod]
        //public void TestLoadInstruments()
        //{
        //    string pluginPath = ScConfig.Instance.ScPath;
        //    CodeInfoGenerator dataLoader = new CodeInfoGenerator(pluginPath);
        //    List<CodeInfo> instruments = dataLoader.GetAllInstruments();
        //    AssertUtils.PrintLineList(instruments);
        //    AssertUtils.AssertEqual_List<CodeInfo>("instruments", GetType(), instruments);
        //}

        [TestMethod]
        public void TestGetEndDay()
        {
            int day = CodeInfoGenerator.GetEndDay(2010, 8);
            //Console.WriteLine(day);
            Assert.AreEqual(20100820, day);
        }
    }
}
