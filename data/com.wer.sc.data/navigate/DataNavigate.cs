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
    /// <summary>
    /// 数据导航实现类
    /// </summary>
    public class DataNavigate : IDataNavigate
    {
        private DataReaderFactory fac;

        private string code;

        private double time;

        private ITimeLineData realData;

        //当前的k线数据
        private Dictionary<KLinePeriod, DataNavigate_KLine> dicNavigateKLine = new Dictionary<KLinePeriod, DataNavigate_KLine>();

        private SimpleDataCache<RealTimeDataBuilder_DayData> cache_DataBuilder_Day1Minute = new SimpleDataCache<RealTimeDataBuilder_DayData>();

        public DataNavigate(DataReaderFactory fac, string code, double time)
        {
            this.fac = fac;
            this.code = code;
            this.NavigateTo(time);
        }

        public void NavigateTo(double time)
        {
            this.time = time;
            /*
             * 导航后
             * 
             */
        }

        public void NavigateForward_Period(KLinePeriod period, int len)
        {

        }

        public void NavigateForward_Tick(int len)
        {
            throw new NotImplementedException();
        }

        public void NavigateForward_Time(KLinePeriod period, int len)
        {
            throw new NotImplementedException();
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            throw new NotImplementedException();
        }

        public ITimeLineData GetRealData()
        {
            throw new NotImplementedException();
        }

        public ITickData GetTickData()
        {
            throw new NotImplementedException();
        }        

        public event DataChangeEventHandler OnDataChangeHandler;
    }
}
