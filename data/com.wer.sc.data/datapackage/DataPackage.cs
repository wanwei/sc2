using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class DataPackage : IDataPackage
    {
        private IDataReader dataReader;

        private string code;

        private int startDate;

        private int endDate;

        private IList<int> allTradingDays;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private int minBefore;

        private int minAfter;

        public DataPackage(IDataReader dataReader, string code, int startDate, int endDate, int minBefore, int minAfter)
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

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (dic_Period_KLineData.ContainsKey(period))
            {
                return dic_Period_KLineData[period];
            }
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, startDate, endDate, minBefore, minAfter, period);
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
        public ITickData GetTickData(int date)
        {
            return dataReader.TickDataReader.GetTickData(code, date);
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
    }
}