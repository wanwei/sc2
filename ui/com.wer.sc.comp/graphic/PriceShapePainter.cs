using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PriceShapePainter : IShapePainter
    {
        private ShapePainter_Point shapePainter_Point = new ShapePainter_Point();
        private ShapePainter_Line shapePainter_Line = new ShapePainter_Line();
        private ShapePainter_PolyLine shapePainter_PolyLine = new ShapePainter_PolyLine();

        public void Paint(Graphics g, PriceGraphicMapping priceGraphic, PriceShape shape)
        {
            switch (shape.GetShapeType())
            {
                case PriceShapeType.Point:
                    shapePainter_Point.Paint(g, priceGraphic, shape);
                    break;
                case PriceShapeType.Line:
                    shapePainter_Line.Paint(g, priceGraphic, shape);
                    break;
                case PriceShapeType.PolyLine:
                    shapePainter_PolyLine.Paint(g, priceGraphic, shape);
                    break;
            }
        }
    }

    public interface IShapePainter
    {
        void Paint(Graphics g, PriceGraphicMapping priceGraphic, PriceShape shape);
    }

    class ShapePainter_Point : IShapePainter
    {
        public void Paint(Graphics g, PriceGraphicMapping priceGraphic, PriceShape shape)
        {
            PriceShape_Point point = (PriceShape_Point)shape;
            float x1 = priceGraphic.CalcX(point.X);
            float y1 = priceGraphic.CalcY(point.Y);
            float w = point.Width;
            g.FillEllipse(new SolidBrush(point.Color), x1 - point.Width, y1 - point.Width, point.Width, point.Width);
        }
    }

    class ShapePainter_Line : IShapePainter
    {
        public void Paint(Graphics g, PriceGraphicMapping priceGraphic, PriceShape shape)
        {
            PriceShape_Line line = (PriceShape_Line)shape;
            float x1 = priceGraphic.CalcX(line.StartPoint.X);
            float y1 = priceGraphic.CalcY(line.StartPoint.Y);

            float x2 = priceGraphic.CalcX(line.EndPoint.X);
            float y2 = priceGraphic.CalcY(line.EndPoint.Y);

            g.DrawLine(new Pen(line.Color, line.Width), x1, y1, x2, y2);
        }
    }

    class ShapePainter_PolyLine : IShapePainter
    {
        public void Paint(Graphics g, PriceGraphicMapping priceGraphic, PriceShape shape)
        {
            PriceShape_PolyLine line = (PriceShape_PolyLine)shape;
            PointF[] points = new PointF[line.Points.Count];
            for (int i = 0; i < line.Points.Count; i++)
            {
                points[i] = GetPointF(priceGraphic, line.Points[i]);
            }
            g.DrawLines(new Pen(line.Color, line.Width), points);
        }

        private PointF GetPointF(PriceGraphicMapping priceGraphic, PriceShape_Point pricePoint)
        {
            float x1 = priceGraphic.CalcX(pricePoint.X);
            float y1 = priceGraphic.CalcY(pricePoint.Y);
            return new PointF(x1, y1);
        }
    }
}
