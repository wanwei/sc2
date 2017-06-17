using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    [TestClass]
    public class TestIKLineDataReader
    {
        [TestMethod]
        public void TestKLineData_GetDate()
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            Assert.AreEqual(20040102, dataReader.KLineDataReader.GetFirstDate("m05", KLinePeriod.KLinePeriod_1Minute));
            Assert.AreEqual(20160429, dataReader.KLineDataReader.GetLastDate("m05", KLinePeriod.KLinePeriod_1Minute));
        }

        [TestMethod]
        public void TestKLineData_GetData_M01_20040101_20040130()
        {
            TestKLineData_GetData("m01", 20040101, 20040130, KLinePeriod.KLinePeriod_1Minute, "KLineData_M01_20040101_20040130");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20130101_20151231()
        {
            TestKLineData_GetData("m05", 20130101, 20151231, KLinePeriod.KLinePeriod_1Minute, "KLineData_M05_20130101_20151231_1Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150116_15Minute()
        {
            TestKLineData_GetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_15Minute, "KLineData_M05_20141215_20150116_15Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150116_1Minute()
        {
            TestKLineData_GetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_1Minute, "KLineData_M05_20141215_20150116_1Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150116_Day()
        {
            TestKLineData_GetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_1Day, "KLineData_M05_20141215_20150116_Day");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150127_15Minute()
        {
            TestKLineData_GetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_15Minute, "KLineData_M05_20141215_20150127_15Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150127_1Minute()
        {
            TestKLineData_GetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_1Minute, "KLineData_M05_20141215_20150127_1Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141215_20150127_1Day()
        {
            TestKLineData_GetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_1Day, "KLineData_M05_20141215_20150127_Day");
        }

        [TestMethod]
        public void TestKLineData_GetData_M09_20141215_20150127_1Minute()
        {
            TestKLineData_GetData("m09", 20141215, 20150127, KLinePeriod.KLinePeriod_1Minute, "KLineData_M09_20141215_20150127_1Minute");
        }

        [TestMethod]
        public void TestKLineData_GetData_M05_20141229_20140129_1Minute()
        {
            TestKLineData_GetData("m05", 20141229, 20141229, KLinePeriod.KLinePeriod_1Minute, "KLineData_M05_20141229_20141229_1Minute");
        }

        private void TestKLineData_GetData(string code, int start, int end, KLinePeriod period, string fileName)
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, start, end, period);
            //AssertUtils.PrintKLineData(klineData);
            AssertUtils.AssertEqual_KLineData(fileName, GetType(), klineData);
        }

        [TestMethod]
        public void TestKLineDataReaderGetAll()
        {
            IKLineDataReader reader = DataReaderFactory.CreateDataReader(DataCenterUri.URI).KLineDataReader;
            IKLineData data_m01 = reader.GetAllData("m01", KLinePeriod.KLinePeriod_1Minute);

            IKLineData data_m03 = reader.GetAllData("m03", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m03);
            IKLineData data_m05 = reader.GetAllData("m05", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m05);
            IKLineData data_m07 = reader.GetAllData("m07", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m07);
            IKLineData data_m08 = reader.GetAllData("m08", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m08);
            IKLineData data_m09 = reader.GetAllData("m09", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m09);
            IKLineData data_m11 = reader.GetAllData("m11", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m11);
            IKLineData data_m12 = reader.GetAllData("m12", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m12);
            IKLineData data_m13 = reader.GetAllData("m13", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m13);
            IKLineData data_mmi = reader.GetAllData("mmi", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_mmi);
        }

        private void AssertKLineTime(IKLineData srcKLineData, IKLineData targetKLineData)
        {
            //Assert.AreEqual(srcKLineData.Length, targetKLineData.Length);
            for (int i = 0; i < srcKLineData.Length; i++)
                Assert.AreEqual(srcKLineData.Arr_Time[i], targetKLineData.Arr_Time[i]);
        }
    }
}
