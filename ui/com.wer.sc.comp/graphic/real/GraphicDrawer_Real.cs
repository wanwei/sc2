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

namespace com.wer.sc.comp.graphic.real
{
    public class GraphicDrawer_Real : GraphicDrawer_Compound
    {
        private IGraphicDataProvider_Real dataProvider;

        public IGraphicDataProvider_Real DataProvider
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

        private PriceGraphicMapping priceMapping = new PriceGraphicMapping();

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                priceMapping.DrawRect = DisplayRect;
                //priceMapping.PriceRect = GetPriceRectangle();
                return priceMapping;
            }
        }


        public GraphicDrawer_RealChart drawer_chart;

        internal GraphicDrawer_RealMount drawer_mount;
        public GraphicDrawer_Real()
        {
            this.MarginInfo = new GraphicMarginInfo(0, 20, 20, 20);
            this.Padding = new GraphicPaddingInfo(80, 0, 0, 0);
            this.drawer_chart = new GraphicDrawer_RealChart();
            this.drawer_chart.MarginInfo = new GraphicMarginInfo(0, 0, 0, 1);
            this.drawer_chart.Padding = new GraphicPaddingInfo(0, 20, 0, 0);

            this.drawer_mount = new GraphicDrawer_RealMount();
            this.drawer_mount.MarginInfo = new GraphicMarginInfo(0, 0, 0, 0);
            this.drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 0, 0);
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
            this.BindOthers(control);
        }

        public void BindOthers(Control control)
        {
            this.control = control;
            CrossHairDataPrivider_Real crossHairProvider = new CrossHairDataPrivider_Real(this);
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
            DateTime dt = DateTime.Now;
            base.DrawGraph(graphic);
            DrawSelectBlock(graphic);
            crossHairDrawer.DrawGraphic(graphic);
            StringBuilder sb = new StringBuilder();
            TimeSpan span = DateTime.Now.Subtract(dt);
            sb.Append("画分时线" + dataProvider.CurrentTime + "；十字线位置：" + crossHairDrawer.CrossPoint
                + ";共花费" + (span.Seconds * 1000 + span.Milliseconds) + "毫秒");
            LogHelper.Info(typeof(GraphicDrawer_Real), sb.ToString());
        }

        public void DrawGraph(Graphics graphic, Rectangle rect)
        {
            if (!IsEnable)
                return;
            drawer_chart.DrawGraph(graphic, rect);
            drawer_mount.DrawGraph(graphic, rect);
            DrawSelectBlock(graphic);
        }

        public void DrawSelectBlock(Graphics g)
        {
            if (selectIndex < 0)
                return;
            int index = selectIndex > DataProvider.CurrentIndex ? DataProvider.CurrentIndex : selectIndex;
            SelectedPointInfo blockInfo = GetBlockInfo(index, selectIndex);
            if (blockInfo == null)
                return;
            blockInfo.DrawGraph(g, ColorConfig);
        }

        public SelectedPointInfo GetBlockInfo(int index, int timeIndex)
        {
            ITimeLineData data = dataProvider.GetRealData();

            SelectedPointInfo info = new SelectedPointInfo();
            info.LineHeight = 20;
            info.Width = 58;
            info.StartPoint = new Point(DisplayRect.X - info.Width, DisplayRect.Y);

            Brush brushNormal = new SolidBrush(Color.White);
            Font font = new Font("New Times Roman", 10, FontStyle.Regular);
            Brush brushRed = ColorConfig.Brush_CandleBlockUp;
            Brush brushGreen = ColorConfig.Brush_CandleBlockDown;

            //TODO
            info.Lines.Add(new BlockLineInfo("----", new SolidBrush(ColorUtils.GetColor("#CCCC00")), font));
            info.Lines.Add(new BlockLineInfo("时间", brushNormal, font));
            info.Lines.Add(new BlockLineInfo(GetTimeString(data.Arr_Time[timeIndex]), brushNormal, font));
            info.Lines.Add(new BlockLineInfo("价格", brushNormal, font));
            info.Lines.Add(new BlockLineInfo(data.Arr_Price[index].ToString(), brushNormal, font));
            info.Lines.Add(new BlockLineInfo("均价", brushNormal, font));
            //TODO
            info.Lines.Add(new BlockLineInfo("----", brushNormal, font));
            info.Lines.Add(new BlockLineInfo("涨跌", brushNormal, font));
            info.Lines.Add(new BlockLineInfo(data.Arr_UpRange[index].ToString(), brushNormal, font));
            info.Lines.Add(new BlockLineInfo((data.Arr_UpPercent[index] + "%").ToString(), brushNormal, font));
            info.Lines.Add(new BlockLineInfo("量", brushNormal, font));
            info.Lines.Add(new BlockLineInfo(data.Arr_Mount[index].ToString(), brushNormal, font));
            info.Lines.Add(new BlockLineInfo("持仓", brushNormal, font));
            info.Lines.Add(new BlockLineInfo(data.Arr_Hold[index].ToString(), brushNormal, font));
            int add = GetHoldAdd(index);
            Brush b;
            if (index == 0 || add == 0)
                b = brushNormal;
            else if (add > 0)
                b = brushRed;
            else
                b = brushGreen;
            info.Lines.Add(new BlockLineInfo(add.ToString(), b, font));
            return info;
        }

        private String GetTimeString(double time)
        {
            time = ((time - (int)time) * 100);
            int hour = (int)time;
            int minute = (int)((time - hour) * 100);
            return hour + ":" + (minute < 10 ? "0" + minute : minute.ToString());
        }

        private int GetHoldAdd(int index)
        {
            ITimeLineData data = dataProvider.GetRealData();
            if (index == 0)
                return data.Arr_Hold[0];
            return (data.Arr_Hold[index] - data.Arr_Hold[index - 1]);
        }
    }

    public class CrossHairDataPrivider_Real : CrossHairDataPrivider
    {
        private GraphicDrawer_Real drawer;

        public CrossHairDataPrivider_Real(GraphicDrawer_Real drawer)
        {
            this.drawer = drawer;
            //drawer.AfterGraphicDraw += Drawer_AfterGraphicDraw;
        }

        //private void Drawer_AfterGraphicDraw(object sender, GraphicRefreshArgs e)
        //{
        //    if (this.AfterGraphicDraw != null)
        //        this.AfterGraphicDraw(sender, e);
        //}
        private CrossHairDrawer crossDrawer;
        public CrossHairDrawer CrossDrawer
        {
            get { return crossDrawer; }
            set { crossDrawer = value; }
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

        //public event AfterGraphicDrawHandler AfterGraphicDraw;

        public bool DoMoveNext()
        {
            return false;
        }

        public bool DoMovePrev()
        {
            return false;
        }

        public void DoRedraw()
        {
            this.drawer.DrawGraph();
        }

        public void DoRedraw(Graphics g, Rectangle rect)
        {
            this.drawer.DrawGraph(g, rect);
        }

        public void DoSelectIndexChange(int index)
        {
            this.drawer.SelectIndex = index;
        }

        public Point GetCrossHairPoint(int selectIndex)
        {
            PriceGraphicMapping priceMapping = this.drawer.drawer_chart.PriceMapping;
            float x = priceMapping.CalcX(selectIndex);
            float y = priceMapping.CalcY(drawer.DataProvider.GetRealData().Arr_Price[selectIndex]);
            return new Point((int)x, (int)y);
        }

        public int[] IndexRange
        {
            get { return new int[] { 0, drawer.DataProvider.GetRealData().Length - 1 }; }
        }
    }
}
