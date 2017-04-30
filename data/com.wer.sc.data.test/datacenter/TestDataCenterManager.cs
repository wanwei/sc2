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
            DataCenter dataCenter = DataCenterManager.Instance.GetDataCenter("file:/d:/scdata/cnfutures/");
            Assert.AreEqual("file:/d:/scdata/cnfutures/", dataCenter.Config.Uri);
        }

        [TestMethod]
        public void TestCreateDataCenter2()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            DataCenterManager mgr = DataCenterManager.Create(path);
            string uri = "file:/e:/FUTURES/MOCKDATACENTER/";
            DataCenter dataCenter = mgr.GetDataCenter(uri);
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

                DataCenterConfig config = new DataCenterConfig();
                config.MarketType = plugin.MarketType.CnStock;
                config.DataCenterStoreMethod = StoreMethod.File;
                config.Uri = uri;
                config.StoredDataTypes.IsStoredTradingDay = true;
                config.StoredDataTypes.IsStoreTick = true;
                config.StoredDataTypes.IsStoreTradingSession = true;
                config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
                config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
                config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
                config.StoredDataTypes.StoreKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
                mgr.RegisterDataCenter(config);                
                Assert.AreEqual(1, mgr.GetAllConfig().Count);

                DataCenter dataCenter = mgr.GetDataCenter(uri);
                Assert.AreEqual(uri, dataCenter.Config.Uri);
                string uri2 = "file:/d:/scdata/cnfutures/";
                config.Uri = uri2;
                mgr.RegisterDataCenter(config);
                Assert.AreEqual(2, mgr.GetAllConfig().Count);

                mgr.UnRegisterDataCenter(uri);
                dataCenter = mgr.GetDataCenter(uri);
                Assert.IsNull(dataCenter);
                Assert.AreEqual(1, mgr.GetAllConfig().Count);
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
