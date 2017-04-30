using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate_KLine
    {
        private DataPathUtils utils;
        private Plugin_DataProvider dataProvider;

        public DataUpdate_KLine(Plugin_DataProvider dataProvider)
        {
            this.utils = new DataPathUtils(dataProvider.GetDataPath());
            this.dataProvider = dataProvider;
        }

        public void Update()
        {
            DataReaderFactory tmpFac = new DataReaderFactory(dataProvider.GetDataPath());
            List<InstrumentInfo> codes = tmpFac.CodeReader.GetAllCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                InstrumentInfo code = codes[i];
                UpdateCode(code.Code, tmpFac);
            }
        }

        public int GetUpdateFirstTime(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            if (!File.Exists(path))
                return -1;
            KLineDataStore store = new KLineDataStore(path);
            double time = store.GetFirstTime();
            return (int)time;
        }

        public int GetUpdateLastDate(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            return dataReaderFactory.KLineDataReader.GetLastDate(code, period);
        }

        public List<int> GetUpdateDates(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            List<int> openDates = dataProvider.GetOpenDates(code);
            if (openDates == null)
                return new List<int>();
            int lastDate = GetUpdateLastDate(code, dataReaderFactory, period);
            if (lastDate < 0)
                return openDates;
            int index = openDates.IndexOf(lastDate) + 1;
            return openDates.GetRange(index, openDates.Count - index);
        }

        public void UpdateCode(String code, DataReaderFactory dataReaderFactory)
        {
            //更新1分钟、15分钟、日线
            IKLineData data = Update(code, dataReaderFactory, new KLinePeriod(KLineTimeType.MINUTE, 1));
            if (data != null)
                UpdateOther(code, dataReaderFactory, data);
        }

        public IKLineData Update(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            if (period.PeriodType == KLineTimeType.MINUTE && period.Period == 1)
                return UpdateByTick(code, dataReaderFactory, period);
            return null;
        }

        private IKLineData UpdateByTick(string code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            int lastDate = (int)store.GetLastTime();
            List<int> openDates = dataProvider.GetOpenDates();
            int lastIndex;
            if (lastDate < 0)
                lastIndex = -1;
            else
                lastIndex = openDates.IndexOf(lastDate);

            float lastPrice = -1;
            List<IKLineData> klineDataList = new List<IKLineData>();
            for (int i = lastIndex + 1; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                TickData tickdata = dataReaderFactory.TickDataReader.GetTickData(code, openDate);
                if (tickdata != null)
                {
                    List<double[]> openTimes = dataProvider.GetOpenTime(code, openDate);
                    KLineData klineData = DataTransfer_Tick2KLine.Transfer(tickdata, period, openTimes, lastPrice);
                    klineDataList.Add(klineData);
                    lastPrice = klineData.arr_end[klineData.Length - 1];
                }
            }
            if (klineDataList.Count == 0)
                return null;
            IKLineData data = KLineData.Merge(klineDataList);
            store.Append(data);
            return data;
        }

        public IKLineData UpdateByTick(string code, DataReaderFactory dataReaderFactory, KLinePeriod period, List<int> dates)
        {
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);

            IKLineData data = GetKLineDataByTick(code, dataReaderFactory, period, dates);
            store.Append(data);
            return data;
        }

        public IKLineData GetKLineDataByTick(string code, DataReaderFactory dataReaderFactory, KLinePeriod period, IList<int> dates)
        {
            IKLineData lastKLineData = null;
            float lastPrice = -1;
            List<IKLineData> klineDataList = new List<IKLineData>();
            for (int i = 0; i < dates.Count; i++)
            {
                int openDate = dates[i];
                TickData tickdata = dataReaderFactory.TickDataReader.GetTickData(code, openDate);
                List<double[]> openTimes = dataProvider.GetOpenTime(code, openDate);
                KLineData klineData;
                if (tickdata != null)
                {
                    klineData = DataTransfer_Tick2KLine.Transfer(tickdata, period, openTimes, lastPrice);
                    klineDataList.Add(klineData);
                    lastPrice = klineData.arr_end[klineData.Length - 1];
                }
                else
                {
                    klineData = GetEmptyDayKLineData(code, openDate, openTimes, dataReaderFactory.OpenDateReader, lastKLineData);
                    klineDataList.Add(klineData);
                }
                lastKLineData = klineData;
            }
            if (klineDataList.Count == 0)
                return null;
            IKLineData data = KLineData.Merge(klineDataList);
            return data;
        }

        private KLineData GetEmptyDayKLineData(string code, int date, List<double[]> openTimes, ITradingDayReader openDateReader, IKLineData lastKLineData)
        {
            float lastPrice = lastKLineData.Arr_End[lastKLineData.Length - 1];
            int lastHold = lastKLineData.Arr_Hold[lastKLineData.Length - 1];
            List<double> openTimeList = OpenTimeUtils.GetKLineTimeList(date, openDateReader, openTimes, KLinePeriod.KLinePeriod_1Minute);
            KLineData klineData = new KLineData(openTimeList.Count);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.arr_time[i] = openTimeList[i];
                klineData.arr_start[i] = lastPrice;
                klineData.arr_high[i] = lastPrice;
                klineData.arr_low[i] = lastPrice;
                klineData.arr_end[i] = lastPrice;
                klineData.arr_mount[i] = 0;
                klineData.arr_money[i] = 0;
                klineData.arr_hold[i] = lastHold;
            }
            return klineData;
        }

        public IKLineData UpdateByKLine(String code, DataReaderFactory dataReaderFactory, KLinePeriod period, IKLineData originalData)
        {
            IKLineData data_Target = DataTransfer_KLine2KLine.Transfer(originalData, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
            return data_Target;
        }

        private void UpdateBy1Minute(String code, DataReaderFactory dataReaderFactory, KLinePeriod period)
        {
            int lastDate = dataReaderFactory.KLineDataReader.GetLastDate(code, period);
            IKLineData data = dataReaderFactory.KLineDataReader.GetData(code, lastDate + 1, int.MaxValue, period);
            IKLineData data_Target = DataTransfer_KLine2KLine.Transfer(data, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
        }

        private void UpdateOther(String code, DataReaderFactory tmpFac, IKLineData data)
        {
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLineTimeType.MINUTE, 15));
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLineTimeType.HOUR, 1));
            DoUpdate(code, tmpFac, data, new KLinePeriod(KLineTimeType.DAY, 1));
        }

        private void DoUpdate(String code, DataReaderFactory tmpFac, IKLineData data, KLinePeriod period)
        {
            //TODO 检查已有文件的时间
            IKLineData data_Target = DataTransfer_KLine2KLine.Transfer(data, period);
            String path = utils.GetKLineDataPath(code, period);
            KLineDataStore store = new KLineDataStore(path);
            store.Append(data_Target);
        }
    }
}