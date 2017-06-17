using com.wer.sc.data.datacenter;
using com.wer.sc.data.store;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    //[TestClass]
    //public class TestStepGetter_UpdateKLineData
    //{
    //    [TestMethod]
    //    public void TestGetUpdateKLineDataSteps()
    //    {
    //        IPlugin_HistoryData plugin_HistoryData = new Plugin_History_MockUpdate(@"E:\FUTURES\MOCKDATAUPDATE\UpdateData1\");
    //        string configFilePath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
    //        DataCenter dataCenter = DataCenterManager.Create(configFilePath).GetDataCenter("file:/E:/FUTURES/MOCKDATAUPDATE/DataCenter/");
    //        IDataStore dataStore = dataCenter.DataStore;

    //        StepGetter_UpdateKLineData stepGetter = new StepGetter_UpdateKLineData(plugin_HistoryData, dataStore, dataCenter.Config.StoredDataTypes.StoreKLinePeriods, true);
    //        AssertUtils.AssertEqual_List("Steps_UpdateKLineData1", GetType(), stepGetter.GetSteps());
    //    }
    //}
}
