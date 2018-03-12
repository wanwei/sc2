using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.graphic.shape;

namespace com.wer.sc.strategy
{
    public class StrategyDrawer_PriceRect_Empty : IStrategyDrawer_PriceRect
    {
        public void ClearShapes()
        {
            
        }

        public void DrawLabel(PriceShape_Label label)
        {
            
        }

        public void DrawLabels(IList<PriceShape_Label> label)
        {
            
        }

        public void DrawLine(PriceShape_Line line)
        {
            
        }

        public void DrawLine(double startTime, float startPrice, double endTime, float endPrice)
        {
            
        }

        public void DrawLines(IList<PriceShape_Line> lines)
        {
            
        }

        public void DrawPoint(PriceShape_Point points)
        {
            
        }

        public void DrawPoints(IList<PriceShape_Point> points)
        {
            
        }

        public void DrawPoints(IList<float> points, Color color)
        {
            
        }

        public void DrawPoints(IList<float> points, Color color, int width)
        {
            
        }

        public void DrawPolyLine(PriceShape_PolyLine polyLine)
        {
            
        }

        public void DrawPolyLine(IList<float> points, Color color)
        {
            
        }

        public void DrawRect(PriceShape_Rect priceRect)
        {
            
        }

        public void DrawTitle(int x, string text, Color color)
        {
            
        }

        public void DrawTitle(int x, string text, Color color, Font font)
        {
            
        }

        public void Load(XmlElement xmlElem)
        {
        }

        public void Refresh()
        {
            
        }

        public void Save(XmlElement xmlElem)
        {
        }
    }
}