using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.historydata.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 该类帮助获取需要更新的Csv数据信息，包括类型和日期
    /// </summary>
    public class DataUpdateUtils
    {
        private List<CodeInfo> codes;

        private DataLoader_InstrumentInfo dataLoader_InstrumentInfo;

        private List<int> openDates;

        private WaitForUpdateDateGetter updateDateGetter;

        public DataUpdateUtils(string dataPath, ITradingDayReader tradingDayReader, DataLoader_InstrumentInfo dataLoader_InstrumentInfo, IUpdatedDataInfo openDateReader_HistoryData)
        {
            this.dataLoader_InstrumentInfo = dataLoader_InstrumentInfo;
            this.codes = dataLoader_InstrumentInfo.GetAllInstruments();
            this.openDates = tradingDayReader.GetAllTradingDays();
            this.updateDateGetter = new WaitForUpdateDateGetter(openDateReader_HistoryData, tradingDayReader, dataLoader_InstrumentInfo);
        }

        /// <summary>
        /// 得到还需要更新的Tick数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="isFillUp">是否将所有缺的数据全部补上，如果isFillUp为false，那么会从现在的历史数据中最新的数据开始更新，否则会将会补全所有数据</param>
        /// <returns></returns>
        public List<InstrumentDatesInfo> GetTickNewData(bool isFillUp)
        {
            List<InstrumentDatesInfo> newDataList = new List<InstrumentDatesInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                InstrumentDatesInfo info = new InstrumentDatesInfo();
                info.instrument = codes[i].Code;
                if (isFillUp)
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_TickData_FillUp(codes[i].Code);
                else
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_TickData(codes[i].Code);
                newDataList.Add(info);
            }
            return newDataList;
        }

        /// <summary>
        /// 得到还需要更新的K线数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="period"></param>
        /// <param name="isFillUp"></param>
        /// <returns></returns>
        public List<InstrumentDatesInfo> GetKLineNewData(KLinePeriod period, bool isFillUp)
        {
            List<InstrumentDatesInfo> newDataList = new List<InstrumentDatesInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                InstrumentDatesInfo info = new InstrumentDatesInfo();
                info.instrument = codes[i].Code;
                if (isFillUp)
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_KLineData_FillUp(codes[i].Code, period);
                else
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_KLineData(codes[i].Code, period);
                newDataList.Add(info);
            }
            return newDataList;
        }
    }

    /// <summary>
    /// 该类根据现在已经更新的数据得到需要更新的Tick和KLine的日期
    /// </summary>
    class WaitForUpdateDateGetter
    {
        private IUpdatedDataInfo historyDataInfoLoader;

        //private List<int> openDates;

        private DataLoader_InstrumentInfo dataLoader_InstrumentInfo;

        private ITradingDayReader tradingDayReader;

        public WaitForUpdateDateGetter(IUpdatedDataInfo historyDataInfoLoader, ITradingDayReader tradingDayReader, DataLoader_InstrumentInfo dataLoader_InstrumentInfo)
        {
            //this.historyDataInfoLoader = new HistoryDataInfoLoader(srcDataPath);
            this.historyDataInfoLoader = historyDataInfoLoader;
            this.tradingDayReader = tradingDayReader;
            this.dataLoader_InstrumentInfo = dataLoader_InstrumentInfo;
        }

        public List<int> GetWaitForUpdateOpenDates_TickData(string code)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_TickData(code);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return GetCodeOpenDates(code);
            TradingDayCache cache = new TradingDayCache(codeOpenDates);
            codeOpenDates.Sort();
            int lastUpdatedOpenDate = codeOpenDates[codeOpenDates.Count - 1];

            return GetWaitForUpdateOpenDates(GetCodeOpenDates(code), cache);
        }

        private List<int> GetCodeOpenDates(string code)
        {
            CodeInfo codeInfo = this.dataLoader_InstrumentInfo.GetInstrument(code);
            List<int> list = new List<int>();
            list.AddRange(tradingDayReader.GetTradingDays(codeInfo.Start, codeInfo.End));
            return list;
        }

        public List<int> GetWaitForUpdateOpenDates_TickData_FillUp(string code)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_TickData(code);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return GetCodeOpenDates(code);
            TradingDayCache cache = new TradingDayCache(codeOpenDates);
            return GetWaitForUpdateOpenDates_FillUp(GetCodeOpenDates(code), cache);
        }

        public List<int> GetWaitForUpdateOpenDates_KLineData(string code, KLinePeriod period)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_KLineData(code, period);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return GetCodeOpenDates(code);
            TradingDayCache cache = new TradingDayCache(codeOpenDates);
            return GetWaitForUpdateOpenDates(GetCodeOpenDates(code), cache);
        }

        public List<int> GetWaitForUpdateOpenDates_KLineData_FillUp(string code, KLinePeriod period)
        {
            List<int> codeOpenDates = this.historyDataInfoLoader.GetOpenDates_KLineData(code, period);
            if (codeOpenDates == null || codeOpenDates.Count == 0)
                return GetCodeOpenDates(code);
            TradingDayCache cache = new TradingDayCache(codeOpenDates);
            return GetWaitForUpdateOpenDates_FillUp(GetCodeOpenDates(code), cache);
        }

        private static List<int> GetWaitForUpdateOpenDates(List<int> allOpenDates, TradingDayCache currentOpenDateCache)
        {
            if (currentOpenDateCache.GetAllTradingDays().Count == 0)
                return allOpenDates;
            int date = currentOpenDateCache.LastTradingDay;
            int index = allOpenDates.IndexOf(date);
            if (index == allOpenDates.Count - 1)
                return new List<int>();
            int startIndex = index + 1;
            return allOpenDates.GetRange(startIndex, allOpenDates.Count - startIndex);
        }

        private static List<int> GetWaitForUpdateOpenDates_FillUp(List<int> allOpenDates, TradingDayCache currentOpenDateCache)
        {
            List<int> waitForUpdateOpenDates = new List<int>();
            for (int i = 0; i < allOpenDates.Count; i++)
            {
                int openDate = allOpenDates[i];
                if (!currentOpenDateCache.IsTrade(openDate))
                {
                    waitForUpdateOpenDates.Add(openDate);
                }
            }
            return waitForUpdateOpenDates;
        }
    }
}