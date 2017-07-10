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
    public class GraphicDrawer_Candle : GraphicDrawer_Compound
    {
        private IGraphicDrawer_Chart_Candle dataProvider;

        public IGraphicDrawer_Chart_Candle DataProvider
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

        public override bool IsEnable
        {
            get
            {
                return base.IsEnable;
            }

            set
            {
                base.IsEnable = value;
                crossHairDrawer.Enable = value;
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

        public GraphicDrawer_CandleChart drawer_chart;

        internal GraphicDrawer_CandleMount drawer_mount;

        public GraphicDrawer_Candle()
        {
            this.MarginInfo = new GraphicMarginInfo(0, 20, 20, 20);
            this.Padding = new GraphicPaddingInfo(80, 0, 0, 0);
            this.drawer_chart = new GraphicDrawer_CandleChart();
            this.drawer_chart.MarginInfo = new GraphicMarginInfo(0, 0, 0, 1);
            this.drawer_chart.Padding = new GraphicPaddingInfo(0, 20, 50, 0);

            this.drawer_mount = new GraphicDrawer_CandleMount();
            this.drawer_mount.MarginInfo = new GraphicMarginInfo(0, 0, 0, 0);
            this.drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 50, 0);
            this.AddGraph(drawer_chart, 0.7f);
            this.AddGraph(drawer_mount, 0.3f);

            crossHairDrawer = new CrossHairDrawer();
        }

        CrossHairDrawer crossHairDrawer;

        public CrossHairDrawer CrossHairDrawer
        {
            get
            {
                return crossHairDrawer;
            }
        }

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            BindOthers(control);
        }

        public void BindOthers(Control control)
        {
            this.control = control;
            CrossHairDataPrivider_Candle crossHairProvider = new CrossHairDataPrivider_Candle(this);
            crossHairDrawer.Bind(crossHairProvider);
        }

        public override void UnBindControl()
        {
            base.UnBindControl();
        }

        public override void DrawGraph(Graphics graphic)
        {
            if (!IsEnable)
                return;
            //crossHairDrawer.DrawGraphic(graphic);
            base.DrawGraph(graphic);
            DrawSelectBlock(graphic);
            crossHairDrawer.DrawGraphic(graphic);
        }

        public void DrawSelectBlock(Graphics g)
        {
            if (selectIndex < 0)
                return;
            SelectedPointInfo blockInfo = GetBlockInfo(selectIndex);
            if (blockInfo == null)
                return;
            blockInfo.DrawGraph(g, ColorConfig);
        }

        public SelectedPointInfo GetBlockInfo(int index)
        {
            IKLineData data = dataProvider.GetKLineData();
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
            b.StartPoint = new Point(DisplayRect.X - b.Width, DisplayRect.Y);

            double lastEndPrice = lastChart != null ? lastChart.End : chart.Start;
            Pen pen = new Pen(Color.White);

            Brush brushNormal = new SolidBrush(Color.White);
            Font font = new Font("New Times Roman", 10, FontStyle.Regular);

            //int len = chart.Time.Length;
            b.Lines.Add(new BlockLineInfo(chart.Time.ToString(), brushNormal, font));
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
        private Brush GetPriceBrush(double price, double referPrice)
        {
            Brush brushEarn = new SolidBrush(ColorUtils.GetColor("#CC0000"));
            Brush brushLose = new SolidBrush(ColorUtils.GetColor("#00CC00"));
            return price >= referPrice ? brushEarn : brushLose;
        }
    }

    public class CrossHairDataPrivider_Candle : CrossHairDataPrivider
    {
        private GraphicDrawer_Candle drawer;
        private CrossHairDrawer crossDrawer;

        public CrossHairDataPrivider_Candle(GraphicDrawer_Candle drawer)
        {
            this.drawer = drawer;
            drawer.AfterGraphicDraw += Drawer_AfterGraphicDraw;
        }

        private void Drawer_AfterGraphicDraw(object sender, GraphicRefreshArgs e)
        {
            if (this.AfterGraphicDraw != null)
                this.AfterGraphicDraw(sender, e);
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
                return drawer.control;
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return drawer.DisplayRect;
            }
        }

        public Pen Pen
        {
            get
            {
                return drawer.drawer_chart.ColorConfig.Pen_CrossHair;
            }
        }

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                return drawer.drawer_chart.PriceMapping;
            }
        }

        public event AfterGraphicDrawHandler AfterGraphicDraw;

        public bool DoMoveNext()
        {
            int lastIndex = drawer.DataProvider.GetKLineData().Length - 1;
            if (this.drawer.drawer_chart.PriceMapping.PriceRect.PriceRight + 1 > lastIndex)
                return false;
            this.drawer.DataProvider.EndIndex++;
            return true;
        }

        public bool DoMovePrev()
        {
            if (this.drawer.drawer_chart.PriceMapping.PriceRect.PriceLeft - 1 < 0)
                return false;
            this.drawer.DataProvider.EndIndex--;
            return true;
        }

        public void DoRedraw()
        {
            this.drawer.DrawGraph();
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
            PriceGraphicMapping priceMapping = this.drawer.drawer_chart.PriceMapping;
            float x = priceMapping.CalcX(selectIndex);
            float y = priceMapping.CalcY(drawer.DataProvider.GetKLineData().Arr_End[selectIndex]);
            return new Point((int)x, (int)y);
        }

        public IGraphicDrawer_Chart_Candle GetDataProvider()
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