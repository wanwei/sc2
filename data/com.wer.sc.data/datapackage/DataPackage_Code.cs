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
using System.Xml;

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

        private ITradingTimeReader_Code tradingTimeReader;

        private int minBefore;

        private int minAfter;

        public DataPackage_Code(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public DataPackage_Code(IDataReader dataReader, string code, int startDate, int endDate, int minBefore, int minAfter)
        {
            this.dataReader = dataReader;
            Init(dataReader, code, startDate, endDate, minBefore, minAfter);
        }

        private void Init(IDataReader dataReader, string code, int startDate, int endDate, int minBefore, int minAfter)
        {
            this.code = code;
            if (dataReader.TradingDayReader.IsTrade(startDate))
                this.startDate = startDate;
            else
                this.startDate = dataReader.TradingDayReader.GetNextTradingDay(startDate);
            if (this.dataReader.TradingDayReader.IsTrade(endDate))
                this.endDate = endDate;
            else
                this.endDate = dataReader.TradingDayReader.GetPrevTradingDay(endDate);

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

        public int MinBefore { get { return minBefore; } }

        public int MinAfter { get { return minAfter; } }

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

        public ITradingTimeReader_Code GetTradingTimeReader()
        {
            if (this.tradingTimeReader == null)
                this.tradingTimeReader = dataReader.CreateTradingTimeReader(this.code);
            return this.tradingTimeReader;
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

        public IKLineData_Extend GetKLineData_Second(int date, KLinePeriod period)
        {
            if (dic_Period_SecondKLineData.ContainsKey(period))
            {
                KLineDataSecond klineData = dic_Period_SecondKLineData[period];
                if (date == klineData.Date)
                    return klineData.KlineData;
            }
            IKLineData_Extend klineData_Extend = dataReader.KLineDataReader.GetData_Extend(code, date, period);
            KLineDataSecond klineDataSecond = new KLineDataSecond();
            klineDataSecond.Date = date;
            klineDataSecond.KlineData = klineData_Extend;
            dic_Period_SecondKLineData[period] = klineDataSecond;
            return klineData_Extend;
        }

        private Dictionary<KLinePeriod, KLineDataSecond> dic_Period_SecondKLineData = new Dictionary<KLinePeriod, KLineDataSecond>();

        class KLineDataSecond
        {
            private int date;

            private IKLineData_Extend klineData;

            public int Date
            {
                get
                {
                    return date;
                }

                set
                {
                    date = value;
                }
            }

            public IKLineData_Extend KlineData
            {
                get
                {
                    return klineData;
                }

                set
                {
                    klineData = value;
                }
            }
        }

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        public ITimeLineData_Extend GetTimeLineData(int date)
        {
            return dataReader.TimeLineDataReader.GetData_Extend(code, date);
        }

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        public ITickData_Extend GetTickData(int date)
        {
            return dataReader.TickDataReader.GetTickData_Extend(code, date);
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

        public IKLineData_RealTime CreateKLineData_RealTime_Second(int date, KLinePeriod period)
        {
            IKLineData_Extend klineData = GetKLineData_Second(date, period);
            return new KLineDataExtend_RealTime(klineData);
        }

        public Dictionary<KLinePeriod, IKLineData_RealTime> CreateKLineData_RealTimes(IList<KLinePeriod> periods)
        {
            Dictionary<KLinePeriod, IKLineData_RealTime> dic = new Dictionary<KLinePeriod, IKLineData_RealTime>();
            for (int i = 0; i < periods.Count; i++)
            {
                KLinePeriod period = periods[i];
                dic.Add(period, CreateKLineData_RealTime(period));
            }
            return dic;
        }

        public ITimeLineData_RealTime CreateTimeLineData_RealTime(int date)
        {
            return new TimeLineDataExtend_RealTime(GetTimeLineData(date));
        }

        public ITickData_Extend CreateTickData_RealTime(int date)
        {
            return new TickData_RealTime(GetTickData(date));
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("code", code);
            xmlElem.SetAttribute("start", startDate.ToString());
            xmlElem.SetAttribute("end", endDate.ToString());
            xmlElem.SetAttribute("minBefore", minBefore.ToString());
            xmlElem.SetAttribute("minAfter", minAfter.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            string code = xmlElem.GetAttribute("code");
            int start = int.Parse(xmlElem.GetAttribute("start"));
            int end = int.Parse(xmlElem.GetAttribute("end"));
            int minBefore = int.Parse(xmlElem.GetAttribute("minBefore"));
            int minAfter = int.Parse(xmlElem.GetAttribute("minAfter"));
            this.Init(dataReader, code, start, end, minBefore, minAfter);
        }
    }
}