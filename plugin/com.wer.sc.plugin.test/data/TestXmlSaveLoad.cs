using com.wer.sc.data;
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
        [TestMethod]
        public void TestCompareTo()
        {
            KLinePeriod period_5Second = KLinePeriod.KLinePeriod_5Second;
            KLinePeriod period_1Minute = KLinePeriod.KLinePeriod_1Minute;
            KLinePeriod period_15Minute = KLinePeriod.KLinePeriod_15Minute;
            KLinePeriod period_1Hour = KLinePeriod.KLinePeriod_1Hour;
            KLinePeriod period_1Day = KLinePeriod.KLinePeriod_1Day;

            Assert.AreEqual(0, period_1Minute.CompareTo(KLinePeriod.KLinePeriod_1Minute));
            Assert.AreEqual(1, period_1Minute.CompareTo(period_5Second));
            Assert.AreEqual(1, period_15Minute.CompareTo(period_1Minute));
            Assert.AreEqual(1, period_1Hour.CompareTo(period_1Minute));
            Assert.AreEqual(1, period_1Day.CompareTo(period_1Hour));
        }

        private XmlElement GetXmlRoot()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            return root;
        }

        [TestMethod]
        public void TestSaveLoad_KLinePeriod()
        {
            KLinePeriod period = KLinePeriod.KLinePeriod_1Minute;
            XmlElement root = GetXmlRoot();
            period.Save(root);
            KLinePeriod p2 = new KLinePeriod();
            p2.Load(root);
            Assert.AreEqual(p2, period);
        }

        [TestMethod]
        public void TestSaveLoad_ForwardPeriod()
        {
            ForwardPeriod fp = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            XmlElement root = GetXmlRoot();
            fp.Save(root);
            ForwardPeriod fp2 = new ForwardPeriod();
            fp2.Load(root);
            Assert.AreEqual(fp, fp2);
        }

        [TestMethod]
        public void TestSaveLoad_ReferedPeriod()
        {
            ForwardReferedPeriods rp = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Hour, KLinePeriod.KLinePeriod_1Day }, true, false);
            XmlElement root = GetXmlRoot();
            rp.Save(root);
            ForwardReferedPeriods rp2 = new ForwardReferedPeriods();
            rp2.Load(root);
            Assert.AreEqual(rp, rp2);
        }
    }
}
