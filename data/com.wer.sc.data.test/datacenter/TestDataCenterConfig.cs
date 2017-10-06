using com.wer.sc.data.datacenter;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.datacenter
{
    [TestClass]
    public class TestDataCenterConfig
    {
        [TestMethod]
        public void TestDataCenterConfig_LoadXml()
        {
            string xml = TestCaseManager.LoadTestCaseFile(GetType(), "datacenter.config");
            DataCenterInfo config = new DataCenterInfo();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            config.LoadXml((XmlElement)doc.DocumentElement.ChildNodes[0]);

            AssertDataCenterConfig(config);
        }

        private static void AssertDataCenterConfig(DataCenterInfo config)
        {
            Assert.AreEqual(StoreMethod.File, config.DataCenterStoreMethod);
            Assert.AreEqual(MarketType.CnFutures, config.MarketType);
            Assert.AreEqual("file:/d:/scdata/cnfutures/", config.Uri);
            StoreDataTypes storeDataTypes = config.StoredDataTypes;
            Assert.AreEqual(true, storeDataTypes.IsStoredTradingDay);
            Assert.AreEqual(true, storeDataTypes.IsStoreTick);
            Assert.AreEqual(true, storeDataTypes.IsStoreTradingSession);
            List<KLinePeriod> klinePeriods = storeDataTypes.StoreKLinePeriods;
            Assert.AreEqual(6, klinePeriods.Count);
            Assert.AreEqual(new KLinePeriod(KLineTimeType.SECOND, 15), klinePeriods[0]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Minute, klinePeriods[1]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_5Minute, klinePeriods[2]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_15Minute, klinePeriods[3]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Hour, klinePeriods[4]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Day, klinePeriods[5]);
        }
    }
}