using com.wer.sc.data;
using System;

namespace com.wer.sc.comp.graphic.info
{
    public class GraphicDataProvider_CurrentInfo_Nav : IGraphicDataProvider_CurrentInfo
    {
        private IDataNavigate3 navigate;

        private IGraphicOperator_CurrentInfo graphicOperator;

        public GraphicDataProvider_CurrentInfo_Nav(DataReaderFactory fac)
        {
            this.navigate = fac.DataNavigate;
            this.graphicOperator = new GraphicOperator_CurrentInfo_Nav(navigate);
        }

        public GraphicDataProvider_CurrentInfo_Nav(IDataNavigate3 navigate)
        {
            this.navigate = navigate;
            this.graphicOperator = new GraphicOperator_CurrentInfo_Nav(navigate);
        }

        public CurrentInfo GetCurrentInfo()
        {
            CurrentInfo chartinfo = new CurrentInfo();
            ITickData tick = navigate.CurrentTickData;

            //List<RealDataInfo> reals = currentInfo.GetReal();
            //List<ChartInfo> charts = currentInfo.GetChart(ChartPeriod.DAY, 1);
            ITickBar tickChart = tick.GetBar(navigate.CurrentTickIndex);
            chartinfo.currentPrice = Math.Round(tick.Price, 2);
            chartinfo.currentHand = tickChart.Mount;
            chartinfo.totalHand = tickChart.TotalMount;
            chartinfo.totalHold = tickChart.Hold;
            chartinfo.dailyAdd = 0;
            chartinfo.outMount = 0;
            chartinfo.outPercent = 0.5;
            chartinfo.inMount = 0;
            chartinfo.inPercent = 0.5;

            //RealDataInfo r = reals[reals.Count - 1];
            //ChartInfo chart = charts[0];
            ITimeLineData realData = navigate.CurrentRealData;

            ITimeLineBar realChart = realData.GetCurrentBar();
            chartinfo.upRange = Math.Round(realChart.UpRange, 2);
            chartinfo.upPercent = realChart.UpPercent;
            chartinfo.upSpeed = 0;
            //chartinfo.open = realData.StartPrice;
            //chartinfo.high = chart.HighPrice;
            //chartinfo.low = chart.LowPrice;
            //chartinfo.jsPrice = 0;
            //chartinfo.lastJsPrice = Math.Round(r.LastJs, 2);
            //double maxUprange = (int)(r.LastJs * 0.04);
            //chartinfo.maxUp = r.LastJs + maxUprange;
            //chartinfo.maxDown = r.LastJs - maxUprange;
            //return chartinfo;
            return chartinfo;
        }

        public ITickData GetCurrentTickData()
        {
            return navigate.CurrentTickData;
        }

        public int CurrentTickIndex
        {
            get { return navigate.CurrentTickIndex; }
        }

        public IGraphicOperator_CurrentInfo GetOperator()
        {
            return graphicOperator;
        }
    }
}
