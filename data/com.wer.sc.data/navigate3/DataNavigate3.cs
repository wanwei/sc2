using com.wer.sc.data.cache;
using com.wer.sc.data.cache.impl;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate3 : IDataNavigate3
    {
        private DataCacheFactory dataCacheFac;

        private DataReaderFactory dataReaderFac;

        private DayDataCache dayDataCache;

        private IDataCache_Code dataCache_Code;

        private KLineData_RealTime klineData;

        private ITimeLineData realData;

        private String code;

        private KLinePeriod period;

        private double time;

        private int currentKLineIndex;

        private RealTimeDataBuilder_KLine chartBuilder;

        public event DataChangeEventHandler OnDataChangeHandler;

        public DataNavigate3(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
            this.dataCacheFac = new DataCacheFactory(dataReaderFac);
            this.dayDataCache = new DayDataCache(dataReaderFac);
        }

        public String Code { get { return code; } }

        public void Change(IKLineData data, double time)
        {
            this.klineData = (KLineData_RealTime)data;
            this.code = data.Code;
            this.period = data.Period;
            this.ChangeTime(time);
        }

        public void Change(string code, double time, KLinePeriod period)
        {
            if (this.code == code && this.period.Equals(period) && IsInCurrentKLineData(time))
            {
                ChangeTime(time);
                return;
            }

            dataCache_Code = dataCacheFac.CreateCache_Code(code);

            int extendBefore = 30;
            int extendAfter = 30;
            if (period.PeriodType == KLineTimeType.MINUTE)
            {
                extendBefore = 30;
                extendAfter = 30;
            }
            else if (period.PeriodType == KLineTimeType.HOUR)
            {
                extendBefore = 100;
                extendAfter = 100;
            }
            else if (period.PeriodType == KLineTimeType.DAY)
            {
                extendBefore = 2000;
                extendAfter = 2000;
            }

            int date = (int)time;
            int start = (int)TimeUtils.AddDays(date, -extendBefore);
            int end = (int)TimeUtils.AddDays(date, extendAfter);
            KLineData data = (KLineData)dataReaderFac.KLineDataReader.GetData(code, start, end, period);
            this.klineData = new KLineData_RealTime(data);
            this.code = code;
            this.period = period;

            ChangeTime(time);
        }

        public void ChangeCode(string code)
        {
            if (this.code == code)
                return;
            Change(code, time, period);
        }

        public void ChangePeriod(KLinePeriod period)
        {
            if (this.period.Equals(period))
                return;
            Change(code, time, period);
        }

        //private KLineDataIndeier klineDataIndeier;

        public void ChangeTime(double time)
        {
            if (this.time == time)
                return;
            if (!IsInCurrentKLineData(time))
            {
                Change(code, time, period);
                return;
            }
            RefreshChartBuilder(time);
            this.time = time;

            this.currentKLineIndex = TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
            this.chartBuilder.ChangeTime(time);
            IKLineBar chart = this.chartBuilder.GetCurrentChart();
            klineData.SetRealTimeData(chart, currentKLineIndex);
        }

        public void ChangeIndex(int index)
        {
            if (index >= klineData.Length)
                return;
            klineData.SetRealTimeData(null);
            this.currentKLineIndex = index;
            this.time = klineData.Arr_Time[index];
        }

        private void RefreshChartBuilder(double time)
        {
            //bool isNight = (time - (int)time > 0.18);
            //int date = (int)time;
            //if (isNight)
            //{
            //    date += 1;
            //    date = (int)(this.klineData.IndexOfTime(date));
            //}
            if (this.chartBuilder == null || this.chartBuilder.Code != this.code)
                this.chartBuilder = new RealTimeDataBuilder_KLine(klineData, dataCache_Code, time);

            //int date = DaySpliter.GetTimeDate(time, dataReaderFac.OpenDateReader);
            //if (this.chartBuilder == null || this.chartBuilder.Date != date || this.chartBuilder.Code != this.code)
            //{
            //    TickData tickData = dayDataCache.GetTickData(code, date);
            //    IKLineData minuteKlineData = dayDataCache.GetKLineData(code, date);
            //    this.chartBuilder = new KLineChartBuilder_AllPeriod(klineData, dataCache_Code, time);
            //}
        }

        private bool IsInCurrentKLineData(double time)
        {
            return time >= CurrentKLineData.Arr_Time[0] && time <= CurrentKLineData.Arr_Time[klineData.Length - 1];
        }

        public IKLineData CurrentKLineData
        {
            get
            {
                return klineData;
            }
        }

        public int CurrentKLineIndex
        {
            get
            {
                return currentKLineIndex;
            }
        }

        public ITimeLineData CurrentRealData
        {
            get
            {
                if (IsCreateNewRealData())
                {
                    int date = chartBuilder.CurrentDate;
                    LogHelper.Info(typeof(DataNavigate3), "装载分时数据" + code + "-" + date);
                    realData = dayDataCache.GetRealData(code, date);
                }
                return realData;
            }
        }

        private bool IsCreateNewRealData()
        {
            if (realData == null)
                return true;
            if (realData.Code != this.code)
                return true;
            if (realData.Date != this.chartBuilder.CurrentDate)
                return true;
            return false;
        }

        public int CurrentRealIndex
        {
            get
            {
                return realData.IndexOfTime(CurrentTime);
            }
        }

        public ITickData CurrentTickData
        {
            get
            {
                return chartBuilder.ChartBuilder_FromTick.TickData;
            }
        }

        public int CurrentTickIndex
        {
            get
            {
                return chartBuilder.ChartBuilder_FromTick.TickData.BarPos;
            }
        }

        public double CurrentTime
        {
            get
            {
                return time;
            }
        }
    }

    class DataNavigate_Single
    {
        private DayDataCache cache;

        public DataNavigate_Single(DayDataCache cache)
        {
            this.cache = cache;
        }
    }



    ///// <summary>
    ///// 数据导航
    ///// 可指定时间并获取该时间的各种时间周期的K线数据
    ///// </summary>
    //class DataNavigate2
    //{
    //    private DataReaderFactory dataReaderFac;

    //    private String code;

    //    private float currentTime;

    //    private IKLineData data;

    //    private int startDate;

    //    private int endDate;

    //    private KLinePeriod period;

    //    private int currentIndex;

    //    private KLineChart currentChart;


    //    DataNavigate2(DataReaderFactory dataReaderFac)
    //    {
    //        this.dataReaderFac = dataReaderFac;
    //    }

    //    #region 修改数据

    //    /// <summary>
    //    /// 修改提供的数据
    //    /// </summary>
    //    /// <param name="code"></param>
    //    /// <param name="startDate"></param>
    //    /// <param name="endDate"></param>
    //    /// <param name="period"></param>
    //    public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
    //    {
    //        this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
    //        this.code = code;
    //        this.period = period;
    //        this.startDate = startDate;
    //        this.endDate = endDate;
    //        int currentDate = (int)currentTime;
    //    }

    //    public void ChangeCode(String code)
    //    {
    //        this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
    //        this.code = code;
    //        this.ChangeTime(currentTime);
    //    }

    //    /// <summary>
    //    /// 修改周期
    //    /// </summary>
    //    /// <param name="period"></param>
    //    public void ChangePeriod(KLinePeriod period)
    //    {
    //        this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
    //        this.period = period;
    //        this.ChangeTime(currentTime);
    //    }

    //    public void ChangeTime(float time)
    //    {
    //        this.currentTime = time;
    //    }

    //    public void NextChart()
    //    {

    //    }
    //    public void NextChart(int period)
    //    {

    //    }

    //    #endregion

    //    #region 获取数据

    //    public string Code
    //    {
    //        get
    //        {
    //            return code;
    //        }
    //    }

    //    public IKLineData GetKLineData()
    //    {
    //        return this.data;
    //    }

    //    public int CurrentIndex
    //    {
    //        get
    //        {
    //            return currentIndex;
    //        }
    //    }

    //    public KLineChart GetKLineChart()
    //    {
    //        return currentChart;
    //    }

    //    #endregion
    //}




}