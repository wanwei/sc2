using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.transfer;
using com.wer.sc.data.update;
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
        private IKLineDataStore klineDataStore;

        private KLineDataReader_Extend dataReaderExtend;

        public KLineDataReader(IDataStore dataStore,IDataReader dataReader)
        {
            this.klineDataStore = dataStore.CreateKLineDataStore();
            this.dataReaderExtend = new KLineDataReader_Extend(dataReader);
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
            //return LoadKLineData(code, startDate, endDate, period);
            throw new ArgumentException("暂未实现");
        }

        //public IKLineData GetData(string code, int startDate, int endDate, KLinePeriod period, int beforeBarCount)
        //{
        //    //beforeBarCount

        //    return null;
        //}

        private IKLineData LoadKLineData(string code, int startDate, int endDate, KLinePeriod period)
        {
            KLineData klineData = klineDataStore.Load(code, startDate, endDate, period);
            if (klineData == null)
                return null;
            klineData.Code = code;
            klineData.Period = period;
            return klineData;
        }

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
    }
}
