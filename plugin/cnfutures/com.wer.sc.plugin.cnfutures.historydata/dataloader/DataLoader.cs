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
        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        private DataLoader_Variety dataLoader_Variety;

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
            this.dataLoader_Variety = new DataLoader_Variety(pluginHelper.PluginDirPath);
            //this.dataLoader_Instrument = new CodeInfoGenerator(pluginHelper.PluginDirPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginHelper.PluginDirPath, dataLoader_Variety);
            this.plugin_HistoryData_CnFutures = new Plugin_HistoryData_CnFutures(pluginHelper);
        }

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CodeInfo GetInstrument(string code)
        {
            //return dataLoader_Instrument.GetInstrument(code);
            return null;
        }

        public List<CodeInfo> LoadNewInstruments()
        {
            //return dataLoader_Instrument.GetAllInstruments();
            return dataProvider.GetNewCodes();
        }

        public List<CodeInfo> LoadInstruments(string variety)
        {
            //return dataLoader_Instrument.GetInstruments(variety);
            return null;
        }

        public ITradingDayReader LoadTradingDayReader()
        {
            //return dataProvider.LoadTradingDayReader();
            return null;
        }

        private CodeInfo GetCodeInfo(String code)
        {
            return null;
        }

        public List<TradingSession> LoadTradingSessions(string code)
        {            
            List<TradingSession> dayStartTimes = this.LoadUpdatedTradingSessions(code);

            CodeInfo codeInfo = this.dataLoader_Instrument.GetInstrument(code);
            ITradingDayReader openDateReader = this.LoadTradingDayReader();
            int firstCodeTradingDayIndex;
            int lastCodeTradingDayIndex;
            int lastUpdateTradingDayIndex = GetLastUpdatedIndex(dayStartTimes);
            if (lastUpdateTradingDayIndex < 0)
            {
                firstCodeTradingDayIndex = GetFirstTradingDayIndex(codeInfo, openDateReader);
                lastCodeTradingDayIndex = GetLastTradingDayIndex(codeInfo, openDateReader);
            }
            else
            {
                lastCodeTradingDayIndex = lastUpdateTradingDayIndex + 1;
                if (lastCodeTradingDayIndex >= openDateReader.GetAllTradingDays().Count)
                    return null;
                firstCodeTradingDayIndex = GetFirstTradingDayIndex(codeInfo, openDateReader);
            }
            if (firstCodeTradingDayIndex < 0)
                return null;

            List<int> openDates = openDateReader.GetAllTradingDays();
            List<TradingSession> updateStartTimes = CalcDayOpenTime(code, openDates, firstCodeTradingDayIndex, lastCodeTradingDayIndex);

            List<TradingSession> result = new List<TradingSession>();
            if (dayStartTimes != null)
                result.AddRange(dayStartTimes);
            result.AddRange(updateStartTimes);
            return result;
        }

        private int GetLastUpdatedIndex(List<TradingSession> dayStartTimes)
        {
            if (dayStartTimes == null || dayStartTimes.Count == 0)
                return -1;
            int lastDate = dayStartTimes[dayStartTimes.Count - 1].TradingDay;
            ITradingDayReader openDateReader = this.LoadTradingDayReader();
            int lastIndex = openDateReader.GetTradingDayIndex(lastDate);
            return lastIndex;
        }

        private int GetFirstTradingDayIndex(CodeInfo codeInfo, ITradingDayReader openDateReader)
        {
            int codeFirstDate = codeInfo.Start;
            if (codeFirstDate < 0)
                return 0;
            int firstTradingDay = openDateReader.FirstTradingDay;
            firstTradingDay = codeFirstDate > firstTradingDay ? codeFirstDate : firstTradingDay;

            int index = openDateReader.GetTradingDayIndex(firstTradingDay);
            if (index >= 0)
                return index;
            int date = openDateReader.GetNextTradingDay(firstTradingDay);
            return openDateReader.GetTradingDayIndex(date);
        }

        private int GetLastTradingDayIndex(CodeInfo codeInfo, ITradingDayReader openDateReader)
        {
            int codeLastDate = codeInfo.End;
            if (codeLastDate < 0)
                return openDateReader.GetAllTradingDays().Count - 1;
            int lastTradingDay = openDateReader.LastTradingDay;
            lastTradingDay = codeLastDate > lastTradingDay ? lastTradingDay : codeLastDate;

            int index = openDateReader.GetTradingDayIndex(lastTradingDay);
            if (index >= 0)
                return index;
            int date = openDateReader.GetPrevTradingDay(lastTradingDay);
            return openDateReader.GetTradingDayIndex(date);
        }

        //private int GetCodeLastTradingDayIndex(string code)
        //{
        //    CodeInfo codeInfo = this.dataLoader_Instrument.GetInstrument(code);
        //    ITradingDayReader openDateReader = this.LoadTradingDayReader();
        //    int codeLastDate = codeInfo.End;
        //    int codeLastIndex = openDateReader.GetTradingDayIndex(codeLastDate);
        //    if (codeLastIndex < 0)
        //        return openDateReader.GetAllTradingDays().Count - 1;
        //    return openDateReader.GetTradingDayIndex(codeLastDate);
        //}

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