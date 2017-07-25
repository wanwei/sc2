using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.reader;
using com.wer.sc.comp.graphic.info;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;

namespace com.wer.sc.ui.comp
{
    public partial class CompCurrentInfo : UserControl
    {
        private CompChartData compChartData;

        private GraphicDrawer_CurrentInfo drawer;

        private IGraphicData_CurrentInfo graphicData_CurrentInfo;

        public CompChartData CompChartData
        {
            get
            {
                return compChartData;
            }

            set
            {
                InitWithCompChartData(value);
            }
        }


        public CompCurrentInfo()
        {
            InitializeComponent();
            //this.compChartData = compChartData;
            //this.drawer = new GraphicDrawer_CurrentInfo();
            //
            //this.drawer.DataProvider = 
        }

        private void InitWithCompChartData(CompChartData value)
        {
            if (value == null)
                return;
            this.compChartData = value;
            ITickData tickData = GetTickData();
            CurrentInfo currentInfo = GetCurrentInfo(tickData);
            this.graphicData_CurrentInfo = new GraphicData_CurrentInfo(currentInfo, tickData);
            this.drawer = new GraphicDrawer_CurrentInfo();
            this.drawer.DataProvider = this.graphicData_CurrentInfo;
            this.drawer.BindControl(this);
            this.compChartData.OnDataRefresh += CompChartData_OnDataRefresh;
        }

        private void CompChartData_OnDataRefresh(object sender, DataRefreshArgument arg)
        {
            if (this.compChartData.CurrentRealTimeDataReader == null)
                return;
            ITickData tickData = GetTickData();
            CurrentInfo currentInfo = GetCurrentInfo(tickData);
            graphicData_CurrentInfo.ChangeData(currentInfo, tickData);
            this.drawer.Paint();
        }

        private ITickData GetTickData()
        {
            if (this.compChartData.CurrentRealTimeDataReader == null)
                return null;
            return this.compChartData.CurrentRealTimeDataReader.GetTickData();
        }

        private CurrentInfo GetCurrentInfo(ITickData tick)
        {
            CurrentInfo chartinfo = new CurrentInfo();
            chartinfo.code = this.compChartData.Code;
            if (tick == null)
                return chartinfo;
            //ITickData tick = null;
            //CurrentInfo chartinfo = new CurrentInfo();
            ////ITickData tick = navigate.CurrentTickData;
            ITickBar tickBar = tick.GetCurrentBar();
            //////List<RealDataInfo> reals = currentInfo.GetReal();
            //////List<ChartInfo> charts = currentInfo.GetChart(ChartPeriod.DAY, 1);
            ////ITickBar tickChart = tick.GetBar(navigate.CurrentTickIndex);
            chartinfo.currentPrice = Math.Round(tick.Price, 2);
            chartinfo.currentHand = tickBar.Mount;
            chartinfo.totalHand = tickBar.TotalMount;
            chartinfo.totalHold = tickBar.Hold;
            chartinfo.dailyAdd = 0;
            chartinfo.outMount = 0;
            chartinfo.outPercent = 0.5;
            chartinfo.inMount = 0;
            chartinfo.inPercent = 0.5;

            //////RealDataInfo r = reals[reals.Count - 1];
            //////ChartInfo chart = charts[0];
            ITimeLineData realData = compChartData.CurrentRealTimeDataReader.GetTimeLineData();

            ITimeLineBar realChart = realData.GetCurrentBar();
            chartinfo.upRange = Math.Round(realChart.UpRange, 2);
            chartinfo.upPercent = realChart.UpPercent;
            chartinfo.upSpeed = 0;
            //chartinfo.open = realData.StartPrice;
            //chartinfo.high = chart.HighPrice;
            //chartinfo.low = chart.LowPrice;
            ////chartinfo.jsPrice = 0;
            ////chartinfo.lastJsPrice = Math.Round(r.LastJs, 2);
            ////double maxUprange = (int)(r.LastJs * 0.04);
            ////chartinfo.maxUp = r.LastJs + maxUprange;
            ////chartinfo.maxDown = r.LastJs - maxUprange;
            ////return chartinfo;
            //return chartinfo;            
            return chartinfo;
        }
    }
}
