using com.wer.sc.data.market.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    /// <summary>
    /// 单个股票或期货的接收器
    /// 该类实现
    /// </summary>
    public class RealTimeDataReceiver_Code : IRealTimeDataReader
    {
        private string code;

        private int date;

        private ITickData tickData;

        private TimeLineData_RealTime timeLineData;

        private Dictionary<KLinePeriod, KLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        public string GetCode()
        {
            return code;
        }

        public double Time
        {
            get
            {
                return tickData.Time;
            }
        }

        public RealTimeDataReceiver_Code(string code, int date, IDataReader dataReader, List<KLinePeriod> periods, List<double[]> openTime)
        {
            this.code = code;
            this.date = date;
            int lastOpenDate = dataReader.TradingDayReader.GetPrevTradingDay(date);
            RecentKLineDataLoader recentKLineDataLoader = new RecentKLineDataLoader(dataReader);
            for (int i = 0; i < periods.Count; i++)
            {
                KLinePeriod period = periods[i];
                this.dic_Period_KLineData.Add(period, new KLineData_RealTime(recentKLineDataLoader.GetRecentKLineData(code, lastOpenDate, period), KLineTimeListUtils.GetKLineTimeList(date, lastOpenDate, openTime, period), period));
            }

            List<double> timeList = KLineTimeListUtils.GetKLineTimeList(date, lastOpenDate, openTime, KLinePeriod.KLinePeriod_1Minute);
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, date, date, KLinePeriod.KLinePeriod_1Day);
            //this.timeLineData = new TimeLineData_RealTime(timeList);
        }

        public void Receive(ITickData tickData)
        {
            this.tickData = tickData;
            foreach (KLineData_RealTime klineData in this.dic_Period_KLineData.Values)
            {
                klineData.Receive(tickData);
            }
            //this.timeLineData.Receive(tickData);
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            KLineData_RealTime klineData;
            bool exist = dic_Period_KLineData.TryGetValue(period, out klineData);
            if (exist)
                return klineData;
            return null;
        }

        public ITickData GetTickData()
        {
            return tickData;
        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData;
        }
    }
}