using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class DataPackage_Code : IDataPackage_Code
    {
        private IDataReader dataReader;

        private string code;

        private int startDate;

        private int endDate;

        private IList<int> allTradingDays;

        private ITradingDayReader tradingDayReader;

        private Dictionary<KLinePeriod, IKLineData_Extend> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_Extend>();

        private int minBefore;

        private int minAfter;

        public DataPackage_Code(IDataReader dataReader, string code, int startDate, int endDate, int minBefore, int minAfter)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.startDate = startDate;
            if (dataReader.TradingDayReader.IsTrade(startDate))
                this.startDate = startDate;
            else
                this.startDate = dataReader.TradingDayReader.GetNextTradingDay(startDate);
            this.endDate = endDate;

            this.minBefore = minBefore;
            this.minAfter = minAfter;

            this.allTradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
        }

        /// <summary>
        /// 得到股票或期货的ID
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
        }

        /// <summary>
        /// 得到开始日期
        /// </summary>
        public int StartDate { get { return startDate; } }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        public int EndDate { get { return endDate; } }

        public IList<int> GetTradingDays()
        {
            return allTradingDays;
        }

        public ITradingDayReader GetTradingDayReader()
        {
            if (tradingDayReader == null)
                tradingDayReader = new CacheUtils_TradingDay(allTradingDays);
            return tradingDayReader;
        }

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData_Extend GetKLineData(KLinePeriod period)
        {
            if (dic_Period_KLineData.ContainsKey(period))
            {
                return dic_Period_KLineData[period];
            }
            IKLineData_Extend klineData = dataReader.KLineDataReader.GetData_Extend(code, startDate, endDate, minBefore, minAfter, period);
            dic_Period_KLineData.Add(period, klineData);
            return klineData;
        }

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        public ITimeLineData GetTimeLineData(int date)
        {
            return dataReader.TimeLineDataReader.GetData(code, date);
        }

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        public ITickData_Extend GetTickData(int date)
        {
            return dataReader.TickDataReader.GetTickData_Extend(code, date);
        }

        //public ITradingSessionReader_Code GetTradingSessionReader()
        //{
        //    return dataReader.CreateTradingSessionReader(code);
        //}

        public ITradingTimeReader_Code GetTradingTimeReader()
        {
            return dataReader.CreateTradingTimeReader(code);
        }

        public float GetLastEndPrice(int date)
        {
            return dataReader.KLineDataReader.GetLastEndPrice(code, date);
        }

        public override string ToString()
        {
            return code + ":" + startDate + "-" + endDate + "|" + minBefore + "," + minAfter;
        }

        public IKLineData_RealTime CreateKLineData_RealTime(KLinePeriod period)
        {
            IKLineData_Extend klineData = GetKLineData(period);
            return new KLineDataExtend_RealTime(klineData);
        }

        public Dictionary<KLinePeriod, IKLineData_RealTime> CreateKLineData_RealTimes(IList<KLinePeriod> periods)
        {
            Dictionary<KLinePeriod, IKLineData_RealTime> dic = new Dictionary<KLinePeriod, IKLineData_RealTime>();
            for(int i = 0; i < periods.Count; i++)
            {
                KLinePeriod period = periods[i];
                dic.Add(period, CreateKLineData_RealTime(period));
            }
            return dic;
        }
    }
}