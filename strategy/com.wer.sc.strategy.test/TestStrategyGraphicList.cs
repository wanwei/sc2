using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyGraphicList
    {
        [TestMethod]
        public void TestStrategyGraphicListSaveLoad()
        {
            StrategyGraphicList graphics = new StrategyGraphicList();
            StrategyGraphic graphic = TestStrategyGraphic.GetGraphic();
            graphics.AddGraphic(graphic);

            Console.WriteLine(XmlUtils.ToString(graphics));

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            graphics.Save(root);

            StrategyGraphicList graphics2 = new StrategyGraphicList();
            graphics2.Load(root);
            Console.WriteLine(XmlUtils.ToString(graphics2));

            Assert.AreEqual(XmlUtils.ToString(graphics), XmlUtils.ToString(graphics2));

        }
    }
}
