using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.transfer;
using com.wer.sc.data.update;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class KLineDataReader : IKLineDataReader
    {
        private IDataReader dataReader;

        private IKLineDataStore klineDataStore;

        private KLineDataReader_Extend dataReaderExtend;

        public KLineDataReader(IDataStore dataStore, IDataReader dataReader)
        {
            this.dataReader = dataReader;
            this.klineDataStore = dataStore.CreateKLineDataStore();
            this.dataReaderExtend = new KLineDataReader_Extend(dataReader, klineDataStore);
        }

        public IKLineData GetAllData(string code, KLinePeriod period)
        {
            KLineData klineData = klineDataStore.LoadAll(code, period);
            if (klineData == null)
                return null;
            klineData.Code = code;
            klineData.Period = period;
            return klineData;
        }

        public IKLineData GetData(string code, int date, KLinePeriod period)
        {
            return GetData(code, date, date, period);
        }

        public IKLineData GetData(String code, int startDate, int endDate, KLinePeriod period)
        {
            if (period.PeriodType == KLineTimeType.MINUTE)
            {
                if (period.Period == 1 || period.Period == 15)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLineTimeType.MINUTE, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period, null);
            }
            if (period.PeriodType == KLineTimeType.HOUR)
            {
                if (period.Period == 1)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLineTimeType.HOUR, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period, null);
            }
            if (period.PeriodType == KLineTimeType.DAY)
            {
                if (period.Period == 1)
                    return LoadKLineData(code, startDate, endDate, period);
                IKLineData data = LoadKLineData(code, startDate, endDate, new KLinePeriod(KLineTimeType.DAY, 1));
                return DataTransfer_KLine2KLine.Transfer(data, period, null);
            }
            if (period.PeriodType == KLineTimeType.SECOND)
            {
                //return LoadKLineData_Second(code, startDate, endDate, period);
                return dataReaderExtend.GetKLineData_Second(code, startDate, endDate, period);
            }
            //return LoadKLineData(code, startDate, endDate, period);
            throw new ArgumentException("暂未实现");
        }

        public IKLineData_Extend GetData_Extend(string code, int date, KLinePeriod period)
        {
            return GetData_Extend(code, date, date, period);
        }

        public IKLineData_Extend GetData_Extend(string code, int startDate, int endDate, KLinePeriod period)
        {
            if (period.PeriodType == KLineTimeType.SECOND)
                return dataReaderExtend.GetKLineDataExtend_Second(code, startDate, endDate, period);
            return GetData_Extend(code, startDate, endDate, 0, 0, period);
        }

        public IKLineData_Extend GetData_Extend(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period)
        {
            return dataReaderExtend.GetData_Extend(code, startDate, endDate, minBeforeBarCount, minAfterBarCount, period);
        }

        private IKLineData LoadKLineData(string code, int startDate, int endDate, KLinePeriod period)
        {
            KLineData klineData = klineDataStore.Load(code, startDate, endDate, period);
            if (klineData == null)
                return null;
            klineData.Code = code;
            klineData.Period = period;
            return klineData;
        }

        //private IKLineData LoadKLineData_Second(string code, int startDate, int endDate, KLinePeriod period)
        //{
        //    IList<int> dates = this.dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
        //    List<IKLineData> klineDataList = new List<IKLineData>();
        //    for (int i = 0; i < dates.Count; i++)
        //    {
        //        int date = dates[i];
        //        IKLineData klineData = LoadKLineData_Second(code, date, period);
        //        if (klineData != null)
        //            klineDataList.Add(klineData);
        //    }
        //    return KLineData.Merge(klineDataList);
        //}

        //private IKLineData LoadKLineData_Second(string code, int date, KLinePeriod period)
        //{
        //    TickData tickData = dataReader.TickDataReader.GetTickData(code, date);
        //    if (tickData == null)
        //        return null;
        //    float lastEndPrice = dataReaderExtend.GetLastEndPrice(code, date);
        //    int lastEndHold = dataReaderExtend.GetLastEndHold(code, date);
        //    IList<double[]> tradingPeriods = dataReader.CreateTradingTimeReader(code).GetTradingTime(date).TradingPeriods;
        //    return DataTransfer_Tick2KLine.Transfer(tickData, tradingPeriods, period, lastEndPrice, lastEndHold);
        //}

        public int GetFirstDate(String code, KLinePeriod period)
        {
            return klineDataStore.GetFirstTradingDay(code, period);
        }

        public int GetLastDate(String code, KLinePeriod period)
        {
            return klineDataStore.GetLastTradingDay(code, period);
        }

        public IKLineData GetData(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period)
        {
            return dataReaderExtend.GetData(code, startDate, endDate, minBeforeBarCount, minAfterBarCount, period);
        }

        public float GetLastEndPrice(string code, int date)
        {
            return dataReaderExtend.GetLastEndPrice(code, date);
        }

        public KLineDataTimeInfo GetKLineDataTimeInfo(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            return dataReaderExtend.GetKLineDataTimeInfo(code, startDate, endDate, klinePeriod);
        }
    }
}
