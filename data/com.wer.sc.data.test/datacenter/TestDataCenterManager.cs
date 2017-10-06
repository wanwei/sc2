using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datacenter
{
    [TestClass]
    public class TestDataCenterManager
    {
        [TestMethod]
        public void TestCreateDataCenter()
        {
            DataCenter dataCenter = DataCenterManager.Instance.GetDataCenterByUri("file:/d:/scdata/cnfutures/");
            Assert.AreEqual("file:/d:/scdata/cnfutures/", dataCenter.Config.Uri);
        }

        [TestMethod]
        public void TestCreateDataCenter2()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            DataCenterManager mgr = DataCenterManager.Create(path);
            string uri = "file:/e:/FUTURES/MOCKDATACENTER/";
            DataCenter dataCenter = mgr.GetDataCenterByUri(uri);
            Assert.AreEqual(uri, dataCenter.Config.Uri);
        }

        [TestMethod]
        public void TestRegisterDataCenter()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "output_RegisterDataCenter.config");
            try
            {
                DataCenterManager mgr = DataCenterManager.Create(path);

                string uri = "file:/e:/futures/mockdatacenter/";
                string id = "d1";
                DataCenterInfo config = GetDataCenterInfo(uri, id);
                mgr.RegisterDataCenter(config);
                Assert.AreEqual(1, mgr.GetAllConfig().Count);
                DataCenter dataCenter = mgr.GetDataCenterByUri(uri);
                Assert.AreEqual(uri, dataCenter.Config.Uri);

                string uri2 = "file:/d:/scdata/cnfutures/";
                string id2 = "d2";
                config = GetDataCenterInfo(uri2, id2);
                mgr.RegisterDataCenter(config);
                Assert.AreEqual(2, mgr.GetAllConfig().Count);

                mgr.UnRegisterDataCenter(uri);
                dataCenter = mgr.GetDataCenterByUri(uri);
                Assert.IsNull(dataCenter);
                Assert.AreEqual(1, mgr.GetAllConfig().Count);
            }
            finally
            {
                File.Delete(path);
            }
        }

        private static DataCenterInfo GetDataCenterInfo(string uri, string id)
        {
            DataCenterInfo config = new DataCenterInfo();
            config.MarketType = plugin.MarketType.CnStock;
            config.DataCenterStoreMethod = StoreMethod.File;
            config.Id = id;
            config.Uri = uri;
            config.StoredDataTypes.IsStoredTradingDay = true;
            config.StoredDataTypes.IsStoreTick = true;
            config.StoredDataTypes.IsStoreTradingSession = true;
            config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            return config;
        }
    }
}
