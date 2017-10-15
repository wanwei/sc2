using com.wer.sc.data.store;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 扩展实现
    /// </summary>
    public class KLineDataReader_Extend
    {
        private IDataReader dataReader;

        private IKLineDataStore klineDataStore;

        public KLineDataReader_Extend(IDataReader dataReader, IKLineDataStore klineDataStore)
        {
            this.dataReader = dataReader;
            this.klineDataStore = klineDataStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="minBeforeBarCount"></param>
        /// <param name="minAfterBarCount"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData GetData(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period)
        {
            int realStart;
            int realEnd;
            return GetDataInternal(code, startDate, endDate, minBeforeBarCount, minAfterBarCount, period, out realStart, out realEnd);
        }

        private IKLineData GetDataInternal(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period, out int realStartDate, out int realEndDate)
        {
            int realStartDateIndex = 0;
            int realEndDateIndex = 0;
            if (period.PeriodType == KLineTimeType.WEEK)
            {
                realStartDate = startDate;
                realEndDate = endDate;
                return null;
            }
            else if (period.PeriodType == KLineTimeType.DAY)
            {
                int beforeDays = period.Period * minBeforeBarCount;
                realStartDateIndex = GetRealStartDateIndex(startDate, beforeDays);

                int afterDays = period.Period * minAfterBarCount;
                realEndDateIndex = GetRealEndDateIndex(endDate, afterDays);
            }
            else if (period.PeriodType == KLineTimeType.HOUR)
            {
                //一天按4个小时算
                int beforeDays = minBeforeBarCount == 0 ? 0 : (period.Period * minBeforeBarCount / 4 + 1);
                realStartDateIndex = GetRealStartDateIndex(startDate, beforeDays);
                int afterDays = minAfterBarCount == 0 ? 0 : period.Period * minAfterBarCount / 4 + 1;
                realEndDateIndex = GetRealEndDateIndex(endDate, afterDays);
            }
            else if (period.PeriodType == KLineTimeType.MINUTE)
            {
                //一天按240分钟算
                int beforeDays = minBeforeBarCount == 0 ? 0 : (period.Period * minBeforeBarCount / 240 + 1);
                realStartDateIndex = GetRealStartDateIndex(startDate, beforeDays);
                int afterDays = minAfterBarCount == 0 ? 0 : period.Period * minAfterBarCount / 240 + 1;
                realEndDateIndex = GetRealEndDateIndex(endDate, afterDays);
            }
            else if (period.PeriodType == KLineTimeType.SECOND)
            {
                realStartDate = startDate;
                realEndDate = endDate;
                return null;
            }
            realStartDate = dataReader.TradingDayReader.GetTradingDay(realStartDateIndex);
            realEndDate = dataReader.TradingDayReader.GetTradingDay(realEndDateIndex);
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, realStartDate, realEndDate, period);
            return klineData;
        }

        private int GetRealEndDateIndex(int endDate, int afterDays)
        {
            int realEndDateIndex;
            int endDateIndex = dataReader.TradingDayReader.GetTradingDayIndex(endDate, true);
            realEndDateIndex = endDateIndex + afterDays;
            int allDayCount = dataReader.TradingDayReader.GetAllTradingDays().Count;
            realEndDateIndex = realEndDateIndex >= allDayCount ? allDayCount - 1 : realEndDateIndex;
            return realEndDateIndex;
        }

        private int GetRealStartDateIndex(int startDate, int beforeDays)
        {
            int realStartDateIndex;
            int startDateIndex = dataReader.TradingDayReader.GetTradingDayIndex(startDate, false);
            realStartDateIndex = startDateIndex - beforeDays;
            realStartDateIndex = realStartDateIndex < 0 ? 0 : realStartDateIndex;
            return realStartDateIndex;
        }

        public IKLineData GetData_Second(string code, int date, KLinePeriod period)
        {
            ITickData tickData = dataReader.TickDataReader.GetTickData(code, date);
            //DataTransfer_Tick2KLine.Transfer()
            ITradingDayReader openDateReader = dataReader.TradingDayReader;
            //ITradingTimeReader openTimeReader = dataReader.CreateTradingSessionReader(code);
            // timeListGetter = new KLineTimeListGetter(openDateReader, openTimeReader);
            //List<double> klineTimeList = klineTimeListGetter.GetKLineTimeList(code, date, period);
            return null;
        }

        public float GetLastEndPrice(string code, int date)
        {
            int lastTradingDay = dataReader.TradingDayReader.GetPrevTradingDay(date);
            if (lastTradingDay < 0)
            {
                if (dataReader.TradingDayReader.IsTrade(date))
                {
                    IKLineData klineData = dataReader.KLineDataReader.GetData(code, date, date, KLinePeriod.KLinePeriod_1Day);
                    return klineData.Start;
                }
                else
                    return -1;
            }

            IKLineData lastDayklineData = dataReader.KLineDataReader.GetData(code, lastTradingDay, lastTradingDay, KLinePeriod.KLinePeriod_1Day);
            if (lastDayklineData == null)
                return -1;
            return lastDayklineData.End;
        }

        public KLineDataTimeInfo GetKLineDataTimeInfo(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            ITradingTimeReader_Code tradingTimeReader_Code = this.dataReader.CreateTradingTimeReader(code);
            List<int> allTradingDays = klineDataStore.GetAllTradingDay(code, klinePeriod);
            CacheUtils_TradingDay cache = new CacheUtils_TradingDay(allTradingDays);
            IList<int> tradingDays = cache.GetTradingDays(startDate, endDate);

            IList<double[]>[] periodArr = new List<double[]>[tradingDays.Count];
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                TradingTime time = tradingTimeReader_Code.GetTradingTime(tradingDay);
                List<double[]> periods = time.TradingPeriods;
                periodArr[i] = periods;
            }
            return TradingTimeUtils.GetKLineDataTimeInfo(periodArr, klinePeriod);
        }

        public IKLineData_Extend GetData_Extend(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period)
        {
            int realStartDate;
            int realEndDate;
            IKLineData klineData = GetDataInternal(code, startDate, endDate, minBeforeBarCount, minAfterBarCount, period, out realStartDate, out realEndDate);
            KLineDataTimeInfo timeInfo = GetKLineDataTimeInfo(code, realStartDate, realEndDate, period);
            int startPos = timeInfo.GetDayStartPos(startDate);
            klineData.BarPos = startPos;
            return new KLineData_Extend(klineData, timeInfo);
        }
    }
}
