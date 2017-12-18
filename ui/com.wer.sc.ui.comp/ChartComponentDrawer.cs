using com.wer.sc.graphic;
using com.wer.sc.graphic.timeline;
using com.wer.sc.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentDrawer
    {
        private ChartComponentData compData;

        private ChartComponentController compDataController;

        private Control control;

        private GraphicDrawer_Switch graphicDrawer;

        //蜡烛图画图器
        private GraphicDrawer_Candle drawer_Candle;

        //蜡烛图数据
        private IGraphicData_Candle graphicData_Candle;

        //分时图画图器
        private GraphicDrawer_TimeLine drawer_TimeLine;

        //分时图数据
        private IGraphicData_TimeLine graphicData_TimeLine;

        private bool inited;

        public IGraphicDrawer GraphicDrawer
        {
            get { return graphicDrawer; }
        }

        public ChartComponentDrawer(Control control, ChartComponentController compDataController)
        {
            this.control = control;
            this.compDataController = compDataController;
            this.compData = compDataController.ChartComponentData;
            this.compDataController.OnDataChanged += CompDataController_OnDataChanged;
            this.Init();
        }

        private void CompDataController_OnDataChanged(object sender, ChartComponentDataChangeArgument arg)
        {
            this.RePaint();
        }

        private void Init()
        {
            if (inited)
                return;
            try
            {
                this.graphicDrawer = new GraphicDrawer_Switch();

                this.drawer_Candle = InitGraphicDrawer_Candle();
                this.drawer_TimeLine = InitGraphicDrawer_TimeLine();

                this.graphicDrawer.Drawers.Add(drawer_Candle);
                this.graphicDrawer.Drawers.Add(drawer_TimeLine);
                this.graphicDrawer.BindControl(this.control);
                this.graphicDrawer.Control.KeyDown += Control_KeyDown;
                CrossHairDrawer crossHairDrawer = new CrossHairDrawer();
                crossHairDrawer.Bind(this.graphicDrawer);
                this.inited = true;
            }
            catch (Exception e)
            {
                LogHelper.Warn(GetType(), e);
                this.PaintEmpty();
            }
        }

        public void PaintEmpty()
        {
            Graphics g = this.control.CreateGraphics();
            Rectangle rect = this.control.DisplayRectangle;
            g.FillRectangle(new SolidBrush(Color.Black), rect);
            SolidBrush brush = new SolidBrush(Color.White);
            SolidBrush redbrush = new SolidBrush(Color.Red);
            Font MyFontTitle = new Font("宋体", 16, FontStyle.Regular);
            Font MyFont1 = new Font("宋体", 14, FontStyle.Regular);

            int x = 20;
            int y = 20;
            g.DrawString("数据无法正常显示，参数：", MyFontTitle, redbrush, new PointF(x, y));
            y += 30;
            int between = 25;
            //g.DrawString("数据中心地址: " + GetObjectStr(DataCenterUri), MyFont1, brush, new PointF(x, y));
            //y += between;
            g.DrawString("合约或股票ID: " + GetObjectStr(compData.Code), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("时间: " + GetObjectStr(compData.Time), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("图表类型: " + GetObjectStr(compData.ChartType), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("K线周期: " + GetObjectStr(compData.KlinePeriod), MyFont1, brush, new PointF(x, y));
        }

        private string GetObjectStr(Object obj)
        {
            return obj == null ? "----" : obj.ToString();
        }

        private GraphicDrawer_Candle InitGraphicDrawer_Candle()
        {
            this.drawer_Candle = new GraphicDrawer_Candle();
            IKLineData klineData = this.compDataController.CurrentRealTimeDataReader.GetKLineData(compData.KlinePeriod);
            this.graphicData_Candle = GraphicDataFactory.CreateGraphicData_Candle(klineData, 0, klineData.BarPos);
            this.graphicData_Candle.OnGraphicDataChange += GraphicData_Candle_OnGraphicDataChange;
            this.drawer_Candle.DataProvider = graphicData_Candle;
            return this.drawer_Candle;
        }

        public IGraphicData_Candle GraphicData_Candle
        {
            get { return graphicData_Candle; }
        }

        private GraphicDrawer_TimeLine InitGraphicDrawer_TimeLine()
        {
            this.drawer_TimeLine = new GraphicDrawer_TimeLine();
            ITimeLineData timeLineData = compDataController.CurrentRealTimeDataReader.GetTimeLineData();

            int barPos = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, compData.Time);
            timeLineData.BarPos = barPos;

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            drawer_TimeLine.DataProvider = graphicData_TimeLine;
            return this.drawer_TimeLine;
        }

        private void GraphicData_Candle_OnGraphicDataChange(object sender, GraphicDataChangeArgument arg)
        {
            //if (OnChartRefresh != null)
            //    OnChartRefresh(this, new ChartRefreshArguments(false, GetChartState(compChartData.GetChartDataState()), GetChartState(compChartData.GetChartDataState())));
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.graphicDrawer.CurrentIndex == 0)
                    this.compDataController.ChangeChartType(ChartType.TimeLine);
                else
                    this.compDataController.ChangeChartType(ChartType.KLine);
            }
        }

        private bool isPainting = false;

        private Object paintObj = new object();

        public void RePaint()
        {
            if (isPainting)
                return;
            lock (paintObj)
            {
                try
                {
                    isPainting = true;
                    if (this.compData.ChartType == ChartType.KLine)
                        RefreshCandleDrawer();
                    else if (this.compData.ChartType == ChartType.TimeLine)
                        RefreshTimeLineDrawer();
                    else if (this.compData.ChartType == ChartType.Tick)
                        RefreshTickDrawer();
                    this.graphicDrawer.Paint();
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                    PaintEmpty();
                }
                finally
                {
                    isPainting = false;
                }
            }
        }

        private void RefreshCandleDrawer()
        {
            IKLineData klineData = compDataController.CurrentRealTimeDataReader.GetKLineData(compData.KlinePeriod);
            klineData.BarPos = compData.ShowKLineIndex;
            this.graphicData_Candle.ChangeData(klineData);
            this.graphicDrawer.Switch(0);
        }

        private void RefreshTimeLineDrawer()
        {
            ITimeLineData timeLineData = compDataController.CurrentRealTimeDataReader.GetTimeLineData();

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            this.drawer_TimeLine.DataProvider = graphicData_TimeLine;
            this.graphicDrawer.Switch(1);
        }

        private void RefreshTickDrawer()
        {

        }

        public IGraphicDrawer_PriceRect Drawer_PriceRect
        {
            get
            {
                switch (this.compData.ChartType)
                {
                    case ChartType.KLine:
                        return drawer_Candle.Drawer_Chart;
                    case ChartType.TimeLine:
                        return drawer_TimeLine.drawer_chart;
                    case ChartType.Tick:
                        return null;
                }
                return null;
            }
        }

        public PriceGraphicMapping GraphicMapping
        {
            get
            {
                switch (this.compData.ChartType)
                {
                    case ChartType.KLine:
                        return drawer_Candle.Drawer_Chart.PriceMapping;
                    case ChartType.TimeLine:
                        return drawer_TimeLine.drawer_chart.PriceMapping;
                    case ChartType.Tick:
                        return null;
                }
                return null;
            }
        }
    }
}
