using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.reader;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using com.wer.sc.comp.graphic.utils;
using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.data.navigate;
using com.wer.sc.data.forward;

namespace com.wer.sc.ui.comp
{
    public partial class CompChart : UserControl
    {
        private CompChartData compChartData;

        public CompChartData CompChartData
        {
            get { return compChartData; }
        }

        //private IDataNavigate_Code dataNavigate_Code;

        //private IHistoryDataForward_Code historyDataForward_Code;

        //private IRealTimeDataReader currentRealTimeDataReader;

        //chart是否完成初始化
        private bool Inited = false;

        //是否需要刷新图像，如果修改了参数，则会刷新
        public bool RePaintGraphic
        {
            get { return this.compChartData.IsDataRefresh; }
        }

        //控件的画图器
        private GraphicDrawer_Switch graphicDrawer;

        //蜡烛图画图器
        private GraphicDrawer_Candle drawer_Candle;

        public GraphicDrawer_Candle Drawer_Candle
        {
            get { return drawer_Candle; }
        }

        private IGraphicData_Candle graphicData_Candle;

        private GraphicDrawer_TimeLine drawer_TimeLine;

        private IGraphicData_TimeLine graphicData_TimeLine;

        private object lockInitObj = new object();

        public CompChart()
        {
            InitializeComponent();
            this.compChartData = new CompChartData();
            this.compChartData.OnDataRefresh += CompChartData_OnDataRefresh;
        }

        public event DelegateOnDataRefresh OnDataRefresh;

        private void CompChartData_OnDataRefresh(object sender, DataRefreshArgument arg)
        {
            if (OnDataRefresh != null)
                OnDataRefresh(this, arg);
        }

        [Browsable(true), DisplayName("数据中心"), Description("数据中心"), Category("自定义属性"), DefaultValue(null)]
        public string DataCenterUri
        {
            get
            {
                return this.compChartData.DataCenterUri;
            }

            set
            {
                this.compChartData.DataCenterUri = value;
                //if (dataCenterUri == value)
                //    return;
                //try
                //{
                //    this.dataCenterUri = value;
                //    this.dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
                //    this.tradingSessionReader = null;
                //}
                //catch (Exception e)
                //{
                //    this.dataCenterUri = null;
                //    this.dataReader = null;
                //    MessageBox.Show("数据中心'" + dataCenterUri + "'创建失败");
                //}
                //this.RePaintGraphic = true;
            }
        }

        [Browsable(true), DisplayName("合约或股票代码"), Description("合约或股票代码"), Category("自定义属性"), DefaultValue(null)]
        public string Code
        {
            get
            {
                return compChartData.Code;
            }

            set
            {
                this.compChartData.Code = value;
            }
        }

        [Browsable(true), DisplayName("图表类型"), Description("图表类型"), Category("自定义属性"), DefaultValue(ChartType.KLine)]
        public ChartType ChartType
        {
            get
            {
                return compChartData.ChartType;
            }

            set
            {
                this.compChartData.ChartType = value;
            }
        }

        [Browsable(true), DisplayName("当前时间"), Description("当前时间"), Category("自定义属性"), DefaultValue(20150107.093005)]
        public double Time
        {
            get
            {
                return compChartData.Time;
            }

            set
            {
                this.compChartData.Time = value;
            }
        }

        [Browsable(true), DisplayName("K线柱子宽度"), Description("显示K线数量"), Category("自定义属性"), DefaultValue(5)]
        public float KLineBlockWidth
        {
            get
            {
                return this.compChartData.KLineBlockWidth;
            }

            set
            {
                this.compChartData.KLineBlockWidth = value;
            }
        }

        public KLinePeriod GetKLinePeriod()
        {
            return new KLinePeriod(KlineTimeType, KlinePeriod);
        }

        [Browsable(true), DisplayName("K线周期"), Description("K线周期"), Category("自定义属性")]
        public int KlinePeriod
        {
            get
            {
                return this.compChartData.KlinePeriod;
            }

            set
            {
                this.compChartData.KlinePeriod = value;
            }
        }

        [Browsable(true), DisplayName("K线种类"), Description("K线种类"), Category("自定义属性"), DefaultValue(KLineTimeType.MINUTE)]
        public KLineTimeType KlineTimeType
        {
            get
            {
                return this.compChartData.KlineTimeType;
            }

            set
            {
                this.compChartData.KlineTimeType = value;
            }
        }

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return this.compChartData.ForwardPeriod;
            }

            set
            {
                this.compChartData.ForwardPeriod = value;
            }
        }

        public void Forward(int length)
        {
            if (this.ChartType != ChartType.KLine)
                return;
            if (this.graphicData_Candle == null)
                return;
            IKLineData klineData = this.graphicData_Candle.GetKLineData();
            int endIndex = klineData.BarPos;
            int lengthToEnd = endIndex - this.graphicData_Candle.EndIndex;
            if (lengthToEnd < length)
                this.graphicData_Candle.EndIndex = endIndex;
            else
                this.graphicData_Candle.EndIndex += length;
            this.graphicDrawer.Paint();
        }

        public void Backward(int length)
        {
            if (this.ChartType != ChartType.KLine)
                return;
            if (this.graphicData_Candle == null)
                return;
            int realLength = this.graphicData_Candle.StartIndex > length ? length : 0;
            if (realLength == 0)
                return;
            this.graphicData_Candle.EndIndex -= realLength;
            this.graphicDrawer.Paint();
        }

        public void ForwardTime()
        {
            this.compChartData.ForwardTime();
        }

        public void BackwardTime()
        {
            this.compChartData.BackwardTime();
        }

        public void PaintChart()
        {
            this.CompChart_Paint(null, null);
        }

        private void CompChart_Paint(object sender, PaintEventArgs e)
        {
            if (!this.compChartData.CheckData())
            {
                PaintEmpty();
                return;
            }

            if (!Inited)
                Init();
            else
            {
                if (this.RePaintGraphic)
                {
                    RefreshData();
                }
            }
        }

        private void PaintEmpty()
        {
            Graphics g = this.CreateGraphics();
            Rectangle rect = this.DisplayRectangle;
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
            g.DrawString("数据中心地址: " + GetObjectStr(DataCenterUri), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("合约或股票ID: " + GetObjectStr(Code), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("时间: " + GetObjectStr(Time), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("图表类型: " + GetObjectStr(ChartType), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("K线周期: " + GetObjectStr(new KLinePeriod(KlineTimeType, KlinePeriod)), MyFont1, brush, new PointF(x, y));
        }

        private string GetObjectStr(Object obj)
        {
            return obj == null ? "----" : obj.ToString();
        }

        private void Init()
        {
            lock (lockInitObj)
            {
                if (Inited)
                    return;
                try
                {
                    //if (this.dataReader == null)
                    //    return;

                    this.graphicDrawer = new GraphicDrawer_Switch();
                    //if (this.TradingSessionReader == null)
                    //    return;
                    //int date = TradingSessionReader.GetTradingDay(time);
                    //if (date < 0)
                    //    return;

                    drawer_Candle = InitGraphicDrawer_Candle();
                    if (drawer_Candle == null)
                        return;
                    drawer_TimeLine = InitGraphicDrawer_TimeLine();
                    if (drawer_TimeLine == null)
                        return;

                    this.graphicDrawer.Drawers.Add(drawer_Candle);
                    this.graphicDrawer.Drawers.Add(drawer_TimeLine);
                    this.graphicDrawer.BindControl(this);
                    this.graphicDrawer.Control.KeyDown += Control_KeyDown;
                    CrossHairDrawer crossHairDrawer = new CrossHairDrawer();
                    crossHairDrawer.Bind(this.graphicDrawer);
                    this.Inited = true;
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                }
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.graphicDrawer.CurrentIndex == 0)
                    this.graphicDrawer.Switch(1);
                else
                    this.graphicDrawer.Switch(0);
            }
        }

        private GraphicDrawer_Candle InitGraphicDrawer_Candle()
        {
            this.drawer_Candle = new GraphicDrawer_Candle();
            KLinePeriod period = new KLinePeriod(this.KlineTimeType, this.KlinePeriod);
            IKLineData klineData = compChartData.CurrentRealTimeDataReader.GetKLineData(period);
            this.graphicData_Candle = GraphicDataFactory.CreateGraphicData_Candle(klineData, 0, klineData.BarPos);
            drawer_Candle.DataProvider = graphicData_Candle;
            return this.drawer_Candle;
        }

        private GraphicDrawer_TimeLine InitGraphicDrawer_TimeLine()
        {
            this.drawer_TimeLine = new GraphicDrawer_TimeLine();
            ITimeLineData timeLineData = compChartData.CurrentRealTimeDataReader.GetTimeLineData();

            int barPos = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, Time);
            timeLineData.BarPos = barPos;

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            drawer_TimeLine.DataProvider = graphicData_TimeLine;
            return this.drawer_TimeLine;
        }

        private void RefreshData()
        {
            //int date = this.tradingSessionReader.GetTradingDay(time);
            //if (date < 0)
            //    return;
            if (this.ChartType == ChartType.KLine)
                RefreshCandleDrawer();
            else if (this.ChartType == ChartType.TimeLine)
                RefreshTimeLineDrawer();
            else if (this.ChartType == ChartType.Tick)
                RefreshTickDrawer();

            this.graphicDrawer.Paint();
            //this.RePaintGraphic = false;
        }

        private void RefreshCandleDrawer()
        {
            try
            {
                KLinePeriod period = new KLinePeriod(this.KlineTimeType, this.KlinePeriod);
                IKLineData klineData = compChartData.CurrentRealTimeDataReader.GetKLineData(period);
                //int date = TradingSessionReader.GetTradingDay(time);            
                //IKLineData klineData = dataReader.KLineDataReader.GetData(code, date, date, 500, 100, period);
                if (klineData == null)
                {
                    MessageBox.Show("未能找到" + Code + " -" + Time + "的" + period + "周期K线数据");
                    return;
                }

                //int barPos = TimeIndeierUtils.IndexOfTime_KLine(klineData, Time);
                //klineData.BarPos = barPos;
                graphicData_Candle.ChangeData(klineData);
                graphicData_Candle.EndIndex = klineData.BarPos;
                this.graphicDrawer.Switch(0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //this.graphicDrawer.Paint();
        }

        private void RefreshTimeLineDrawer()
        {
            //KLinePeriod period = new KLinePeriod(this.KlineTimeType, this.KlinePeriod);
            //IKLineData klineData = compChartData.CurrentRealTimeDataReader.GetKLineData(period);

            //int date = TradingSessionReader.GetTradingDay(time);
            ITimeLineData timeLineData = compChartData.CurrentRealTimeDataReader.GetTimeLineData();
            //  dataReader.TimeLineDataReader.GetData(code, date);

            //int barPos = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, time);
            //timeLineData.BarPos = barPos;

            this.graphicData_TimeLine = GraphicDataFactory.CreateGraphicData_TimeLine(timeLineData);
            drawer_TimeLine.DataProvider = graphicData_TimeLine;
            this.graphicDrawer.Switch(1);
            //this.graphicDrawer.Paint();
        }

        private void RefreshTickDrawer()
        {

        }

        public void Play()
        {
            this.compChartData.Play();
        }

        public void Pause()
        {
            this.compChartData.Pause();
        }

        //public CompChartData CompData
        //{
        //    get { return this.compChartData; }
        //}
    }

    public enum ChartType
    {

        KLine = 0,

        TimeLine = 1,

        Tick = 2
    }
}
