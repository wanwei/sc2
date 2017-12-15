using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.strategy.draw;
using System.Drawing;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.comp.graphic.shape;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 策略的画图器
    /// 该画图器注意点：
    /// draw的时候不会
    /// </summary>
    public class StrategyDrawer : IStrategyDrawer
    {
        private int startPos;

        private IGraphicDrawer_PriceRect drawer;

        private IGraphicData graphicData;

        private PriceGraphicMapping mapping;

        private List<StrategyShape> drawStrategyShapes = new List<StrategyShape>();

        private List<PriceShape> drawPriceShapes = new List<PriceShape>();

        public StrategyDrawer(IGraphicDrawer_PriceRect drawer, IGraphicData graphicData, PriceGraphicMapping mapping)
        {
            this.drawer = drawer;
            this.graphicData = graphicData;
            this.mapping = mapping;
        }

        public StrategyDrawer(IGraphicDrawer_PriceRect drawer, IGraphicData graphicData, PriceGraphicMapping mapping, int startPos)
        {
            this.drawer = drawer;
            this.graphicData = graphicData;
            this.mapping = mapping;
            this.startPos = startPos;
        }

        private PriceShape_Point GetPoint(double time, float price)
        {
            if (graphicData is IGraphicData_Candle)
            {
                IGraphicData_Candle candleData = (IGraphicData_Candle)graphicData;
                IKLineData klineData = candleData.GetKLineData();
                int index = TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
                return new PriceShape_Point(index, price);
            }
            else if (graphicData is IGraphicData_TimeLine)
            {

            }
            return null;
        }

        public void DrawTitle(int x, string text, Color color)
        {
            Shape_Label label = new Shape_Label();
            label.X = x;
            label.Y = -20;
            label.Text = text;
            label.Color = color;
            label.Font = new Font("宋体", 10f, FontStyle.Regular);
            drawer.DrawShape(label);
        }

        public void DrawLabel(PriceLabel label)
        {
            PriceShape_Label shape = new PriceShape_Label();
            shape.Color = label.Color;
            shape.Text = label.Text;
            shape.Point = GetPoint(label.Time, label.Price);
            if (shape.Point == null)
                return;
            drawer.DrawPriceShape(shape);
        }

        public void DrawLabels(List<float> positions, List<string> txts, Color color)
        {
            PriceShape_Label shape = new PriceShape_Label();

            //shape.Color = label.Color;
            //shape.Text = label.Text;
            //shape.Point = GetPoint(label.Time, label.Price);
            if (shape.Point == null)
                return;
            drawer.DrawPriceShape(shape);
        }

        public void DrawLine(PriceLine line)
        {

        }

        public void DrawLine(double startTime, float startPrice, double endTime, float endPrice)
        {

        }

        public void DrawPoint(PricePoint points)
        {

        }
        public void DrawPoints(List<float> points, Color color, int width)
        {
            RecordStrategyShape(new StrategyPoints(points, color, width));
        }

        public void DrawPoints(List<float> points, Color color)
        {
            DrawPoints(points, color, 6);
        }

        public void DrawPolyLine(PricePolyLine polyLine)
        {
            PriceRectangle priceRect = mapping.PriceRect;
            int start = priceRect.StartIndex;
            int end = priceRect.EndIndex;

            for (int i = 0; i < polyLine.Points.Count; i++)
            {
                PricePoint point = polyLine.Points[i];

            }
        }


        public void DrawPolyLine(List<float> line, Color color)
        {
            RecordStrategyShape(new StrategyPolyLine(line, color));
        }

        private void RecordStrategyShape(StrategyShape shape)
        {
            Record(shape);
            DrawShape(shape);
        }

        private void DrawShape(StrategyShape shape)
        {
            switch (shape.GetShapeType())
            {
                case PriceShapeType.PolyLine:
                    DrawPolyLineInternal((StrategyPolyLine)shape);
                    break;
                case PriceShapeType.Point:
                    DrawPointsInternal((StrategyPoints)shape);
                    break;
            }
        }

        private void DrawPointsInternal(StrategyPoints points)
        {
            PriceRectangle priceRect = mapping.PriceRect;
            int start = priceRect.StartIndex;
            int end = priceRect.EndIndex;

            List<float> prices = points.Points;
            for (int i = start; i <= end; i++)
            {
                if (i >= prices.Count)
                    return;
                if (prices[i] == float.MinValue)
                    continue;
                PriceShape_Point point = new PriceShape_Point(i, prices[i]);
                point.Width = points.Width;
                point.Color = points.Color;
                DrawShape(point);
            }
        }

        private void DrawPolyLineInternal(StrategyPolyLine polyLine)
        {
            List<float> line = polyLine.Prices;
            Color color = polyLine.Color;
            PriceRectangle priceRect = mapping.PriceRect;
            int start = priceRect.StartIndex;// - startPos + 1;
            int end = priceRect.EndIndex + 1;// - startPos + 1;
            PriceShape_PolyLine polyline = new PriceShape_PolyLine();
            for (int i = start; i <= end; i++)
            {
                if (i >= line.Count)
                    return;
                if (i < startPos)
                    continue;
                float price = line[i - startPos];
                if (price == float.MinValue)
                    continue;
                PriceShape_Point point = new PriceShape_Point(i, price);
                polyline.AddPoint(point);
            }
            polyline.Color = color;
            DrawShape(polyline);
        }

        private void DrawShape(PriceShape shape)
        {
            this.drawer.DrawPriceShape(shape);
        }

        public void Refresh()
        {
            drawer.ClearPriceShapes();
            for (int i = 0; i < shapes.Count; i++)
            {
                DrawShape(shapes[i]);
            }
        }

        private List<StrategyShape> shapes = new List<StrategyShape>();
        private void Record(StrategyShape shape)
        {
            this.shapes.Add(shape);
        }

        public void ClearShapes()
        {
            this.shapes.Clear();
            drawer.ClearShapes();
            drawer.ClearPriceShapes();
        }
    }

    interface StrategyShape
    {
        PriceShapeType GetShapeType();
    }

    class StrategyPolyLine : StrategyShape
    {
        public List<float> Prices;

        public Color Color;

        public StrategyPolyLine(List<float> prices, Color color)
        {
            this.Prices = prices;
            this.Color = color;
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.PolyLine;
        }
    }

    class StrategyPoints : StrategyShape
    {
        public List<float> Points;

        public Color Color;

        public int Width;

        public StrategyPoints(List<float> points, Color color, int width)
        {
            this.Points = points;
            this.Color = color;
            this.Width = width;
        }
        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Point;
        }
    }

    class StrategyLabels : StrategyShape
    {
        public List<float> Positions;
        public List<string> Txts;
        public Color Color;

        public StrategyLabels(List<float> positions, List<String> txts, Color color)
        {
            this.Positions = positions;
            this.Txts = txts;
            this.Color = color;
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Label;
        }
    }

    class StrategyLine : StrategyShape
    {
        public double StartTime;
        public float StartPrice;
        public double EndTime;
        public float EndPrice;

        public StrategyLine(double startTime, float startPrice, double endTime, float endPrice)
        {
            this.StartTime = startTime;
            this.StartPrice = startPrice;
            this.EndTime = endTime;
            this.EndPrice = endPrice;
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Line;
        }
    }

    //class StrategyRect : StrategyShape
    //{
    //    double startTime;
    //    float startPrice;
    //    double endTime;
    //    float endPrice;

    //    public PriceShapeType GetShapeType()
    //    {
    //        return PriceShapeType.Line;
    //    }
    //}
}