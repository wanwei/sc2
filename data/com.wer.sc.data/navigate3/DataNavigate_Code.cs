using com.wer.sc.data.cache.impl;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_Code
    {
        DataReaderFactory fac;
        private string code;
        private int startDate;
        private int endDate;
        private double currentTime;

        private Dictionary<KLinePeriod, DataNavigate_KLine> dicNavigateKLine = new Dictionary<KLinePeriod, DataNavigate_KLine>();

        private SimpleDataCache<RealTimeDataBuilder_DayData> cache_DataBuilder_Day1Minute = new SimpleDataCache<RealTimeDataBuilder_DayData>();

        private DataNavigate_TimeLine navigate_Real;

        private DataCache_Code dataCache_Code;

        private ITimeLineData currentRealData;

        private RealTimeDataBuilder_DayDataCache dayDataBuilderCache;

        public DataNavigate_Code(DataReaderFactory fac, string code, int startDate, int endDate, double currentTime)
        {
            this.fac = fac;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.currentTime = currentTime;
            this.dataCache_Code = new DataCache_Code(fac, code);
            this.dayDataBuilderCache = new RealTimeDataBuilder_DayDataCache(dataCache_Code);
            this.navigate_Real = new DataNavigate_TimeLine(fac, dataCache_Code, currentTime);
        }

        /// <summary>
        /// 设置或获取导航的开始日期
        /// </summary>
        public int StartDate { get { return startDate; } }

        /// <summary>
        /// 设置或获取导航的结束时间
        /// </summary>
        public int EndDate { get { return endDate; } }

        /// <summary>
        /// 得到当前股票或期货代码
        /// </summary>
        public String Code { get { return code; } }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        public double CurrentTime { get { return currentTime; } }

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (dicNavigateKLine.ContainsKey(period))
            {               
                return dicNavigateKLine[period].CurrentKLineData;
            }
            DataNavigate_KLine navigate_KLine = new DataNavigate_KLine(fac, code, period, currentTime);
            dicNavigateKLine.Add(period, navigate_KLine);
            navigate_KLine.CurrentTime = currentTime;
            return navigate_KLine.CurrentKLineData;
        }

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        public ITimeLineData GetRealData()
        {
            //if (IsCreateNewRealData())
            //{
            //    int date = chartBuilder.CurrentDate;
            //    LogHelper.Info(typeof(DataNavigate), "装载分时数据" + code + "-" + date);
            //    realData = dayDataCache.GetRealData(code, date);
            //}
            //return realData;
            //int date = chartb
            //dataCache_Code.GetCache_CodeDate(date)
            //int date = day
            //RealData r = dataCache_Code.GetCache_CodeDate(date);
            //dataCache_Code.GetCache_CodeDate()
            return null;
        }

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        public ITickData GetTickData()
        {
            return null;
        }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        public void ChangeTime(double time)
        {
            this.currentTime = time;
            foreach (DataNavigate_KLine kline in dicNavigateKLine.Values)
            {
                kline.CurrentTime = time;
            }

        }

        /// <summary>
        /// 前进，前进时会修改当前时间
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        public void Forward(KLinePeriod period, int len)
        {

        }

        /// <summary>
        /// 前进一个tick数据
        /// </summary>
        /// <param name="len"></param>
        public void ForwardTick(int len)
        {

        }
    }
}