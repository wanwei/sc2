using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;
using com.wer.sc.data.utils;
using com.wer.sc.data.transfer;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    /// <summary>
    /// 数据装载的抽象实现
    /// </summary>
    public abstract class DataLoader_Abstract : IDataLoader
    {
        protected DataLoader_InstrumentInfo dataLoader_Instrument;

        public DataLoader_InstrumentInfo DataLoader_InstrumentInfo
        {
            get { return dataLoader_Instrument; }
        }

        protected DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        protected Plugin_HistoryData_CnFutures plugin_HistoryData_CnFutures;

        //private string csvDataPath;

        protected string srcDataPath;

        public DataLoader_Abstract(PluginHelper pluginHelper, string srcDataPath)
        {
            this.srcDataPath = srcDataPath;
            this.dataLoader_Instrument = new DataLoader_InstrumentInfo(pluginHelper.PluginDirPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginHelper.PluginDirPath, this.dataLoader_Instrument);
            this.plugin_HistoryData_CnFutures = new Plugin_HistoryData_CnFutures(new PluginHelper(pluginHelper.PluginInfo));
        }

        //public string CsvDataPath
        //{
        //    get
        //    {
        //        return csvDataPath;
        //    }
        //}

        public List<CodeInfo> LoadAllInstruments()
        {
            return dataLoader_Instrument.GetAllInstruments();
        }

        public List<CodeInfo> LoadInstruments(string variety)
        {
            return dataLoader_Instrument.GetInstruments(variety);
        }

        public List<double[]> LoadTradingSessionDetail(string code, int date)
        {
            return dataLoader_TradingSessionDetail.GetTradingTime(code, date);
        }

        public ITradingTimeReader LoadTradingSessionDetailReader()
        {
            return dataLoader_TradingSessionDetail;
        }

        public List<TradingSession> LoadTradingSessions(string code)
        {
            List<TradingSession> dayStartTimes = this.LoadUpdatedTradingSessions(code);

            ITradingDayReader openDateReader = this.LoadTradingDayReader();
            int firstIndex = 0;
            if (dayStartTimes != null && dayStartTimes.Count != 0)
            {
                int lastDate = dayStartTimes[dayStartTimes.Count - 1].TradingDay;
                if (lastDate == openDateReader.LastTradingDay)
                    return null;
                int lastIndex = openDateReader.GetTradingDayIndex(lastDate);
                firstIndex = lastIndex + 1;
            }
            List<int> openDates = openDateReader.GetAllTradingDays();
            List<TradingSession> updateStartTimes = CalcDayOpenTime(code, openDates, firstIndex, openDates.Count - 1);

            List<TradingSession> result = new List<TradingSession>();
            if (dayStartTimes != null)
                result.AddRange(dayStartTimes);
            result.AddRange(updateStartTimes);
            return result;
        }

        private List<TradingSession> CalcDayOpenTime(string code, List<int> openDates, int startIndex, int endIndex)
        {
            List<TradingSession> dayStartTimes = new List<TradingSession>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                int date = openDates[i];

                List<double[]> openTime = this.LoadTradingSessionDetail(code, date);
                double startTime = openTime[0][0];
                double endTime = openTime[openTime.Count - 1][1];
                if (startTime > 0.18)
                {
                    if (i == 0)
                        throw new ArgumentException("传入的" + date + "有夜盘，必须传入其之前的日期");
                    dayStartTimes.Add(new TradingSession(date, openDates[i - 1] + startTime, openDates[i] + endTime));
                }
                else
                {
                    dayStartTimes.Add(new TradingSession(date, date + startTime, date + endTime));
                }
            }
            return dayStartTimes;
        }

        public IKLineData LoadKLineData(string code, int date, KLinePeriod klinePeriod, float lastEndPrice, int lastEndHold)
        {
            KLineTimeListGetter timeListGetter = new KLineTimeListGetter(LoadTradingDayReader(), this.dataLoader_TradingSessionDetail);
            ITickData tickData = this.LoadUpdatedTickData(code, date);
            /*
             * 此处不处理tickData为空的情况
             * 在DataTransfer_Tick2KLine.Transfer里处理tickData为空的情况
             */
            List<double> klineTimes = timeListGetter.GetKLineTimeList(code, date, klinePeriod);
            IKLineData klineData = DataTransfer_Tick2KLine.Transfer(tickData, klineTimes, lastEndPrice, lastEndHold);
            return klineData;
        }

        public IKLineData LoadUpdatedKLineData(string code, int date, KLinePeriod period)
        {
            return plugin_HistoryData_CnFutures.GetKLineData(code, date, period);
        }

        public ITickData LoadUpdatedTickData(string code, int date)
        {
            return plugin_HistoryData_CnFutures.GetTickData(code, date);
        }

        public List<TradingSession> LoadUpdatedTradingSessions(string code)
        {
            return plugin_HistoryData_CnFutures.GetTradingSessions(code);
        }

        /// <summary>
        /// 装载tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract ITickData LoadTickData(string code, int date);

        /// <summary>
        /// 得到所有交易日数据
        /// </summary>
        /// <returns></returns>
        public abstract ITradingDayReader LoadTradingDayReader();
    }
}