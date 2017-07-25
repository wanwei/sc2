using com.wer.sc.data;
using com.wer.sc.data.navigate;
using System;

namespace com.wer.sc.comp.graphic.info
{
    public class GraphicData_CurrentInfo : IGraphicData_CurrentInfo
    {
        //private IDataNavigate navigate;

        //private IGraphicOperator_CurrentInfo graphicOperator;

        private CurrentInfo detailInfo = new CurrentInfo();

        private ITickData tickData;

        private CurrentInfo currentInfo;

        public GraphicData_CurrentInfo(CurrentInfo currentInfo, ITickData tickData)
        {
            this.ChangeData(currentInfo, tickData);
        }

        public CurrentInfo GetCurrentInfo()
        {
            //ITickData tick = null;
            //CurrentInfo chartinfo = new CurrentInfo();
            ////ITickData tick = navigate.CurrentTickData;
            //ITickBar tickBar = tick.GetCurrentBar();
            //////List<RealDataInfo> reals = currentInfo.GetReal();
            //////List<ChartInfo> charts = currentInfo.GetChart(ChartPeriod.DAY, 1);
            ////ITickBar tickChart = tick.GetBar(navigate.CurrentTickIndex);
            //chartinfo.currentPrice = Math.Round(tick.Price, 2);
            //chartinfo.currentHand = tickBar.Mount;
            //chartinfo.totalHand = tickBar.TotalMount;
            //chartinfo.totalHold = tickBar.Hold;
            //chartinfo.dailyAdd = 0;
            //chartinfo.outMount = 0;
            //chartinfo.outPercent = 0.5;
            //chartinfo.inMount = 0;
            //chartinfo.inPercent = 0.5;

            //////RealDataInfo r = reals[reals.Count - 1];
            //////ChartInfo chart = charts[0];
            ////ITimeLineData realData = navigate.CurrentRealData;

            //ITimeLineBar realChart = realData.GetCurrentBar();
            //chartinfo.upRange = Math.Round(realChart.UpRange, 2);
            //chartinfo.upPercent = realChart.UpPercent;
            //chartinfo.upSpeed = 0;
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
            return currentInfo;
        }

        //public ITickData GetCurrentTickData()
        //{
        //    return navigate.CurrentTickData;
        //}

        //public int CurrentTickIndex
        //{
        //    get { return navigate.CurrentTickIndex; }
        //}

        //public IGraphicOperator_CurrentInfo GetOperator()
        //{
        //    return graphicOperator;
        //}

        public void ChangeData(CurrentInfo currentInfo, ITickData tickData)
        {
            this.currentInfo = currentInfo;
            this.tickData = tickData;
        }

        public ITickData GetCurrentTickData()
        {
            return tickData;
        }


    }
}
