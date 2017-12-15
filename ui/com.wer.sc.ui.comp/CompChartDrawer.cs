using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.comp.graphic.utils;
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
    public class CompChartDrawer
    {
        private CompChartData compChartData;

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

        public CompChartDrawer(Control control, CompChartData compChartData)
        {
            this.control = control;
            this.compChartData = compChartData;
            this.Init();
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
            g.DrawString("合约或股票ID: " + GetObjectStr(compChartData.Code), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("时间: " + GetObjectStr(compChartData.Time), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("图表类型: " + GetObjectStr(compChartData.ChartType), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("K线周期: " + GetObjectStr(compChartData.KlinePeriod), MyFont1, brush, new PointF(x, y));
        }

        private string GetObjectStr(Object obj)
        {
            return obj == null ? "----" : obj.ToString();
        }

        private GraphicDrawer_Candle InitGraphicDrawer_Candle()
        {
            this.drawer_Candle = new GraphicDrawer_Candle();
            IKLineData klineData = this.compChartData.CurrentRealTimeDataReader.GetKLineData(compChartData.KlinePeriod);
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
            ITimeLineData timeLineData = compChartData.CurrentRealTimeDataReader.GetTimeLineData();

            int barPos = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, compChartData.Time);
            timeLineData.BarPos = barPos;

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            drawer_TimeLine.DataProvider = graphicData_TimeLine;
            return this.drawer_TimeLine;
        }

        private void GraphicData_Candle_OnGraphicDataChange(object sender, GraphicDataChangeArgument arg)
        {
            if (OnChartRefresh != null)
                OnChartRefresh(this, new ChartRefreshArguments(false, GetChartState(compChartData.GetChartDataState()), GetChartState(compChartData.GetChartDataState())));
        }

        private ChartState GetChartState(ChartDataState chartDataState)
        {
            ChartState state = new ChartState();
            state.ChartDataState = chartDataState;

            if (this.drawer_Candle != null)
            {
                IGraphicData_Candle graphicData = this.drawer_Candle.DataProvider;
                state.CandleStartIndex = graphicData.StartIndex;
                state.CandleEndIndex = graphicData.EndIndex;
                state.CandleBlockMount = graphicData.BlockMount;
            }
            return state;
        }

        public event DelegateOnChartRefresh OnChartRefresh;

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.graphicDrawer.CurrentIndex == 0)
                    this.compChartData.ChartType = ChartType.TimeLine;
                else
                    this.compChartData.ChartType = ChartType.KLine;
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
                    if (this.compChartData.ChartType == ChartType.KLine)
                        RefreshCandleDrawer();
                    else if (this.compChartData.ChartType == ChartType.TimeLine)
                        RefreshTimeLineDrawer();
                    else if (this.compChartData.ChartType == ChartType.Tick)
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
            IKLineData klineData = compChartData.CurrentRealTimeDataReader.GetKLineData(compChartData.KlinePeriod);
            this.graphicData_Candle.ChangeData(klineData);
            this.graphicDrawer.Switch(0);
        }

        private void RefreshTimeLineDrawer()
        {
            ITimeLineData timeLineData = compChartData.CurrentRealTimeDataReader.GetTimeLineData();

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            this.drawer_TimeLine.DataProvider = graphicData_TimeLine;
            this.graphicDrawer.Switch(1);
        }

        private void RefreshTickDrawer()
        {

        }
    }
}
