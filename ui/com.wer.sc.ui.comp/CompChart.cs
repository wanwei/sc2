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
using com.wer.sc.strategy;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.ui.comp
{
    public partial class CompChart : UserControl
    {
        private CompChartDrawer compChartDrawer;

        private CompChartData compChartData;

        public CompChartData CompChartData
        {
            get { return compChartData; }
        }

        //chart是否完成初始化
        private bool inited = false;

        //是否需要刷新图像，如果修改了参数，则会刷新
        public bool RePaintGraphic
        {
            get
            {
                return false;
                //return this.compChartData.IsDataRefresh;
            }
        }

        private object lockInitObj = new object();

        public CompChart()
        {
            InitializeComponent();
        }

        #region 初始化和修改数据

        public void Init(string code, double time)
        {
            Init(code, time, KLinePeriod.KLinePeriod_1Minute);
        }

        public void Init(string code, double time, KLinePeriod period)
        {
            if (inited)
                return;
            inited = true;

            this.compChartData = new CompChartData();
            this.compChartData.Code = code;
            this.compChartData.Time = time;
            this.compChartData.KlinePeriod = period;

            this.compChartDrawer = new CompChartDrawer(this, compChartData);
            this.drawHelper = new CompChart_DrawHelper(this);
            this.strategyHelper = new StrategyOperator(drawHelper);
            this.compChartData.OnDataRefresh += CompChartData_OnDataRefresh;

            this.compChartDrawer.RePaint();
        }


        /// <summary>
        /// 切换数据包，并切换时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        public void Change(IDataPackage_Code dataPackage, double time)
        {

        }


        /// <summary>
        /// 修改图中显示的品种
        /// </summary>
        /// <param name="code"></param>
        public void Change(string code)
        {
            if (code == compChartData.Code)
                return;
            this.compChartData.Code = code;
        }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        public void Change(double time)
        {
            this.compChartData.Time = time;
        }

        /// <summary>
        /// 修改图中显示的品种和当前时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public void Change(String code, double time)
        {
            this.compChartData.Code = code;
            this.compChartData.Time = time;
        }

        public void Change(string code, double time, KLinePeriod period)
        {
            this.compChartData.Code = code;
            this.compChartData.Time = time;
            this.compChartData.KlinePeriod = period;
        }

        public void ChangeKLinePeriod(KLinePeriod klinePeriod)
        {
            this.compChartData.KlinePeriod = klinePeriod;
        }

        /// <summary>
        /// 视图向前进或后退，
        /// </summary>
        /// <param name="forwardPeriod"></param>
        public void ForwardTime(ForwardPeriod forwardPeriod) {
        }

        /// <summary>
        /// 切换图中显示的图形，K线、分时线或tick线
        /// </summary>
        /// <param name="chartType"></param>
        public void ChangeChartType(ChartType chartType) {
        }

        /// <summary>
        /// 修改K线每一个bar显示的宽度
        /// </summary>
        /// <param name="width"></param>
        public void ChangeBarWidth(double width) { }

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间，只会改变当前显示的K线
        /// </summary>
        /// <param name="cnt"></param>
        public void ForwardView(int cnt) { }


        #endregion

        private StrategyOperator strategyHelper;

        private CompChart_DrawHelper drawHelper;

        public StrategyOperator StrategyHelper
        {
            get { return strategyHelper; }
        }

        private void CompChartData_OnDataRefresh(object sender, DataRefreshArgument arg)
        {
            if (!inited)
                return;
            this.compChartDrawer.RePaint();
            //if (OnChartRefresh != null)
            //    OnChartRefresh(this, new ChartRefreshArguments(true, GetChartState(arg.OldChartDataState), GetChartState(arg.CurrentChartDataState)));
        }

        public event DelegateOnChartRefresh OnChartRefresh;

        [Browsable(true), DisplayName("合约或股票代码"), Description("合约或股票代码"), Category("自定义属性"), DefaultValue(null)]
        public string Code
        {
            get
            {
                return compChartData.Code;
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
            return KlinePeriod;
        }

        [Browsable(true), DisplayName("K线周期"), Description("K线周期"), Category("自定义属性")]
        public KLinePeriod KlinePeriod
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
            //if (this.ChartType != ChartType.KLine)
            //    return;
            //if (this.graphicData_Candle == null)
            //    return;
            //IKLineData klineData = this.graphicData_Candle.GetKLineData();
            //int endIndex = klineData.BarPos;
            //int lengthToEnd = endIndex - this.graphicData_Candle.EndIndex;
            //if (lengthToEnd < length)
            //    this.graphicData_Candle.EndIndex = endIndex;
            //else
            //    this.graphicData_Candle.EndIndex += length;
            //this.graphicDrawer.Paint();
        }

        public void Backward(int length)
        {
            //if (this.ChartType != ChartType.KLine)
            //    return;
            //if (this.graphicData_Candle == null)
            //    return;
            //int realLength = this.graphicData_Candle.StartIndex > length ? length : 0;
            //if (realLength == 0)
            //    return;
            //this.graphicData_Candle.EndIndex -= realLength;
            //this.graphicDrawer.Paint();
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
                this.compChartDrawer.PaintEmpty();
                return;
            }

            this.compChartDrawer.RePaint();
        }


        public void Play()
        {
            this.compChartData.Play();
        }

        public void Pause()
        {
            this.compChartData.Pause();
        }

        public PriceGraphicMapping CurrentChartGraphicMapping
        {
            get
            {
                switch (this.ChartType)
                {
                    //case ChartType.KLine:
                    //    return drawer_Candle.Drawer_Chart.PriceMapping;
                    //case ChartType.TimeLine:
                    //    return drawer_TimeLine.drawer_chart.PriceMapping;
                    //case ChartType.Tick:
                    //    return null;
                }
                return null;
            }
        }

        public IGraphicDrawer_PriceRect CurrentPriceRectDrawer
        {
            get
            {
                switch (this.ChartType)
                {
                    //case ChartType.KLine:
                    //    return drawer_Candle.Drawer_Chart;
                    //case ChartType.TimeLine:
                    //    return drawer_TimeLine.drawer_chart;
                    //case ChartType.Tick:
                    //    return null;
                }
                return null;
            }
        }

        public IGraphicData CurrentGraphicData
        {
            get
            {
                IGraphicDrawer_PriceRect drawer = CurrentPriceRectDrawer;
                if (drawer == null)
                    return null;
                return drawer.GraphicData;
            }
        }
    }


    public delegate void DelegateOnChartRefresh(object sender, ChartRefreshArguments arg);

    public class ChartRefreshArguments
    {
        private bool dataRefreshed;

        private ChartState oldChartState;

        private ChartState currentChartState;

        public ChartRefreshArguments(bool dataRefreshed, ChartState oldChartState, ChartState chartState)
        {
            this.dataRefreshed = dataRefreshed;
            this.oldChartState = oldChartState;
            this.currentChartState = chartState;
        }

        public bool IsDataPackageChange
        {
            get
            {
                return !currentChartState.ChartDataState.DataPackageInfo.Equals(oldChartState.ChartDataState.DataPackageInfo);
            }
        }

        public bool IsKLinePeriodChange
        {
            get
            {
                return currentChartState.ChartDataState.klinePeriod != oldChartState.ChartDataState.klinePeriod;
            }
        }

        public bool DataRefreshed
        {
            get
            {
                return dataRefreshed;
            }
        }

        public ChartState OldChartState
        {
            get
            {
                return oldChartState;
            }
        }

        public ChartState CurrentChartState
        {
            get
            {
                return currentChartState;
            }
        }
    }

    public class ChartState
    {
        public int CandleStartIndex;

        public int CandleEndIndex;

        public int CandleBlockMount;

        public ChartDataState ChartDataState;
    }
}