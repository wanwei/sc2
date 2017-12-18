using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.info
{
    public class GraphicDrawer_CurrentInfo : GraphicDrawer_Abstract
    {

        private String fontStr = "宋体";
        private IGraphicData_CurrentInfo dataProvider;

        public IGraphicData_CurrentInfo DataProvider
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

        public GraphicDrawer_CurrentInfo()
        {

        }

        public override void Paint(Graphics g)
        {
            if (dataProvider == null)
                return;
            Rectangle rect = DisplayRect;
            drawFrame(g);

            int x_left = rect.X;
            int y = rect.Y;

            CurrentInfo chartinfo = dataProvider.GetCurrentInfo();
            double lastEndPrice = chartinfo.lastJsPrice;

            Point point = new Point(x_left, y);
            g.DrawString(chartinfo.code, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

            ITickData tickData = dataProvider.GetCurrentTickData();
            DrawBuySell(g, point, tickData, lastEndPrice);

            int x_middle = rect.X + rect.Width / 2;
            Point pointl = DrawContent(g, rect, x_left, x_middle, chartinfo, lastEndPrice, point);

            pointl.X = rect.X;
            pointl.Y += 26;
            DrawRecentTick(g, pointl, tickData, lastEndPrice);
        }

        private Point DrawContent(Graphics g, Rectangle rect, int x_left, int x_middle, CurrentInfo chartinfo, double lastJs, Point point)
        {
            int startY = rect.Y + 90;
            point.X = x_left;
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

            point.X = x_middle;
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
            return point;
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

        private Point DrawBuySell(Graphics g, Point point, ITickBar tick, double lastEndPrice)
        {
            int x_Left = point.X;

            int fontSize2 = 16;
            point.Y += 32;
            point.Y += 2;
            g.DrawString("卖出", new Font(fontStr, 12, FontStyle.Regular), new SolidBrush(ColorConfig.Color_White), point);

            point.X += 70;
            point.Y -= 2;
            string sellPriceStr = tick == null ? "----" : Math.Round(tick.SellPrice, 2).ToString();
            g.DrawString(sellPriceStr, new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(GetPriceColor(tick == null ? -1 : tick.SellPrice, lastEndPrice)), point);

            point.X += 80;
            string sellMountStr = tick == null ? "----" : tick.SellMount.ToString();
            g.DrawString(sellMountStr, new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

            point.X = x_Left;
            point.Y += 28;
            point.Y += 2;
            g.DrawString("买入", new Font(fontStr, 12, FontStyle.Regular), new SolidBrush(ColorConfig.Color_White), point);

            point.X += 70;
            point.Y -= 2;
            string buyPriceStr = tick == null ? "----" : Math.Round(tick.BuyPrice, 2).ToString();
            g.DrawString(buyPriceStr, new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(GetPriceColor(tick == null ? -1 : tick.BuyPrice, lastEndPrice)), point);

            point.X += 80;
            string buyMountStr = tick == null ? "----" : tick.BuyMount.ToString();
            g.DrawString(buyMountStr, new Font(fontStr, fontSize2, FontStyle.Bold), new SolidBrush(ColorConfig.Color_StockInfo), point);

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

        private void DrawRecentTick(Graphics g, Point startPoint, ITickData tickData, double lastEndPrice)
        {
            int defaultLeft = startPoint.X + 2;
            Point p = new Point(defaultLeft, startPoint.Y + 2);
            Color titleColor = ColorUtils.GetColor("#808080");
            g.DrawString("时间", new Font("宋体", 11, FontStyle.Regular), new SolidBrush(titleColor), p);
            p.X += 90;
            g.DrawString("价位", new Font("宋体", 11, FontStyle.Regular), new SolidBrush(titleColor), p);
            p.X += 40;
            g.DrawString("现手", new Font("宋体", 11, FontStyle.Regular), new SolidBrush(titleColor), p);
            p.X += 40;
            g.DrawString("增仓", new Font("宋体", 11, FontStyle.Regular), new SolidBrush(titleColor), p);
            p.X += 40;
            g.DrawString("开平", new Font("宋体", 11, FontStyle.Regular), new SolidBrush(titleColor), p);

            if (tickData == null)
                return;
            //TODO 未考虑周全
            int showCount = tickData.BarPos >= 15 ? 15 : tickData.BarPos;
            int startBarPos = tickData.BarPos - showCount;
            for (int i = 0; i < showCount; i++)
            {
                ITickBar tickBar = tickData.GetBar(startBarPos);
                ITickBar lastTickBar = startBarPos <= 0 ? null : tickData.GetBar(startBarPos - 1);
                p.X = defaultLeft;
                p.Y += 25;
                DrawTickBar(g, p, tickBar, lastTickBar, lastEndPrice);
                startBarPos++;
            }
        }

        private void DrawTickBar(Graphics g, Point p, ITickBar tickBar, ITickBar lastTickBar, double lastEndPrice)
        {
            Color titleColor = ColorUtils.GetColor("#808080");
            Color colorNormal = ColorConfig.Color_StockInfo;
            Color colorRed = ColorConfig.Color_TextUp;
            Color colorGreen = ColorConfig.Color_TextDown;

            g.DrawString(GetTimeFormat(tickBar.Time), new Font("宋体", 11, FontStyle.Bold), new SolidBrush(titleColor), p);
            p.X += 90;
            g.DrawString(tickBar.Price.ToString(), new Font("宋体", 11, FontStyle.Bold), new SolidBrush(tickBar.Price > lastEndPrice ? colorRed : colorGreen), p);
            p.X += 40;
            bool isUp = lastTickBar == null ? true : tickBar.Price >= lastTickBar.Price;
            g.DrawString(tickBar.Mount.ToString(), new Font("宋体", 11, FontStyle.Bold), new SolidBrush(isUp ? colorRed : colorGreen), p);
            p.X += 40;
            g.DrawString(tickBar.Add.ToString(), new Font("宋体", 11, FontStyle.Bold), new SolidBrush(colorNormal), p);
            p.X += 40;
            OpenCloseType oct = GetOpenOrClose(tickBar, lastTickBar);
            g.DrawString(GetOpenCloseTypeName(oct), new Font("宋体", 11, FontStyle.Bold), new SolidBrush(colorNormal), p);
        }

        private string GetTimeFormat(double time)
        {
            StringBuilder sb = new StringBuilder();
            int t = (int)((time - (int)time) * 1000000);
            sb.Append(GetStr(t / 10000)).Append(":");
            sb.Append(GetStr((t % 10000) / 100)).Append(":");
            sb.Append(GetStr((t % 100)));
            return sb.ToString();
        }

        private string GetStr(int i)
        {
            if (i == 0)
                return "00";
            if (i < 10)
                return "0" + i;
            return i.ToString();
        }

        private OpenCloseType GetOpenOrClose(ITickBar tickBar, ITickBar lastTickBar)
        {
            if (tickBar.Add == 0)
                return OpenCloseType.ChangeHand;
            bool isUp = lastTickBar == null ? true : tickBar.Price >= lastTickBar.Price;
            bool isOpen = tickBar.Add > 0;
            if (isUp)
                return isOpen ? OpenCloseType.OpenUp : OpenCloseType.CloseDown;
            return isOpen ? OpenCloseType.OpenDown : OpenCloseType.CloseUp;
        }

        public string GetOpenCloseTypeName(OpenCloseType oct)
        {
            switch (oct)
            {
                case OpenCloseType.OpenUp:
                    return "多开";
                case OpenCloseType.OpenDown:
                    return "空开";
                case OpenCloseType.CloseUp:
                    return "多平";
                case OpenCloseType.CloseDown:
                    return "空平";
                case OpenCloseType.ChangeHand:
                    return "换手";
            }
            return "";
        }
    }

    public enum OpenCloseType
    {
        /// <summary>
        /// 多开
        /// </summary>
        OpenUp = 0,

        /// <summary>
        /// 空开
        /// </summary>
        OpenDown = 1,

        /// <summary>
        /// 多平
        /// </summary>
        CloseUp = 2,

        /// <summary>
        /// 空平
        /// </summary>
        CloseDown = 3,

        /// <summary>
        /// 换手
        /// </summary>
        ChangeHand = 4
    }
}
