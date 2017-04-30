using com.wer.sc.data.datacenter;
using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataProceed_DataUpdate
    {
        [TestMethod]
        public void TestDataProceed_DataUpdate_Prepare()
        {
            IPlugin_HistoryData plugin_HistoryData = new Plugin_History_MockUpdate(@"E:\FUTURES\MOCKDATAUPDATE\UpdateData1\");
            string configFilePath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            DataCenter dataCenter = DataCenterManager.Create(configFilePath).GetDataCenter("file:/E:/FUTURES/MOCKDATAUPDATE/DataCenter/");
            IDataStore dataStore = dataCenter.DataStore;

            DataUpdate dataProceed = new DataUpdate(plugin_HistoryData, dataCenter, true);
            List<IStep> steps = dataProceed.GetSteps();
            AssertUtils.AssertEqual_List("Steps_DataProceed_DataUpdate", GetType(), steps);
        }

        [TestMethod]
        public void TestDataProceed_DataUpdate_Update()
        {
            IPlugin_HistoryData plugin_HistoryData = new Plugin_History_MockUpdate(@"E:\FUTURES\MOCKDATAUPDATE\UpdateData1\");
            string configFilePath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            string path = "E:/FUTURES/MOCKDATAUPDATE/EMPTYDataCenter/";
            string uri = "file:/" + path;
            DataCenter dataCenter = DataCenterManager.Create(configFilePath).GetDataCenter(uri);
            IDataStore dataStore = dataCenter.DataStore;

            DataUpdate dataProceed = new DataUpdate(plugin_HistoryData, dataCenter, true);
            List<IStep> steps = dataProceed.GetSteps();
            for (int i = 0; i < steps.Count; i++)
                steps[i].Proceed();

            DataCenter newDataCenter = DataCenterManager.Create(configFilePath).GetDataCenter(uri);
            IInstrumentReader reader = newDataCenter.DataReader.InstrumentReader;
            AssertUtils.PrintLineList(reader.GetAllInstruments());

            //TODO

            Thread.Sleep(2000);
            Directory.Delete(path, true);
        }
    }
}