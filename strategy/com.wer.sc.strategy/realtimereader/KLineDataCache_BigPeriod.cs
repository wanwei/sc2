using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    public class KLineDataCache_BigPeriod
    {
        private IDataReader dataReader;

        private string code;

        private int startDate;

        private int endDate;

        private int beforeBarCount;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private ITradingSessionReader_Instrument tradingSessionReader;

        public KLineDataCache_BigPeriod(IDataReader dataReader, string code, int startDate, int endDate, int beforeBarCount)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.startDate = dataReader.TradingDayReader.GetNextTradingDay(startDate);
            this.endDate = endDate;
            this.beforeBarCount = beforeBarCount;

            this.tradingSessionReader = dataReader.CreateTradingSessionReader(code);
        }

        /// <summary>
        /// 得到一张合约一段时间的K线数据
        /// 并且再获得startDate之前beforeBarCount个bar
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="period"></param>
        /// <param name="beforeBarCount"></param>
        /// <returns></returns>
        public IKLineData GetData(KLinePeriod period)
        {
            if (period.PeriodType == KLineTimeType.DAY)
                return GetData_Day(period);
            if (period.PeriodType == KLineTimeType.MINUTE)
                return GetData_Minute(period);
            //beforeBarCount / 50;
            //dataReader.TradingDayReader.
            return null;
        }

        private IKLineData GetData_Day(KLinePeriod period)
        {
            if (period.PeriodType != KLineTimeType.DAY)
                return null;

            int beforeStartDate = dataReader.TradingDayReader.GetPrevTradingDay(startDate, beforeBarCount);
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, beforeStartDate, endDate, period);
            KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
            int index = klineData_RealTime.IndexOfTime(startDate);
            klineData_RealTime.BarPos = index;
            return klineData_RealTime;
        }

        private IKLineData GetData_Minute(KLinePeriod period)
        {
            if (period.PeriodType != KLineTimeType.MINUTE)
                return null;

            int day = beforeBarCount * period.Period / 225;

            int beforeStartDate = dataReader.TradingDayReader.GetPrevTradingDay(startDate, day);
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, beforeStartDate, endDate, period);
            KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
            TradingSession session = tradingSessionReader.GetTradingSession(startDate);
            int index = klineData_RealTime.IndexOfTime(session.StartTime);
            klineData_RealTime.BarPos = index;

            return klineData_RealTime;
        }
    }
}
