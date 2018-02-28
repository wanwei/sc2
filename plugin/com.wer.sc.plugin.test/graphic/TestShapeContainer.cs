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

namespace com.wer.sc.graphic
{
    [TestClass]
    public class TestShapeContainer
    {
        [TestMethod]
        public void TestShapeSaveLoad()
        {
            PriceShapeContainer container = new PriceShapeContainer();
            GetContainer(container);
            Console.WriteLine(XmlUtils.ToString(container));

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            container.Save(root);

            PriceShapeContainer container2 = new PriceShapeContainer();
            container2.Load(root);
            Console.WriteLine(XmlUtils.ToString(container2));

            Assert.AreEqual(XmlUtils.ToString(container), XmlUtils.ToString(container2));
        }

        public static PriceShapeContainer GetContainer(PriceShapeContainer container)
        {            
            container.AddPriceShape(GetLabel());
            container.AddPriceShape(GetLine());
            container.AddPriceShape(GetPolyLine());
            container.AddPriceShape(GetPoint());
            container.AddPriceShape(GetRect());
            return container;
        }

        private static PriceShape_Label GetLabel()
        {
            PriceShape_Label label = new PriceShape_Label();
            label.Color = Color.Red;
            label.Point = new PricePoint(12, 12);
            label.Text = "test";
            return label;
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

        private static PriceShape_Point GetPoint()
        {
            PriceShape_Point point = new PriceShape_Point(2344, 50);
            point.Color = ColorUtils.GetColor("#84f910"); //Color.Gray;
            point.Width = 20;
            return point;
        }

        private static PriceShape_PolyLine GetPolyLine()
        {
            PriceShape_PolyLine polyLine = new PriceShape_PolyLine();
            polyLine.Width = 2;
            polyLine.Color = Color.White;
            polyLine.AddPoint(new PricePoint(1, 3));
            polyLine.AddPoint(new PricePoint(2, 31));
            polyLine.AddPoint(new PricePoint(3, 55));
            polyLine.AddPoint(new PricePoint(4, 13));
            polyLine.AddPoint(new PricePoint(5, 34));
            polyLine.AddPoint(new PricePoint(6, 41));
            return polyLine;
        }

        private static PriceShape_Rect GetRect()
        {
            PriceShape_Rect rect = new PriceShape_Rect();
            rect.Color = Color.Blue;
            rect.FillRect = true;
            rect.PriceLeft = 123;
            rect.PriceTop = 33;
            rect.PriceRight = 444;
            rect.PriceBottom = 11;
            return rect;
        }
    }
}
