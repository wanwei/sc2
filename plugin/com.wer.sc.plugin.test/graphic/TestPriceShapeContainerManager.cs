using com.wer.sc.data;
using com.wer.sc.graphic.shape;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic
{
    [TestClass]
    public class TestPriceShapeContainerManager
    {
        [TestMethod]
        public void TestShapeContainerManager()
        {
            PriceShapeContainerManager manager = new PriceShapeContainerManager();

            KLineKey klineKey = new KLineKey("rb1710", 20170105, 20170501, KLinePeriod.KLinePeriod_15Minute);
            PriceShapeContainer container = new PriceShapeContainer_KLine(klineKey);
            TestShapeContainer.GetContainer(container);
            manager.AddContainer(container);

            TimeLineKey timeLineKey = new TimeLineKey("rb1710", 20170107);
            PriceShapeContainer container2 = new PriceShapeContainer_TimeLine(timeLineKey);
            TestShapeContainer.GetContainer(container2);
            manager.AddContainer(container2);

            Console.WriteLine(XmlUtils.ToString(manager));

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            manager.Save(root);

            PriceShapeContainerManager manager2 = new PriceShapeContainerManager();
            manager2.Load(root);
            Assert.AreEqual(1, manager2.GetKLineKeies().Count);
            Assert.AreEqual(1, manager2.GetTimeLineKeies().Count);

            Assert.IsNotNull(manager2.GetShapeContainer(klineKey));
            Assert.IsNotNull(manager2.GetShapeContainer(timeLineKey));

            Assert.AreEqual(XmlUtils.ToString(manager), XmlUtils.ToString(manager2));
        }
    }
}
