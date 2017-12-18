using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using com.wer.sc.graphic;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.graphic.shape;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentStrategyDrawer : IStrategyDrawer
    {
        //画的title，存在该属性里，在图形刷新的时候重画
        private Shape_Label title;
        //画的图形，图形都存储在该list里，在图形刷新的时候重画
        private List<StrategyShape> strategyShapes = new List<StrategyShape>();

        //画图的面板
        private IGraphicDrawer_PriceRect drawer;
        //图画数据
        private IGraphicData graphicData;
        //价格和坐标的映射
        private PriceGraphicMapping mapping;

        public ChartComponentStrategyDrawer(IGraphicDrawer_PriceRect drawer, IGraphicData graphicData, PriceGraphicMapping mapping)
        {
            this.drawer = drawer;
            this.graphicData = graphicData;
            this.mapping = mapping;
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
            this.title = label;
        }

        public void DrawPoints(IList<PriceShape_Point> points)
        {

        }

        public void DrawLabels(IList<PriceShape_Label> label)
        {

        }

        public void DrawLines(IList<PriceShape_Line> lines)
        {

        }

        public void DrawLabel(PriceShape_Label label)
        {
            PriceShape_Label shape = new PriceShape_Label();
            shape.Color = label.Color;
            shape.Text = label.Text;
            shape.Point = GetPoint(label.Point.X, label.Point.Y);
            if (shape.Point == null)
                return;
            drawer.DrawPriceShape(shape);
        }

        public void DrawLabels(IList<float> positions, IList<string> txts, Color color)
        {
            PriceShape_Label shape = new PriceShape_Label();

            //shape.Color = label.Color;
            //shape.Text = label.Text;
            //shape.Point = GetPoint(label.Time, label.Price);
            if (shape.Point == null)
                return;
            drawer.DrawPriceShape(shape);
        }

        public void DrawLine(PriceShape_Line line)
        {

        }

        public void DrawLine(double startTime, float startPrice, double endTime, float endPrice)
        {

        }

        public void DrawPoint(PriceShape_Point points)
        {

        }

        public void DrawPoints(IList<float> points, Color color, int width)
        {
            RecordStrategyShape(new StrategyPoints(points, color, width));
        }

        public void DrawPoints(IList<float> points, Color color)
        {
            DrawPoints(points, color, 6);
        }

        public void DrawPolyLine(PriceShape_PolyLine polyLine)
        {
            PriceRectangle priceRect = mapping.PriceRect;
            int start = priceRect.StartIndex;
            int end = priceRect.EndIndex;

            for (int i = 0; i < polyLine.Points.Count; i++)
            {
                PriceShape_Point point = polyLine.Points[i];

            }
        }

        public void DrawPolyLine(IList<float> line, Color color)
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

            IList<float> prices = points.Points;
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
            IList<float> line = polyLine.Prices;
            Color color = polyLine.Color;
            PriceRectangle priceRect = mapping.PriceRect;
            int start = priceRect.StartIndex;
            int end = priceRect.EndIndex + 1;
            PriceShape_PolyLine polyline = new PriceShape_PolyLine();
            for (int index = start; index <= end; index++)
            {
                if (index >= line.Count)
                    return;
                if (index < 0)
                    continue;
                float price = line[index];
                if (price == float.MinValue)
                    continue;
                PriceShape_Point point = new PriceShape_Point(index, price);
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
            if (title != null)
                drawer.DrawShape(title);
            for (int i = 0; i < strategyShapes.Count; i++)
            {
                DrawShape(strategyShapes[i]);
            }
        }

        private void Record(StrategyShape shape)
        {
            this.strategyShapes.Add(shape);
        }

        public void ClearShapes()
        {
            this.strategyShapes.Clear();
            drawer.ClearShapes();
            drawer.ClearPriceShapes();
        }

        public void DrawRect(PriceShape_Rect priceRect)
        {
            
        }
    }

    interface StrategyShape
    {
        PriceShapeType GetShapeType();
    }

    class StrategyPolyLine : StrategyShape
    {
        public IList<float> Prices;

        public Color Color;

        public StrategyPolyLine(IList<float> prices, Color color)
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
        public IList<float> Points;

        public Color Color;

        public int Width;

        public StrategyPoints(IList<float> points, Color color, int width)
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
}