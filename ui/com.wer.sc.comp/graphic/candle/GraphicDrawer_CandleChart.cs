using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 画蜡烛图
    /// </summary>
    public class GraphicDrawer_CandleChart : GraphicDrawer_Candle_Abstract
    {
        private PriceGraphicMapping priceMapping = new PriceGraphicMapping();

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                priceMapping.DrawRect = DisplayRect;
                priceMapping.PriceRect = GetPriceRectangle();
                return priceMapping;
            }
        }

        private PriceRectangle GetPriceRectangle()
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            float priceBottom = data.Arr_Low[startIndex];
            float priceTop = data.Arr_High[startIndex];
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (priceBottom > data.Arr_Low[i])
                    priceBottom = data.Arr_Low[i];
                if (priceTop < data.Arr_High[i])
                    priceTop = data.Arr_High[i];
            }
            return new PriceRectangle(startIndex - 0.5f, endIndex + 0.5f, priceTop, priceBottom);
        }

        public GraphicDrawer_CandleChart()
        {
        }

        public override void DrawGraph(Graphics graphic)
        {
            DataProvider.BlockMount = (int)(this.DisplayRect.Width / BlockWidth);
            DrawFrame(graphic);
            DrawCandle(graphic);
            //DrawPolyLine(graphic);
            //DrawPoint(graphic);
        }

        #region 画两根横轴

        public void DrawFrame(Graphics g)
        {
            Rectangle rectangleScale = FrameRect;
            g.DrawRectangle(this.ColorConfig.Pen_FrameLine, rectangleScale);

            float[] prices = findScalePrices();

            double xLeft = rectangleScale.X;
            double xRight = rectangleScale.X + rectangleScale.Width;
            double lowY = PriceMapping.CalcY(prices[0]);
            double highY = PriceMapping.CalcY(prices[1]);

            drawScaleLine(g, xLeft, xRight, lowY, prices[0]);
            drawScaleLine(g, xLeft, xRight, highY, prices[1]);
        }

        private int[] ARR_SCALEPRICE = { 1, 5, 10, 20, 40, 100, 200, 500, 1000, 2000 };

        private float[] findScalePrices()
        {
            double height = PriceMapping.PriceRect.PriceHeight;
            //最小比例是5，然后依次是10、20、40、100、500、1000、2000
            int scalePriceInterval = 0;
            for (int i = 0; i < ARR_SCALEPRICE.Length; i++)
            {
                int interval = ARR_SCALEPRICE[i];
                if (height > interval && height < 3 * interval)
                {
                    scalePriceInterval = interval;
                    break;
                }
                if (height < interval)
                {
                    int index = i - 1;
                    index = index >= 0 ? index : 0;
                    scalePriceInterval = ARR_SCALEPRICE[index];
                    break;
                }
            }
            float lowPrice = ((int)(PriceMapping.PriceRect.PriceBottom / scalePriceInterval) + 1) * scalePriceInterval;

            float[] arr = new float[2];
            arr[0] = lowPrice;
            arr[1] = lowPrice + scalePriceInterval;
            return arr;
        }

        private void drawScaleLine(Graphics g, double xLeft, double xRight, double y, double price)
        {
            ColorConfig config = ColorConfig;
            g.DrawLine(config.Pen_CandleFrameScaleLine, new Point((int)xLeft, (int)y), new Point((int)xRight, (int)y));

            String label = price.ToString();
            g.DrawString(label, config.Font_CandleFrameScaleFont, config.Brush_CandleFrameScaleBrush, new Point((int)xLeft - label.Length * 8 - 5, (int)y - 5));
        }

        #endregion

        #region 画K线

        private void DrawCandle(Graphics g)
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            for (int i = startIndex; i < endIndex; i++)
            {
                DrawCandle(g, new KLineBar_KLineData(data, i), i);
            }
            DrawCandle(g, DataProvider.GetCurrentChart(), endIndex);
        }

        private void DrawCandle(Graphics g, IKLineBar chart, int index)
        {
            bool isRed = chart.End > chart.Start;
            Brush b = isRed ? this.ColorConfig.Brush_CandleBlockUp : this.ColorConfig.Brush_CandleBlockDown;
            if (chart.End.Equals(chart.Start))
                b = this.ColorConfig.Brush_CandleFlat;
            Pen p = new Pen(b);
            double XMiddle = PriceMapping.CalcX(index);
            double YTop = PriceMapping.CalcY(chart.High);
            double YBottom = PriceMapping.CalcY(chart.Low);
            float YBlockTop = (float)PriceMapping.CalcY(isRed ? chart.End : chart.Start);
            float YBlockBottom = (float)PriceMapping.CalcY(isRed ? chart.Start : chart.End);
            //画上影线和下影线
            g.DrawLine(p, new Point((int)XMiddle, (int)YTop), new Point((int)XMiddle, (int)YBlockTop));
            g.DrawLine(p, new Point((int)XMiddle, (int)YBottom), new Point((int)XMiddle, (int)YBlockBottom));

            float halfBlockWidth = (float)(BlockWidth - BlockPadding) / 2;

            float XLeft = (float)(XMiddle - halfBlockWidth);
            float XRight = (float)(XMiddle + halfBlockWidth);
            //画block
            if (chart.End == chart.Start)
                g.DrawLine(p, XLeft, YBlockBottom, XRight, YBlockBottom);
            else if (isRed)
                g.DrawRectangle(p, XLeft, YBlockTop, halfBlockWidth * 2, YBlockBottom - YBlockTop);
            else
                g.FillRectangle(b, XLeft, YBlockTop, halfBlockWidth * 2, YBlockBottom - YBlockTop);
        }

        //public override Point CalcCrossHairPoint(int selectIndex)
        //{
        //    List<ChartInfo> charts = BelongChart.GetChartListInfo().Charts;
        //    if (charts.Count <= selectIndex)
        //        return new Point();
        //    Point p = new Point();
        //    p.X = (int)CalcX(selectIndex);
        //    p.Y = (int)CalcY(BelongChart.GetChartListInfo().Charts[selectIndex].EndPrice);
        //    return p;
        //}

        #endregion

        //#region 画附加图

        //private List<PolyLineArray> polyLines = new List<PolyLineArray>();

        //public void AddPolyLine(PolyLineArray polyLine)
        //{
        //    this.polyLines.Add(polyLine);
        //}
        //public void AddPolyLines(List<PolyLineArray> polyLines)
        //{
        //    this.polyLines.AddRange(polyLines);
        //}

        //private List<PolyLineList> polyLineList = new List<PolyLineList>();

        //public void AddPolyLine(PolyLineList polyLine)
        //{
        //    this.polyLineList.Add(polyLine);
        //}

        //public void AddPolyLines(List<PolyLineList> polyLines)
        //{
        //    this.polyLineList.AddRange(polyLines);
        //}


        //public void ClearPolyLine()
        //{
        //    polyLines.Clear();
        //    polyLineList.Clear();
        //}

        //private void DrawPolyLine(Graphics g)
        //{
        //    for (int i = 0; i < polyLines.Count; i++)
        //    {
        //        DrawPolyLine(g, polyLines[i]);
        //    }
        //    for (int i = 0; i < polyLineList.Count; i++)
        //    {
        //        DrawPolyLine(g, polyLineList[i]);
        //    }
        //}

        //private void DrawPolyLine(Graphics g, PolyLineList line)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    Pen pen = new Pen(line.color, line.Width);
        //    List<PricePoint> data = line.Data;
        //    for (int i = 1; i < data.Count; i++)
        //    {
        //        PricePoint lastpoint = data[i - 1];
        //        PricePoint point = data[i];
        //        if (lastpoint.X >= DataProvider.StartIndex && point.X <= DataProvider.EndIndex)
        //        {

        //            float x1 = PriceMapping.CalcX(lastpoint.X);
        //            float y1 = PriceMapping.CalcY(lastpoint.Y);
        //            float x2 = PriceMapping.CalcX(point.X);
        //            float y2 = PriceMapping.CalcY(point.Y);
        //            g.DrawLine(pen, x1, y1, x2, y2);
        //        }
        //    }
        //}

        //private void DrawPolyLine(Graphics g, PolyLineArray line)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    Pen pen = new Pen(line.color, line.Width);
        //    float[] data = line.Data;
        //    endIndex = endIndex >= data.Length ? data.Length - 1 : endIndex;
        //    for (int i = startIndex + 1; i <= endIndex; i++)
        //    {
        //        float x1 = PriceMapping.CalcX(i - 1);
        //        float y1 = PriceMapping.CalcY(data[i - 1]);
        //        float x2 = PriceMapping.CalcX(i);
        //        float y2 = PriceMapping.CalcY(data[i]);
        //        g.DrawLine(pen, x1, y1, x2, y2);
        //    }
        //}

        //private List<PointArray> points = new List<PointArray>();

        //public void AddPoint(PointArray polyLine)
        //{
        //    points.Add(polyLine);
        //}

        //public void AddPoints(List<PointArray> polyLine)
        //{
        //    points.AddRange(polyLine);
        //}

        //private List<PointList> pointLists = new List<PointList>();

        //public void AddPoint(PointList polyLine)
        //{
        //    pointLists.Add(polyLine);
        //}

        //public void AddPoints(List<PointList> polyLine)
        //{
        //    pointLists.AddRange(polyLine);
        //}

        //public void ClearPoints()
        //{
        //    points.Clear();
        //    pointLists.Clear();
        //}

        //private void DrawPoint(Graphics g)
        //{
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        DrawPoint(g, points[i]);
        //    }
        //    for (int i = 0; i < pointLists.Count; i++)
        //    {
        //        DrawPoint(g, pointLists[i]);
        //    }
        //}

        //private void DrawPoint(Graphics g, PointArray points)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    float[] data = points.Data;
        //    endIndex = endIndex >= data.Length ? data.Length - 1 : endIndex;

        //    Brush b = new SolidBrush(points.color);
        //    float w = points.Width / 2;
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        if (data[i] <= 0)
        //            continue;
        //        float x1 = PriceMapping.CalcX(i);
        //        float y1 = PriceMapping.CalcY(data[i]);
        //        g.FillEllipse(b, x1 - w, y1 - w, points.Width, points.Width);
        //    }
        //}

        //private void DrawPoint(Graphics g, PointList points)
        //{
        //    List<PricePoint> data = points.Data;
        //    Brush b = new SolidBrush(points.color);
        //    float w = points.Width / 2;
        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        PricePoint point = data[i];
        //        if (point.X >= DataProvider.StartIndex && point.X <= DataProvider.EndIndex)
        //        {
        //            float x1 = PriceMapping.CalcX(point.X);
        //            float y1 = PriceMapping.CalcY(point.Y);
        //            g.FillEllipse(b, x1 - w, y1 - w, points.Width, points.Width);
        //        }
        //    }
        //}

        //#endregion

        #region 画ma

        private String textColor = "#FFFF00";

        private String[] colors = new String[4] { "#E7E7E7", "#FFFF00", "#DB00B6", "#00F000" };

        private void DrawText(Graphics g, int[] periodArr, double[] currentValues)
        {
            int x = DisplayRect.X;
            int y = DisplayRect.Y;

            Font font = new Font("宋体", 10, FontStyle.Regular);

            StringBuilder sb = new StringBuilder();
            sb.Append("MA组合(").Append(periodArr[0]);
            for (int i = 1; i < periodArr.Length; i++)
            {
                sb.Append(",").Append(periodArr[i]);
            }
            sb.Append(")");
            g.DrawString(sb.ToString(), font, new SolidBrush(ColorUtils.GetColor(textColor)), new Point(x, y));

            x += sb.ToString().Length * 8 + 10;
            for (int i = 0; i < periodArr.Length; i++)
            {
                String str = "MA" + periodArr[i] + " " + currentValues[i].ToString("0.00");
                g.DrawString(str, font, new SolidBrush(ColorUtils.GetColor(colors[i])), new Point(x, y));
                x += str.Length * 8 + 10;
            }
        }

        #endregion
    }

    public class BlockCountCalculator
    {
        private float widthEveryBlock;

        private float rectWidth;

        private int blockCount;

        public float WidthEveryBlock
        {
            get
            {
                return widthEveryBlock;
            }

            set
            {
                widthEveryBlock = value;
                blockCount = (int)(rectWidth / widthEveryBlock);
            }
        }

        public float RectWidth
        {
            get
            {
                return rectWidth;
            }

            set
            {
                rectWidth = value;
                blockCount = (int)(rectWidth / widthEveryBlock);
            }
        }

        public int BlockCount
        {
            get
            {
                return blockCount;
            }
        }
    }
}