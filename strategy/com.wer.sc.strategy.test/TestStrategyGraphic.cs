using com.wer.sc.data;
using com.wer.sc.graphic;
using com.wer.sc.graphic.shape;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyGraphic
    {
        [TestMethod]
        public void TestStrategyGraphicSaveLoad()
        {            
            StrategyGraphic graphic = GetGraphic();

            Console.WriteLine(XmlUtils.ToString(graphic));

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            graphic.Save(root);

            StrategyGraphic graphic2 = new StrategyGraphic();
            graphic2.Load(root);
            Console.WriteLine(XmlUtils.ToString(graphic2));

            Assert.AreEqual(XmlUtils.ToString(graphic), XmlUtils.ToString(graphic2));
        }

        public static StrategyGraphic GetGraphic()
        {
            KLineKey dataKey = new KLineKey("rb1801", 20170801, 20170901, KLinePeriod.KLinePeriod_15Minute);
            StrategyGraphic graphic = new StrategyGraphic(dataKey);
            graphic.Title.X = 10;
            graphic.Title.Text = "test";
            graphic.Title.Color = Color.Red;
            graphic.Shapes.AddPriceShape(GetLine());
            return graphic;
        }

        private static PriceShape_Line GetLine()
        {
            PriceShape_Line line = new PriceShape_Line();
            line.StartPoint = new PricePoint(33, 12);
            line.EndPoint = new PricePoint(213, 243);
            line.Width = 3;
            line.Color = Color.Blue;
            return line;
        }
    }
}
