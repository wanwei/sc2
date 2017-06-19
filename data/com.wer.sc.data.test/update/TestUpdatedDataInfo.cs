using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestUpdatedDataInfo
    {
        private const string CODE1 = "m1401";
        private const string CODE2 = "m1405";

        [TestMethod]
        public void TestSaveLoadUpdatedDataInfo()
        {
            string path = TestCaseManager.GetTestCasePath(GetType(), "path_updateinfo");
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            UpdatedDataInfo updatedDataInfo = new UpdatedDataInfo(path);

            updatedDataInfo.WriteUpdateInfo_Tick(CODE1, 20140120);
            updatedDataInfo.WriteUpdateInfo_Tick(CODE2, 20140520);
            updatedDataInfo.WriteUpdateInfo_KLine(CODE1, KLinePeriod.KLinePeriod_1Minute, 20140121);
            updatedDataInfo.WriteUpdateInfo_KLine(CODE1, KLinePeriod.KLinePeriod_5Minute, 20140122);
            updatedDataInfo.WriteUpdateInfo_KLine(CODE1, KLinePeriod.KLinePeriod_15Minute, 20140123);
            updatedDataInfo.WriteUpdateInfo_KLine(CODE1, KLinePeriod.KLinePeriod_1Day, 20140124);
            updatedDataInfo.WriteUpdateInfo_KLine(CODE2, KLinePeriod.KLinePeriod_1Minute, 20140525);
            updatedDataInfo.Save();

            UpdatedDataInfo updatedDataInfo2 = new UpdatedDataInfo(path);
            Assert.AreEqual(20140120, updatedDataInfo2.GetLastUpdatedTickData(CODE1));
            Assert.AreEqual(20140520, updatedDataInfo2.GetLastUpdatedTickData(CODE2));

            Assert.AreEqual(20140121, updatedDataInfo2.GetLastUpdatedKLineData(CODE1, KLinePeriod.KLinePeriod_1Minute));
            Assert.AreEqual(20140122, updatedDataInfo2.GetLastUpdatedKLineData(CODE1, KLinePeriod.KLinePeriod_5Minute));
            Assert.AreEqual(20140123, updatedDataInfo2.GetLastUpdatedKLineData(CODE1, KLinePeriod.KLinePeriod_15Minute));
            Assert.AreEqual(20140124, updatedDataInfo2.GetLastUpdatedKLineData(CODE1, KLinePeriod.KLinePeriod_1Day));
            Assert.AreEqual(20140525, updatedDataInfo2.GetLastUpdatedKLineData(CODE2, KLinePeriod.KLinePeriod_1Minute));

            Directory.Delete(path, true);
        }
    }
}
