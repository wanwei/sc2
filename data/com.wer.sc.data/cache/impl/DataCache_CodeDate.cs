using com.wer.sc.data.reader;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DataCache_CodeDate : IDataCache_CodeDate
    {
        private DataReaderFactory dataReaderFactory;

        private string code;

        private int date;

        private ITickData tickData;

        private IKLineData minuteKLineData;

        private ITimeLineData realData;

        private float lastEndPrice;

        internal DataCache_CodeDate(DataReaderFactory dataReaderFactory, string code, int date, IKLineData minuteKLineData, float lastEndPrice)
        {
            this.dataReaderFactory = dataReaderFactory;
            this.code = code;
            this.date = date;
            this.minuteKLineData = minuteKLineData;
            this.lastEndPrice = lastEndPrice;
        }

        public String Code
        {
            get { return code; }
        }

        public int Date
        {
            get
            {
                return date;
            }
        }

        public DataReaderFactory DataReaderFactory
        {
            get
            {
                return dataReaderFactory;
            }
        }

        public IKLineData GetMinuteKLineData()
        {
            return minuteKLineData;
        }

        private Object lockObj_Tick = new object();


        public ITickData GetTickData()
        {
            if (tickData == null)
            {
                lock (lockObj_Tick)
                {
                    if (tickData == null)
                        tickData = dataReaderFactory.TickDataReader.GetTickData(code, date);
                }
            }
            return tickData;
        }

        private Object lockObj_Real = new object();

        public ITimeLineData GetRealData()
        {
            if (realData == null)
            {
                lock (lockObj_Real)
                {
                    if (realData == null)
                        realData = DataTransfer_KLine2TimeLine.ConvertTimeLineData(minuteKLineData, lastEndPrice);
                }
            }
            //TODO realdata是可以被修改的，所以此处线程不安全
            //应该给IRealData提供两个实现，一个是静态的，一个是动态的，缓存静态的实现，给外部生成时提供动态实现。
            return realData;
        }
    }
}
