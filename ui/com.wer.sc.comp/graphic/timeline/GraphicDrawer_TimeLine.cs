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

namespace com.wer.sc.comp.graphic.timeline
{
    public class GraphicDrawer_TimeLine : GraphicDrawer_Compound, ICrossHairAttachable
    {
        private IGraphicData_TimeLine dataProvider;

        public IGraphicData_TimeLine DataProvider
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
        
        public PriceGraphicMapping PriceMapping
        {
            get
            {
                return this.drawer_chart.PriceMapping;
            }
        }

        public GraphicDrawer_TimeLineChart drawer_chart;

        internal GraphicDrawer_TimeLineMount drawer_mount;
        public GraphicDrawer_TimeLine()
        {
            this.MarginInfo = new GraphicMarginInfo(0, 20, 0, 20);
            this.Padding = new GraphicPaddingInfo(60, 0, 50, 0);
            this.drawer_chart = new GraphicDrawer_TimeLineChart();
            this.drawer_chart.MarginInfo = new GraphicMarginInfo(0, 0, 0, 1);
            this.drawer_chart.Padding = new GraphicPaddingInfo(0, 20, 0, 0);

            this.drawer_mount = new GraphicDrawer_TimeLineMount();
            this.drawer_mount.MarginInfo = new GraphicMarginInfo(0, 0, 0, 0);
            this.drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 0, 0);
            this.AddGraph(drawer_chart, 0.7f);
            this.AddGraph(drawer_mount, 0.3f);

            //crossHairDrawer = new CrossHairDrawer();
        }

        //CrossHairDrawer crossHairDrawer;

        //public CrossHairDrawer CrossHairDrawer
        //{
        //    get
        //    {
        //        return crossHairDrawer;
        //    }
        //}

        //public override void BindControl(Control control)
        //{
        //    base.BindControl(control);
        //    this.BindOthers(control);
        //}

        //public void BindOthers(Control control)
        //{
        //    this.control = control;
        //    CrossHairDataPrivider_Real crossHairProvider = new CrossHairDataPrivider_Real(this);
        //    crossHairDrawer.Bind(crossHairProvider);
        //}

        //public override void UnBindControl()
        //{
        //    base.UnBindControl();
        //}

        //public override void Paint(Graphics graphic)
        //{
        //    if (!IsEnable)
        //        return;
        //    //crossHairDrawer.DrawGraphic(graphic);
        //    DateTime dt = DateTime.Now;
        //    base.Paint(graphic);
        //    DrawSelectBlock(graphic);
        //    crossHairDrawer.DrawGraphic(graphic);
        //    //StringBuilder sb = new StringBuilder();
        //    //TimeSpan span = DateTime.Now.Subtract(dt);
        //    //sb.Append("画分时线" + dataProvider.CurrentTime + "；十字线位置：" + crossHairDrawer.CrossHairPoint
        //    //    + ";共花费" + (span.Seconds * 1000 + span.Milliseconds) + "毫秒");
        //    //LogHelper.Info(typeof(GraphicDrawer_Real), sb.ToString());
        //}

        public void DrawGraph(Graphics graphic, Rectangle rect)
        {
            if (!IsEnable)
                return;
            drawer_chart.DrawGraph(graphic, rect);
            drawer_mount.DrawGraph(graphic, rect);
            //DrawSelectBlock(graphic);
        }

        private CrossHairDataProvider crossHairDataProvider;

        public CrossHairDataProvider GetCrossHairDataProvider()
        {
            if (crossHairDataProvider == null)
                crossHairDataProvider = new CrossHairDataPrivider_Real(this);
            return crossHairDataProvider;
        }
    }

    public class CrossHairDataPrivider_Real : CrossHairDataProvider
    {
        private GraphicDrawer_TimeLine drawer;

        public CrossHairDataPrivider_Real(GraphicDrawer_TimeLine drawer)
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

        public event AfterGraphicPaintHandler AfterGraphicPaint;

        public CrossHairDrawer CrossDrawer
        {
            get { return crossDrawer; }
            set { crossDrawer = value; }
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
            return drawer.drawer_chart.ColorConfig.Pen_CrossHair;
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
            this.drawer.Paint();
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


        public void DrawSelectBlock(Graphics g)
        {
            if (drawer.SelectIndex < 0)
                return;
            int index = drawer.SelectIndex > drawer.DataProvider.CurrentIndex ? drawer.DataProvider.CurrentIndex : drawer.SelectIndex;
            SelectedPointInfo blockInfo = GetBlockInfo(index, drawer.SelectIndex);
            if (blockInfo == null)
                return;
            blockInfo.DrawGraph(g, drawer.ColorConfig);
        }

        public SelectedPointInfo GetBlockInfo(int index, int timeIndex)
        {
            ITimeLineData data = drawer.DataProvider.GetRealData();

            SelectedPointInfo info = new SelectedPointInfo();
            info.LineHeight = 20;
            info.Width = 58;
            info.StartPoint = new Point(drawer.DisplayRect.X - info.Width, drawer.DisplayRect.Y);

            Brush brushNormal = new SolidBrush(Color.White);
            Font font = new Font("New Times Roman", 10, FontStyle.Regular);
            Brush brushRed = drawer.ColorConfig.Brush_CandleBlockUp;
            Brush brushGreen = drawer.ColorConfig.Brush_CandleBlockDown;

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
            ITimeLineData data = drawer.DataProvider.GetRealData();
            if (index == 0)
                return data.Arr_Hold[0];
            return (data.Arr_Hold[index] - data.Arr_Hold[index - 1]);
        }

        public int[] IndexRange
        {
            get { return new int[] { 0, drawer.DataProvider.GetRealData().Length - 1 }; }
        }
    }
}
