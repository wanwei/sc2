using com.wer.sc.data.cache;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_KLine
    {
        private KLineData_RealTime klineData;
        private double currentTime;

        private RealTimeDataBuilder_KLine klineChartBuilder;

        public DataNavigate_KLine(DataReaderFactory factory, string code, KLinePeriod period, double time)
        {
            DataCacheFactory cacheFactory = new DataCacheFactory(factory);
            IDataCache_Code cache = cacheFactory.CreateCache_Code(code);
            ITradingDayReader openDateReader = cache.GetOpenDateReader();

            Init(factory, code, period, time, openDateReader.FirstOpenDate, openDateReader.LastOpenDate);
        }

        public DataNavigate_KLine(DataReaderFactory factory, string code, KLinePeriod period, double time, int startDate, int endDate)
        {
            Init(factory, code, period, time, startDate, endDate);
        }

        private void Init(DataReaderFactory factory, string code, KLinePeriod period, double time, int startDate, int endDate)
        {
            KLineData data = (KLineData)factory.KLineDataReader.GetData(code, startDate, endDate, period);
            this.klineData = new KLineData_RealTime(data);
            DataCacheFactory cacheFac = factory.CacheFactory;
            this.klineChartBuilder = new RealTimeDataBuilder_KLine(klineData, cacheFac.CreateCache_Code(code, startDate, endDate), time);
            this.CurrentTime = time;
        }

        public String Code { get { return this.klineChartBuilder.Code; } }

        /// <summary>
        /// 得到当前K线数据
        /// </summary>
        public IKLineData CurrentKLineData
        {
            get
            {
                return klineData;
            }
        }

        public double CurrentTime
        {
            get
            {
                return currentTime;
            }

            set
            {
                if (currentTime == value)
                    return;
                currentTime = value;
                this.klineChartBuilder.ChangeTime(value);
                IKLineBar klineChart = this.klineChartBuilder.GetCurrentChart();

                //设置为修改当前barpos
                int index = TimeIndeierUtils.IndexOfTime_KLine(klineData, currentTime);
                klineData.BarPos = index;
                klineData.SetRealTimeData(klineChart);
                //int currentKLineIndex = klineData.IndexOfTime(currentTime);
                //((KLineData)klineData).ChangeChart(klineChart, currentKLineIndex);
            }
        }
    }
}