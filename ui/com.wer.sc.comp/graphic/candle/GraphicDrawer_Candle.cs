using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Candle : GraphicDrawer_Compound, ICrossHairAttachable
    {
        private IGraphicData_Candle dataProvider;

        public IGraphicData_Candle DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
                drawer_chart.DataProvider = value;
                drawer_mount.DataProvider = value;
            }
        }

        private int selectIndex;
        public int SelectIndex
        {
            get
            {
                return selectIndex;
            }

            set
            {
                selectIndex = value;
            }
        }

        private GraphicDrawer_CandleChart drawer_chart;

        public GraphicDrawer_CandleChart Drawer_Chart
        {
            get { return drawer_chart; }
        }

        private GraphicDrawer_CandleMount drawer_mount;

        public GraphicDrawer_CandleMount Drawer_Mount
        {
            get { return drawer_mount; }
        }

        public GraphicDrawer_Candle()
        {
            this.MarginInfo = new GraphicMarginInfo(0, 20, 0, 20);
            this.Padding = new GraphicPaddingInfo(60, 0, 0, 0);
            this.drawer_chart = new GraphicDrawer_CandleChart();
            this.drawer_chart.MarginInfo = new GraphicMarginInfo(0, 0, 0, 1);
            this.drawer_chart.Padding = new GraphicPaddingInfo(0, 20, 50, 20);

            this.drawer_mount = new GraphicDrawer_CandleMount();
            this.drawer_mount.MarginInfo = new GraphicMarginInfo(0, 0, 0, 0);
            this.drawer_mount.Padding = new GraphicPaddingInfo(0, 20, 50, 0);
            this.AddGraph(drawer_chart, 0.7f);
            this.AddGraph(drawer_mount, 0.3f);
            this.crossHairDataPrivider = new CrossHairDataPrivider_Candle(this);
        }

        public override void Paint(Graphics graphic)
        {
            base.Paint(graphic);
            string txt = DataProvider.Code + "  " + dataProvider.GetKLineData().Period;
            Font font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            graphic.DrawString(txt, font, new SolidBrush(Color.White), new PointF(3, 3));
        }

        internal override void SetControl(Control control)
        {
            base.SetControl(control);
            control.PreviewKeyDown += Control_PreviewKeyDown;
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!this.IsEnable)
                return;
            if (e.KeyCode == Keys.Up)
            {
                int blockWidth = this.drawer_chart.BlockWidth;
                int newWidth = (int)(blockWidth * 1.5);
                if (newWidth == blockWidth)
                    newWidth += 1;
                if (newWidth >= 50)
                    newWidth = 50;
                this.drawer_chart.BlockWidth = newWidth;
                this.drawer_mount.BlockWidth = newWidth;
                this.Paint();
            }
            else if (e.KeyCode == Keys.Down)
            {
                int blockWidth = this.drawer_chart.BlockWidth;
                int newWidth = (int)(blockWidth / 1.5);
                if (newWidth == 0)
                    newWidth = 1;
                this.drawer_chart.BlockWidth = newWidth;
                this.drawer_mount.BlockWidth = newWidth;
                this.Paint();
           }
        }       

        private CrossHairDataProvider crossHairDataPrivider;

        public CrossHairDataProvider GetCrossHairDataProvider()
        {
            return crossHairDataPrivider;
        }
    }

    public class CrossHairDataPrivider_Candle : CrossHairDataProvider
    {
        private GraphicDrawer_Candle drawer;
        private CrossHairDrawer crossDrawer;

        public CrossHairDataPrivider_Candle(GraphicDrawer_Candle drawer)
        {
            this.drawer = drawer;
            this.drawer.AfterGraphicPaint += Drawer_AfterGraphicDraw;
        }

        private void Drawer_AfterGraphicDraw(object sender, GraphicRefreshArgs e)
        {
            if (this.AfterGraphicPaint != null)
                this.AfterGraphicPaint(sender, e);
        }

        public CrossHairDrawer CrossDrawer
        {
            get
            {
                return crossDrawer;
            }

            set
            {
                this.crossDrawer = value;
            }
        }

        public Control Control
        {
            get
            {
                return drawer.Control;
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return drawer.DisplayRect;
            }
        }

        public Pen GetPen()
        {
            return drawer.Drawer_Chart.ColorConfig.Pen_CrossHair;
        }

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                return drawer.Drawer_Chart.PriceMapping;
            }
        }

        public void DrawSelectBlock(Graphics g)
        {
            if (drawer.SelectIndex < 0)
                return;
            SelectedPointInfo blockInfo = GetBlockInfo(drawer.SelectIndex);
            if (blockInfo == null)
                return;
            blockInfo.DrawGraph(g, drawer.ColorConfig);
        }

        public SelectedPointInfo GetBlockInfo(int index)
        {
            IKLineData data = drawer.DataProvider.GetKLineData();
            KLineBar_KLineData chart = new KLineBar_KLineData(data, index);
            KLineBar_KLineData lastChart;
            if (index == 0)
                lastChart = null;
            else
                lastChart = new KLineBar_KLineData(data, index - 1);
            return GetBlockInfo(chart, lastChart);
        }

        private SelectedPointInfo GetBlockInfo(KLineBar_KLineData chart, KLineBar_KLineData lastChart)
        {
            SelectedPointInfo b = new SelectedPointInfo();
            b.LineHeight = 20;
            b.Width = 58;
            b.StartPoint = new Point(drawer.DisplayRect.X - b.Width, drawer.DisplayRect.Y);

            double lastEndPrice = lastChart != null ? lastChart.End : chart.Start;
            Pen pen = new Pen(Color.White);

            Brush brushNormal = new SolidBrush(Color.White);
            Font font = new Font("New Times Roman", 10, FontStyle.Regular);

            b.Lines.Add(new BlockLineInfo(FormatTime(chart.Time), brushNormal, font));
            b.Lines.Add(new BlockLineInfo("开盘", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.Start.ToString(), GetPriceBrush(chart.Start, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("最高", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.High.ToString(), GetPriceBrush(chart.High, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("最低", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.Low.ToString(), GetPriceBrush(chart.Low, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("收盘", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.End.ToString(), GetPriceBrush(chart.End, lastEndPrice), font));

            double uprange = Math.Round(chart.End - lastEndPrice, 2);
            b.Lines.Add(new BlockLineInfo(uprange.ToString(), GetPriceBrush(uprange, 0), font));
            //涨幅
            //double uppercent = Math.Round((chart.EndPrice - lastEndPrice) / lastEndPrice,2);
            double uppercent = Math.Round(uprange / lastEndPrice * 100, 2);
            b.Lines.Add(new BlockLineInfo(uppercent.ToString(), GetPriceBrush(uppercent, 0), font));
            return b;
        }

        private string FormatTime(double time)
        {
            if (drawer.DataProvider.Period.PeriodType >= KLineTimeType.DAY)
                return time.ToString();
            int itime = (int)time;
            int min = (int)Math.Round((time - itime) * 10000);
            double mtime = (double)itime / 10000;
            int date = (int)Math.Round((mtime - (int)mtime) * 100000000);
            string str = (date + min).ToString();
            return FillTimeStr(str);
        }

        private string FillTimeStr(string str)
        {
            if (str.Length < 8)
            {
                int fillLen = 8 - str.Length;
                string fillStr = "";
                for (int i = 0; i < fillLen; i++)
                {
                    fillStr += "0";
                }
                return fillStr + str;
            }
            return str;
        }

        private Brush GetPriceBrush(double price, double referPrice)
        {
            Brush brushEarn = new SolidBrush(ColorUtils.GetColor("#CC0000"));
            Brush brushLose = new SolidBrush(ColorUtils.GetColor("#00CC00"));
            return price >= referPrice ? brushEarn : brushLose;
        }

        public event AfterGraphicPaintHandler AfterGraphicPaint;

        public bool DoMoveNext()
        {
            int lastIndex = drawer.DataProvider.GetKLineData().BarPos;
            if (this.drawer.Drawer_Chart.PriceMapping.PriceRect.PriceRight >= lastIndex)
                return false;
            this.drawer.DataProvider.EndIndex++;
            return true;
        }

        public bool DoMovePrev()
        {
            if (this.drawer.Drawer_Chart.PriceMapping.PriceRect.PriceLeft - 1 < 0)
                return false;
            this.drawer.DataProvider.EndIndex--;
            return true;
        }

        public void DoRedraw()
        {
            this.drawer.Paint();
        }

        public void DoRedraw(Graphics g, Rectangle rect)
        {

        }

        public void DoSelectIndexChange(int index)
        {
            this.drawer.SelectIndex = index;
        }

        public Point GetCrossHairPoint(int selectIndex)
        {
            PriceGraphicMapping priceMapping = this.drawer.Drawer_Chart.PriceMapping;
            float x = priceMapping.CalcX(selectIndex);
            float y = priceMapping.CalcY(drawer.DataProvider.GetKLineData().Arr_End[selectIndex]);
            return new Point((int)x, (int)y);
        }

        public IGraphicData_Candle GetDataProvider()
        {
            return this.drawer.DataProvider;
        }

        public int[] IndexRange
        {
            get
            {
                return new int[] { drawer.DataProvider.StartIndex, drawer.DataProvider.EndIndex };
            }
        }
    }
}