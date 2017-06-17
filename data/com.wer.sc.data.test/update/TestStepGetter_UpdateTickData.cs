using com.wer.sc.data.datacenter;
using com.wer.sc.data.store;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestStepGetter_UpdateTickData
    {
        //[TestMethod]
        //public void TestGetUpdateTickDataSteps()
        //{
        //    IPlugin_HistoryData plugin_HistoryData = new Plugin_History_MockUpdate(@"E:\FUTURES\MOCKDATAUPDATE\UpdateData1\");
        //    string configFilePath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
        //    DataCenter dataCenter = DataCenterManager.Create(configFilePath).GetDataCenter("file:/E:/FUTURES/MOCKDATAUPDATE/DataCenter/");
        //    IDataStore dataStore = dataCenter.DataStore;

        //    StepGetter_UpdateTickData stepGetter = new StepGetter_UpdateTickData(plugin_HistoryData, dataStore, true);
        //    AssertUtils.PrintLineList(stepGetter.GetSteps());
        //    AssertUtils.AssertEqual_List("Steps_UpdateTickData", GetType(), stepGetter.GetSteps());
        //}
    }
}
