using com.wer.sc.data.cache;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate2 : IDataNavigate2
    {
        private DataReaderFactory factory;
        private int startDate;
        private int endDate;
        private string code;
        private double time;

        private Dictionary<KLinePeriod, DataNavigate_KLine> dicNavigateKLine = new Dictionary<KLinePeriod, DataNavigate_KLine>();

        private DataNavigate_Code currentNavigate_Code;

        private Dictionary<string, DataNavigate_Code> dicNavigate_Code = new Dictionary<string, DataNavigate_Code>();

        public DataNavigate2(DataReaderFactory factory, String code, double time)
        {
            DataCacheFactory cacheFactory = new DataCacheFactory(factory);
            IDataCache_Code cache = cacheFactory.CreateCache_Code(code);
            ITradingDayReader openDateReader = cache.GetOpenDateReader();
            Init(factory, code, time, openDateReader.FirstOpenDate, openDateReader.LastOpenDate);
        }

        public DataNavigate2(DataReaderFactory factory, String code, double time, int startDate, int endDate)
        {
            Init(factory, code, time, startDate, endDate);
        }

        private void Init(DataReaderFactory factory, string code, double time, int startDate, int endDate)
        {
            this.factory = factory;
            this.code = code;
            this.time = time;
            this.startDate = startDate;
            this.endDate = endDate;
            this.currentNavigate_Code = new DataNavigate_Code(factory, code, startDate, endDate, time);
            dicNavigate_Code.Add(code, currentNavigate_Code);
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public double CurrentTime
        {
            get
            {
                return time;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }
        }

        public event DataChangeEventHandler OnDataChangeHandler;

        public void ChangeData(string code, double time)
        {
            if (currentNavigate_Code == null || currentNavigate_Code.Code != code)
            {

            }
            //currentNavigate_Code = new DataNavigate_Code(fac,co)

            //System.Runtime.
            //ObjectCache
            //TODO 要新生成一个Navigate
        }

        public void ChangeTime(double time)
        {
            currentNavigate_Code.ChangeTime(time);
        }

        public void Forward(KLinePeriod period, int len)
        {
            currentNavigate_Code.Forward(period, len);
        }

        public void ForwardTick(int len)
        {
            currentNavigate_Code.ForwardTick(len);
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return currentNavigate_Code.GetKLineData(period);
        }

        public ITimeLineData GetRealData()
        {
            return currentNavigate_Code.GetRealData();
        }

        public ITickData GetTickData()
        {
            return currentNavigate_Code.GetTickData();
        }
    }
}