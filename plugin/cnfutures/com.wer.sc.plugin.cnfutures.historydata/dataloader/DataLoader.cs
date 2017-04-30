using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.data.utils;
using com.wer.sc.data.transfer;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    /// <summary>
    /// 数据装载器
    /// 该类用于数据更新时需要的数据
    /// 可以装载原始数据及已经更新好的数据
    /// 
    /// 该类装载的数据：
    /// 1.tradingday，交易日，从原始的csv数据装载，原始数据存放是按照交易日分类的，所以装载目录即可
    /// 2.instrumentinfo，从配置文件装载，\plugin\cnfutures\generator
    /// 3.tradingsession，从项目的资源文件装载
    /// 4.tickdata，从原始数据装载
    /// 
    /// </summary>
    public class DataLoader : IDataLoader
    {
        private DataLoader_InstrumentInfo dataLoader_Instrument;

        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        private Plugin_HistoryData_CnFutures plugin_HistoryData_CnFutures;

        private IDataProvider dataProvider;

        private string srcDataPath;

        private string targetDataPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pluginHelper">插件帮助类，提供插件相关数据</param>
        /// <param name="dataProvider">数据提供者</param>
        /// <param name="srcDataPath">源数据目录</param>
        /// <param name="targetDataPath">目标目录</param>
        public DataLoader(PluginHelper pluginHelper, IDataProvider dataProvider, String srcDataPath, string targetDataPath)
        {
            this.dataProvider = dataProvider;
            this.srcDataPath = srcDataPath;
            this.targetDataPath = targetDataPath;
            this.dataLoader_Instrument = new DataLoader_InstrumentInfo(pluginHelper.PluginDirPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginHelper.PluginDirPath, dataLoader_Instrument);
            this.plugin_HistoryData_CnFutures = new Plugin_HistoryData_CnFutures(pluginHelper);
        }

        public List<CodeInfo> LoadAllInstruments()
        {
            return dataLoader_Instrument.GetAllInstruments();
        }

        public List<CodeInfo> LoadInstruments(string variety)
        {
            return dataLoader_Instrument.GetInstruments(variety);
        }

        public ITradingDayReader LoadTradingDayReader()
        {
            return dataProvider.LoadTradingDayReader();
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

        public List<double[]> LoadTradingSessionDetail(string code, int date)
        {
            return dataLoader_TradingSessionDetail.GetTradingTime(code, date);
        }

        public ITradingTimeReader LoadTradingSessionDetailReader()
        {
            return dataLoader_TradingSessionDetail;
        }

        public ITickData LoadTickData(string code, int date)
        {
            return dataProvider.LoadTickData(code, date);
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

        /// <summary>
        /// 目标目录
        /// </summary>
        /// <returns></returns>
        public String GetTargetDataPath()
        {
            return targetDataPath;
        }

        public ITickData LoadUpdatedTickData(string code, int date)
        {
            return this.plugin_HistoryData_CnFutures.GetTickData(code, date);
        }

        public IKLineData LoadUpdatedKLineData(string code, int date, KLinePeriod period)
        {
            return this.plugin_HistoryData_CnFutures.GetKLineData(code, date, period);
        }

        public List<TradingSession> LoadUpdatedTradingSessions(string code)
        {
            return this.plugin_HistoryData_CnFutures.GetTradingSessions(code);
        }
    }
}