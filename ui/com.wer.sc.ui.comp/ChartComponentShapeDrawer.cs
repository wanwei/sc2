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
using System.Xml;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentShapeDrawer : IStrategyDrawer_PriceRect
    {
        //画的title，存在该属性里，在图形刷新的时候重画
        private Shape_Label title;
        //画的图形，图形都存储在该list里，在图形刷新的时候重画
        private List<IPriceShape> priceShapes = new List<IPriceShape>();
        //画图的面板
        private IGraphicDrawer_PriceRect drawer;
        //图画数据
        private IGraphicData graphicData;
        //价格和坐标的映射
        private PriceGraphicMapping mapping;

        public ChartComponentShapeDrawer(IGraphicDrawer_PriceRect drawer, IGraphicData graphicData, PriceGraphicMapping mapping)
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
            for (int i = 0; i < points.Count; i++)
            {
                DrawShape(points[i]);
            }
        }

        public void DrawLabels(IList<PriceShape_Label> label)
        {
            for (int i = 0; i < label.Count; i++)
            {
                DrawShape(label[i]);
            }
        }

        public void DrawLabel(PriceShape_Label label)
        {
            PriceShape_Label shape = new PriceShape_Label();
            shape.Color = label.Color;
            shape.Text = label.Text;
            shape.Point = GetPoint(label.Point.X, label.Point.Y);
            if (shape.Point == null)
                return;
            RecordShape(label);
        }

        public void DrawLine(PriceShape_Line line)
        {
            RecordShape(line);
        }

        public void DrawLine(double startTime, float startPrice, double endTime, float endPrice)
        {

        }

        public void DrawLines(IList<PriceShape_Line> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                RecordShape(lines[i]);
            }
        }

        public void DrawPoint(PriceShape_Point points)
        {
            RecordShape(points);
        }

        public void DrawPoints(IList<float> points, Color color, int width)
        {
            RecordShape(new StrategyPoints(points, color, width));
        }

        public void DrawPoints(IList<float> points, Color color)
        {
            DrawPoints(points, color, 6);
        }

        public void DrawPolyLine(PriceShape_PolyLine polyLine)
        {
            RecordShape(polyLine);
        }

        public void DrawPolyLine(IList<float> line, Color color)
        {
            RecordShape(new StrategyPolyLine(line, color));
        }

        public void DrawRect(PriceShape_Rect priceRect)
        {
            RecordShape(priceRect);
        }

        private void RecordShape(IPriceShape shape)
        {
            this.priceShapes.Add(shape);
            DrawShape(shape);
        }

        private void DrawShape2(StrategyShape shape)
        {
            switch (shape.GetShapeType())
            {
                case PriceShapeType.PolyLine:
                    DrawPolyLineInternal((StrategyPolyLine)shape);
                    break;
                case PriceShapeType.Point:
                    DrawPointsInternal((StrategyPoints)shape);
                    break;                
                //case PriceShapeType.Rect:
                    //DrawRect()
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

        private void DrawShape(IPriceShape shape)
        {
            if (shape is StrategyShape)
                this.DrawShape2((StrategyShape)shape);
            else
            {
                PriceRectangle priceRect = mapping.PriceRect;
                int start = priceRect.StartIndex;
                int end = priceRect.EndIndex;

                if (shape.GetShapeType() == PriceShapeType.Point)
                {
                    PriceShape_Point point = (PriceShape_Point)shape;
                    if (point.X < start || point.X > end)
                        return;
                }
                this.drawer.DrawPriceShape(shape);
            }
        }

        public void Refresh()
        {
            drawer.ClearPriceShapes();
            drawer.ClearShapes();
            if (title != null)
                drawer.DrawShape(title);
            for (int i = 0; i < priceShapes.Count; i++)
            {
                DrawShape(priceShapes[i]);
            }
        }

        public void ClearShapes()
        {
            this.priceShapes.Clear();
            drawer.ClearShapes();
            drawer.ClearPriceShapes();
        }
    }

    interface StrategyShape : IPriceShape
    {
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

        public void Save(XmlElement xmlElem)
        {
        }

        public void Load(XmlElement xmlElem)
        {
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

        public void Save(XmlElement xmlElem)
        {
        }

        public void Load(XmlElement xmlElem)
        {            
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

        public void Save(XmlElement xmlElem)
        {
        }

        public void Load(XmlElement xmlElem)
        {
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

        public void Save(XmlElement xmlElem)
        {            
        }

        public void Load(XmlElement xmlElem)
        {           
        }
    }
}