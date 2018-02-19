using com.wer.sc.data.cache;
using com.wer.sc.data.utils;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 当前k线数据获得
    /// </summary>
    public class RealTimeDataBuilder_KLine
    {
        private IDataCache_Code dataCache_Code;

        private IDataCache_CodeDate currentDataCache_CodeDate;

        private IKLineData klineData;

        private RealTimeDataBuilder_DayData klineChartBuilder_1Minute;

        public RealTimeDataBuilder_DayData ChartBuilder_FromTick
        {
            get { return klineChartBuilder_1Minute; }
        }

        private IKLineBar currentChart;

        private double currentTime;

        private int currentDate;

        public int CurrentDate
        {
            get
            {
                return currentDate;
            }
        }

        public string Code
        {
            get { return klineData.Code; }
        }

        public RealTimeDataBuilder_KLine(IKLineData srcData, IDataCache_Code cache_Code, double currentTime)
        {
            this.dataCache_Code = cache_Code;
            this.klineData = srcData;
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(double time)
        {
            if (currentTime == time)
                return;
            //比如 20150531140500
            //1分钟线可以正常显示
            //日线需要将            

            int date = DaySplitter.GetTimeDate(time, dataCache_Code.GetOpenDateReader());
            if (currentDate != date)
            {
                //TODO 将以前生成的klineChartBuilder_1Minute cache下来
                this.currentDataCache_CodeDate = dataCache_Code.GetCache_CodeDate(date);
                this.klineChartBuilder_1Minute = new RealTimeDataBuilder_DayData(currentDataCache_CodeDate, time);
                this.currentDate = date;
            }
            
            int index = klineData.IndexOfTime(time);
            double t = klineData.Arr_Time[index];

            IKLineData todayMinuteKLineData = klineChartBuilder_1Minute.MinuteKlineData;
            KLineBar currentMinuteChart = klineChartBuilder_1Minute.GetCurrentChart();

            //TODO 这里最多只支持到日线，两日线或以上不支持
            int startIndex;
            if (t == (int)t)
                startIndex = 0;
            else
                startIndex = todayMinuteKLineData.IndexOfTime(t);
            int endIndex = todayMinuteKLineData.IndexOfTime(currentMinuteChart.Time);

            IKLineBar currentChart = todayMinuteKLineData.GetAggrKLineBar(startIndex, endIndex - 1);
            KLineBarMerge.MergeTo((KLineBar)currentChart, currentMinuteChart);
            this.currentChart = currentChart;
            this.currentTime = time;
        }

        public IKLineBar GetCurrentChart()
        {
            return currentChart;
        }
    }
}