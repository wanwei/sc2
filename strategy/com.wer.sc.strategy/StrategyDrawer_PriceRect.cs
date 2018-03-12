using com.wer.sc.graphic;
using com.wer.sc.graphic.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyDrawer_PriceRect : IStrategyDrawer_PriceRect
    {
        private int startBarPos;

        private StrategyGraphic strategyGraphic;

        public StrategyDrawer_PriceRect()
        {

        }

        public StrategyDrawer_PriceRect(StrategyGraphic strategyGraphic, int startBarPos)
        {
            this.strategyGraphic = strategyGraphic;
            this.startBarPos = startBarPos;
        }

        /// <summary>
        /// 写该图的title，写在该图的左上角
        /// 如：MA指标
        /// </summary>
        /// <param name="x">绘制文本的x坐标</param>
        /// <param name="text">绘制的文本</param>
        /// <param name="color">使用的颜色</param>
        public void DrawTitle(int x, string text, Color color)
        {
            StrategyGraphicTitle title = (StrategyGraphicTitle)strategyGraphic.Title;
            title.X = x;
            title.Text = text;
            title.Color = color;
        }

        public void DrawTitle(int x, string text, Color color, Font font)
        {
            StrategyGraphicTitle title = (StrategyGraphicTitle)strategyGraphic.Title;
            title.X = x;
            title.Text = text;
            title.Color = color;
            title.Font = font;
        }

        /// <summary>
        /// 画折线
        /// 该方法会在每一个bar绘制折线
        /// 适用于一些指标的计算，如MA
        /// 如果points里面数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        public void DrawPolyLine(IList<float> points, Color color)
        {
            PriceShape_PolyLineLink polyLine = new PriceShape_PolyLineLink(startBarPos, points);
            this.strategyGraphic.Shapes.AddPriceShape(polyLine);
        }

        /// <summary>
        /// 画折线
        /// 该方法在指定点画折线，适用于在图形上画直线
        /// 如Zigzag、通道线、趋势线等。
        /// </summary>
        /// <param name="polyLine"></param>
        /// <param name="color"></param>
        public void DrawPolyLine(PriceShape_PolyLine polyLine)
        {
            this.strategyGraphic.Shapes.AddPriceShape(polyLine);
        }

        /// <summary>
        /// 画点，在图形的每一个bar上画点
        /// 如果points里的数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public void DrawPoints(IList<float> points, Color color)
        {
            PriceShape_PointLink pointLink = new PriceShape_PointLink(startBarPos, points);
            this.strategyGraphic.Shapes.AddPriceShape(pointLink);
        }

        /// <summary>
        /// 画指定点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public void DrawPoint(PriceShape_Point points)
        {
            this.strategyGraphic.Shapes.AddPriceShape(points);
        }

        /// <summary>
        /// 画点，在图形的每一个bar上画点
        /// 如果points里的数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public void DrawPoints(IList<float> points, Color color, int width)
        {
            PriceShape_PointLink pointLink = new PriceShape_PointLink(startBarPos, points);
            pointLink.Width = width;
            this.strategyGraphic.Shapes.AddPriceShape(pointLink);
        }

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="points"></param>
        public void DrawPoints(IList<PriceShape_Point> points)
        {
            foreach (PriceShape_Point point in points)
                this.strategyGraphic.Shapes.AddPriceShape(point);
        }

        /// <summary>
        /// 写文字
        /// </summary>
        /// <param name="label"></param>
        public void DrawLabel(PriceShape_Label label)
        {
            this.strategyGraphic.Shapes.AddPriceShape(label);
        }

        /// <summary>
        /// 写文字
        /// </summary>
        /// <param name="label"></param>
        public void DrawLabels(IList<PriceShape_Label> labels)
        {
            foreach (PriceShape_Label label in labels)
                this.strategyGraphic.Shapes.AddPriceShape(label);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="startPrice"></param>
        /// <param name="endTime"></param>
        /// <param name="endPrice"></param>
        public void DrawLine(double startTime, float startPrice, double endTime, float endPrice)
        {

        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="line"></param>
        public void DrawLine(PriceShape_Line line)
        {
            this.strategyGraphic.Shapes.AddPriceShape(line);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="line"></param>
        public void DrawLines(IList<PriceShape_Line> lines)
        {
            foreach (PriceShape_Line line in lines)
                this.strategyGraphic.Shapes.AddPriceShape(line);
        }

        /// <summary>
        /// 画
        /// </summary>
        /// <param name="priceRect"></param>
        public void DrawRect(PriceShape_Rect priceRect)
        {
            this.strategyGraphic.Shapes.AddPriceShape(priceRect);
        }

        /// <summary>
        /// 刷新图像
        /// </summary>
        public void Refresh()
        {

        }

        /// <summary>
        /// 清空所有图形
        /// </summary>
        public void ClearShapes()
        {
            this.strategyGraphic.Title.Text = "";
            this.strategyGraphic.Shapes.Clear();
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("startBarPos", startBarPos.ToString());
            this.strategyGraphic.Save(xmlElem);
        }

        public void Load(XmlElement xmlElem)
        {
            this.startBarPos = int.Parse(xmlElem.GetAttribute("startBarPos"));
            strategyGraphic = new StrategyGraphic();
            strategyGraphic.Load(xmlElem);
        }
    }
}