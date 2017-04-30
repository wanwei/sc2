using com.wer.sc.data.cache;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class RealTimeDataBuilder_DayDataCache
    {
        private IDataCache_Code cache_Code;

        private RealTimeDataBuilder_DayData realTimeDataBuilder;

        private SimpleDataCache<RealTimeDataBuilder_DayData> cache_RealTimeDataBuilder = new SimpleDataCache<RealTimeDataBuilder_DayData>();

        public RealTimeDataBuilder_DayDataCache(IDataCache_Code cache_Code)
        {
            this.cache_Code = cache_Code;
        }

        public RealTimeDataBuilder_DayData CurrentDayDataBuilder
        {
            get
            {
                return realTimeDataBuilder;
            }
        }

        public void ChangeTime(double time)
        {
            int openDate = cache_Code.GetOpenDate(time);
            RealTimeDataBuilder_DayData dataBuilder = cache_RealTimeDataBuilder.GetCache(openDate);
            if (dataBuilder == null)
            {
                IDataCache_CodeDate cache_CodeDate = cache_Code.GetCache_CodeDate(openDate);
                dataBuilder = new RealTimeDataBuilder_DayData(cache_CodeDate, time);
                cache_RealTimeDataBuilder.AddCache(openDate, dataBuilder);
            }
            else
                dataBuilder.ChangeTime(time);
            realTimeDataBuilder = dataBuilder;
        }

        public void Forward(KLinePeriod period, int len)
        {

        }

        public void ForwardTick(int len)
        {
            
        }
    }
}
