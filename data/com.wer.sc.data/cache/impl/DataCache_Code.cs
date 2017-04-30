using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DataCache_Code : IDataCache_Code
    {
        private DataReaderFactory dataReaderFactory;

        private string code;

        private int startDate;

        private int endDate;

        private TradingDayCache openDateCache;

        private MinuteKLineData_DateGetter minuteDataGetter;

        private IKLineData dayKLineData;

        private List<int> cachedKeies = new List<int>();

        private Dictionary<int, DataCache_CodeDate> dicDateCache = new Dictionary<int, DataCache_CodeDate>();

        private int maxCacheDateCount = 20;

        internal DataCache_Code(DataReaderFactory dataReaderFactory, string code)
        {
            IKLineDataReader dataReader = dataReaderFactory.KLineDataReader;
            KLinePeriod period = new KLinePeriod(KLineTimeType.DAY, 1);
            int start = dataReader.GetFirstDate(code, period);
            int end = dataReader.GetLastDate(code, period);
            Init(dataReaderFactory, code, start, end);
        }

        internal DataCache_Code(DataReaderFactory dataReaderFactory, String code, int startDate, int endDate)
        {
            Init(dataReaderFactory, code, startDate, endDate);
        }

        private void Init(DataReaderFactory dataReaderFactory, string code, int startDate, int endDate)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.dataReaderFactory = dataReaderFactory;

            IKLineData minuteKLineData = dataReaderFactory.KLineDataReader.GetData(code, startDate, endDate, new KLinePeriod(KLineTimeType.MINUTE, 1));
            this.minuteDataGetter = new MinuteKLineData_DateGetter(minuteKLineData, dataReaderFactory.OpenDateReader);
            this.openDateCache = new TradingDayCache(this.minuteDataGetter.GetOpenDates());
            this.dayKLineData = dataReaderFactory.KLineDataReader.GetData(code, startDate, endDate, new KLinePeriod(KLineTimeType.DAY, 1));
        }

        public string Code
        {
            get { return code; }
        }

        public int StartDate
        {
            get { return startDate; }
        }

        public int EndDate
        {
            get { return endDate; }
        }

        public ITradingDayReader GetOpenDateReader()
        {
            return openDateCache;
        }

        public int GetOpenDate(double time)
        {
            int date = (int)time;
            if (!DaySplitter.IsNight(time))
                return date;

            return openDateCache.GetNextOpenDate(date);
        }

        public int MaxCacheDateCount
        {
            get
            {
                return maxCacheDateCount;
            }

            set
            {
                maxCacheDateCount = value;
            }
        }

        public IKLineData GetDayKLineData()
        {
            return dayKLineData;
        }

        private Object lockObj = new object();

        public IDataCache_CodeDate GetCache_CodeDate(int date)
        {
            if (dicDateCache.ContainsKey(date))
                return dicDateCache[date];
            lock (lockObj)
            {
                if (dicDateCache.ContainsKey(date))
                    return dicDateCache[date];

                IKLineData klineData = minuteDataGetter.GetKLineData(date);
                DataCache_CodeDate cache = new DataCache_CodeDate(dataReaderFactory, code, date, klineData, minuteDataGetter.LastEndPrice(date));
                if (cachedKeies.Count > maxCacheDateCount)
                {
                    int removedKey = cachedKeies[0];
                    cachedKeies.RemoveAt(0);
                    dicDateCache.Remove(removedKey);
                }
                cachedKeies.Add(date);
                dicDateCache.Add(date, cache);
                return cache;
            }
        }

        public IDataCache_CodeDate GetCache_CodeDate(double time)
        {
            int date = GetOpenDate(time);
            return GetCache_CodeDate(date);
        }
    }

    public class MinuteKLineData_DateGetter
    {
        private DayMinuteKLineDataGetter dataGetter;

        private Dictionary<int, IKLineData> dicDateKLineData = new Dictionary<int, IKLineData>();

        public MinuteKLineData_DateGetter(IKLineData klineData, ITradingDayReader openDateReader)
        {
            dataGetter = new DayMinuteKLineDataGetter(klineData, openDateReader);
        }

        public IKLineData GetKLineData(int date)
        {
            if (dicDateKLineData.ContainsKey(date))
                return dicDateKLineData[date];
            IKLineData klineData = dataGetter.GetMinuteKLineData(date);
            if (klineData == null)
                return null;
            dicDateKLineData.Add(date, klineData);
            return klineData;
        }

        public float LastEndPrice(int date)
        {
            return dataGetter.LastEndPrice(date);
        }

        public List<int> GetOpenDates()
        {
            return dataGetter.OpenDates;
        }
    }
}
