using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.info
{
    public class GraphicDrawer_CurrentInfo : GraphicDrawer_Abstract
    {
        private String fontStr = "宋体";
        private IGraphicChartRight dataProvider;

        public IGraphicChartRight DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
            }
        }

        public override void DrawGraph(Graphics g)
        {            
            if (dataProvider == null)
                return;
            Rectangle rect = DisplayRect;
            drawFrame(g);

            int x1 = rect.X;
            int x2 = rect.X + rect.Width / 2;
            int y = rect.Y;
            
            DetailInfo chartinfo = dataProvider.GetCurrentInfo();
            double lastJs = chartinfo.lastJsPrice;

            Point point = new Point(x1, y);
            g.DrawString(chartinfo.code, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

            ITickData tickData = dataProvider.GetCurrentTickData();
            //currentInfo.GetChart(ChartPeriod.DAY, 1);
            ITickBar tickChart = tickData.GetBar(dataProvider.CurrentTickIndex);
            drawBuySell(g, x1, point, tickChart, lastJs);

            int startY = rect.Y + 90;
            point.X = x1;
            point.Y = startY;
            int everyheight = 21;
            drawLine_Half(g, "最新", chartinfo.currentPrice.ToString(), GetPriceColor(chartinfo.currentPrice, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "现手", chartinfo.currentHand.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "总手", chartinfo.totalHand.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "持仓", chartinfo.totalHold.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "日增", chartinfo.dailyAdd.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "外盘", chartinfo.outMount.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "比例", StringUtils.GetPercentRound(chartinfo.outPercent), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "内盘", chartinfo.inMount.ToString(), ColorConfig.Color_StockInfo, point);
            point.Y += everyheight;
            drawLine_Half(g, "比例", StringUtils.GetPercentRound(chartinfo.inPercent), ColorConfig.Color_StockInfo, point);

            point.X = x2;
            point.Y = startY;
            drawLine_Half(g, "涨跌", chartinfo.GetUpStr(), GetPriceColor(chartinfo.currentPrice, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "涨速", chartinfo.GetUpSpeedStr(), GetPriceColor(chartinfo.upSpeed, 0), point);
            point.Y += everyheight;
            drawLine_Half(g, "开盘", chartinfo.open.ToString(), GetPriceColor(chartinfo.open, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "最高", chartinfo.high.ToString(), GetPriceColor(chartinfo.high, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "最低", chartinfo.low.ToString(), GetPriceColor(chartinfo.low, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "结算价", chartinfo.jsPrice.ToString(), GetPriceColor(chartinfo.jsPrice, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "昨结", chartinfo.lastJsPrice.ToString(), GetPriceColor(chartinfo.lastJsPrice, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "涨停", chartinfo.maxUp.ToString(), GetPriceColor(chartinfo.maxUp, lastJs), point);
            point.Y += everyheight;
            drawLine_Half(g, "跌停", chartinfo.maxDown.ToString(), GetPriceColor(chartinfo.maxDown, lastJs), point);
        }

        private void drawFrame(Graphics g)
        {
            Rectangle rect = DisplayRect;
            Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width - 2, rect.Height - 2);
            Pen pen = ColorConfig.Pen_FrameLine;
            g.DrawRectangle(pen, r);

            int y = rect.Y + 30;
            g.DrawLine(pen, new Point(rect.X, y), new Point(rect.Right - 2, y));

            y += 28;
            g.DrawLine(pen, new Point(rect.X, y), new Point(rect.Right - 2, y));

            y += 28;
            g.DrawLine(pen, new Point(rect.X, y), new Point(rect.Right - 2, y));

            int y1 = y;
            int y2 = y + 196;
            int x2 = rect.X + rect.Width / 2;
            g.DrawLine(pen, new Point(rect.X, y2), new Point(rect.Right - 2, y2));
            g.DrawLine(pen, new Point(x2, y1), new Point(x2, y2));
        }

        private Point drawBuySell(Graphics g, int x1, Point point, ITickBar tick, double lastJs)
        {
            int fontSize2 = 16;
            point.Y += 32;
            point.Y += 2;
            g.DrawString("卖出", new Font(fontStr, 12, FontStyle.Regular), new SolidBrush(ColorConfig.Color_White), point);
            point.X += 70;
            point.Y -= 2;
            g.DrawString(Math.Round(tick.SellPrice, 2).ToString(), new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(GetPriceColor(tick.SellPrice, lastJs)), point);
            point.X += 80;
            g.DrawString(tick.SellMount.ToString(), new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

            point.X = x1;
            point.Y += 28;
            point.Y += 2;
            g.DrawString("买入", new Font(fontStr, 12, FontStyle.Regular), new SolidBrush(ColorConfig.Color_White), point);
            point.X += 70;
            point.Y -= 2;
            g.DrawString(Math.Round(tick.BuyPrice, 2).ToString(), new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(GetPriceColor(tick.SellPrice, lastJs)), point);
            point.X += 80;
            g.DrawString(tick.BuyMount.ToString(), new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

            return point;
        }

        private Color GetPriceColor(double price, double lastJs)
        {
            if (price == lastJs)
                return ColorConfig.Color_White;
            else if (price > lastJs)
                return ColorConfig.Color_TextUp;
            else
                return ColorConfig.Color_TextDown;
        }

        private void drawLine_Half(Graphics g, String title, String value, Color color, Point point)
        {
            int fontSize3 = 11;
            g.DrawString(title, new Font("宋体", fontSize3, FontStyle.Regular), new SolidBrush(ColorConfig.Color_White), point);

            point.X += (int)(110 - value.Length * 7.5);
            g.DrawString(value, new Font("宋体", fontSize3, FontStyle.Bold), new SolidBrush(color), point);
        }
    }
}
