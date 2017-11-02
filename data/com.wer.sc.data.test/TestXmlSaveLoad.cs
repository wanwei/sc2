using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestXmlSaveLoad
    {
        private XmlElement GetXmlRoot()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            return root;
        }

        [TestMethod]
        public void TestSaveLoad_DataPackage()
        {
            string code = "rb1710";
            int start = 20170601;
            int end = 20170602;
            int minKLineBefore = 100;
            int minKLineAfter = 50;
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end, minKLineBefore, minKLineAfter);
            XmlElement root = GetXmlRoot();
            dataPackage.Save(root);

            DataPackage_Code dataPackage2 = (DataPackage_Code)DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(root);
            Assert.AreEqual(code, dataPackage2.Code);
            Assert.AreEqual(start, dataPackage2.StartDate);
            Assert.AreEqual(end, dataPackage2.EndDate);
            Assert.AreEqual(minKLineBefore, dataPackage2.MinBefore);
            Assert.AreEqual(minKLineAfter, dataPackage2.MinAfter);
        }

        [TestMethod]
        public void TestSaveLoad_DataForward_Code()
        {
            string code = "rb1710";
            int startDate = 20170601;
            int endDate = 20170602;

            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Hour, KLinePeriod.KLinePeriod_1Day }, true, false);
            IDataForward_Code dataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(code, startDate, endDate, referedPeriods, forwardPeriod);

            for (int i = 0; i < 100; i++)
                dataForward.Forward();
            Console.WriteLine(XmlUtils.ToString(dataForward));
            XmlElement root = GetXmlRoot();
            dataForward.Save(root);

            IDataForward_Code dataForward2 = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(root);
            for (int i = 0; i < 100; i++) {
                dataForward.Forward();
                dataForward2.Forward();
            }

            Console.WriteLine(XmlUtils.ToString(dataForward2));
            Assert.AreEqual(dataForward.Time, dataForward2.Time);
        }
    }
}
