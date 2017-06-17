using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using System;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.reader.cache;
using com.wer.sc.plugin.cnfutures.historydata.dataupdater.generator;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 数据更新帮助类
    /// </summary>
    public class DataUpdateHelper
    {
        private string updatedDataPath;

        private UpdatedDataLoader updatedDataLoader;

        private IDataProvider dataProvider;

        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        private DataLoader_Variety dataLoader_Variety;

        private DataGenerator_TickData_Main generator_Main;

        private DataGenerator_TickData_Index generator_Index;

        /// <summary>
        /// 创建数据更新帮助类
        /// </summary>
        /// <param name="updatedDataLoader">已更新数据读取器</param>
        /// <param name="dataProvider">新数据提供器</param>
        public DataUpdateHelper(string pluginPath, UpdatedDataLoader updatedDataLoader, IDataProvider dataProvider)
        {
            this.updatedDataPath = updatedDataLoader.GetUpdatedDataPath();
            this.updatedDataLoader = updatedDataLoader;
            this.dataProvider = dataProvider;

            DataLoader_Variety dataLoader_Variety = new DataLoader_Variety(pluginPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginPath, dataLoader_Variety);
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
            this.generator_Main = new DataGenerator_TickData_Main(this);
            this.generator_Index = new DataGenerator_TickData_Index(this);
        }

        #region PATH

        public string GetPath_Code()
        {
            return CsvHistoryData_PathUtils.GetInstrumentsPath(updatedDataPath);
        }

        public string GetPath_TradingDays()
        {
            return CsvHistoryData_PathUtils.GetTradingDaysPath(updatedDataPath);
        }

        public string GetPath_TradingSession(string code)
        {
            return CsvHistoryData_PathUtils.GetTradingSessionPath(updatedDataPath, code);
        }

        public string GetPath_TickData(string code, int date)
        {
            return CsvHistoryData_PathUtils.GetTickDataPath(updatedDataPath, code, date);
        }

        public string GetPath_KLineData(string code, int date, KLinePeriod klinePeriod)
        {
            return CsvHistoryData_PathUtils.GetKLineDataPath(updatedDataPath, code, date, klinePeriod);
        }

        #endregion

        #region NEWDATA

        /// <summary>
        /// 得到新的交易日
        /// </summary>
        /// <returns></returns>
        public List<int> GetNewTradingDays()
        {
            return dataProvider.GetNewTradingDays();
        }

        private ITradingDayReader newTradingDayReader;

        public ITradingDayReader GetNewTradingDayCache()
        {
            if (newTradingDayReader == null)
            {
                newTradingDayReader = new TradingDayCache(GetNewTradingDays());
            }
            return newTradingDayReader;
        }

        /// <summary>
        /// 得到新的合约
        /// </summary>
        /// <returns></returns>
        public List<CodeInfo> GetNewCodes()
        {
            return dataProvider.GetNewCodes();
        }

        public ITickData GetNewTickData(string code, int date)
        {
            CodeIdParser parser = new CodeIdParser(code);
            if (parser.Suffix == "MI")
            {
                return generator_Main.Generate(parser.VarietyId, date);
            }
            else if (parser.Suffix == "0000")
            {
                return generator_Index.Generate(parser.VarietyId, date);
            }
            return dataProvider.LoadTickData(code, date);
        }

        #endregion

        #region MIXDATA

        private CodeInfoCache newCodeInfoCache;

        public CodeInfo GetCodeInfo(String code)
        {
            CodeInfo codeInfo = updatedDataLoader.GetCodeCache().GetCode(code);
            if (codeInfo != null)
                return codeInfo;
            if (newCodeInfoCache == null)
                newCodeInfoCache = new CodeInfoCache(dataProvider.GetNewCodes());
            return newCodeInfoCache.GetCode(code);
        }

        private ITradingDayReader allTradingDayReader;

        public ITradingDayReader GetAllTradingDayReader()
        {
            if (allTradingDayReader == null)
            {
                ITradingDayReader updatedTradingDayReader = GetUpdatedTradingDayReader();
                List<int> allTradingDays = new List<int>();
                List<int> tradingDays = updatedTradingDayReader.GetAllTradingDays();
                allTradingDays.AddRange(tradingDays);
                List<int> newTradingDays = GetNewTradingDays();
                for (int i = 0; i < newTradingDays.Count; i++)
                {
                    int newTradingDay = newTradingDays[i];
                    if (!updatedTradingDayReader.IsTrade(newTradingDay))
                        allTradingDays.Add(newTradingDay);
                }
                allTradingDayReader = new TradingDayCache(allTradingDays);
            }
            return allTradingDayReader;
        }

        /// <summary>
        /// 得到未更新的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isFill"></param>
        /// <returns></returns>
        public List<int> GetNotUpdateTradingDays_TickData(string code, bool isFill)
        {
            CodeInfo codeInfo = GetCodeInfo(code);
            List<int> newTradingDays = this.GetNewTradingDays();
            List<int> updatedTradingDays = this.GetUpdatedTickDataTradingDays(code);
            return GetNotUpdateTradingDays(codeInfo, newTradingDays, updatedTradingDays, isFill);
        }

        /// <summary>
        /// 得到未更新的KLine数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isFill"></param>
        /// <returns></returns>
        public List<int> GetNotUpdateTradingDays_KLineData(string code, bool isFill)
        {
            CodeInfo codeInfo = GetCodeInfo(code);
            List<int> newTradingDays = this.GetNewTradingDays();
            List<int> updatedTradingDays = this.GetUpdatedKLineDataTradingDays(code);
            return GetNotUpdateTradingDays(codeInfo, newTradingDays, updatedTradingDays, isFill);
        }

        private static List<int> GetNotUpdateTradingDays(CodeInfo codeInfo, List<int> newTradingDays, List<int> updatedTradingDays, bool isFill)
        {
            int start = codeInfo.Start;
            int end = codeInfo.End <= 0 ? int.MaxValue : codeInfo.End;

            List<int> notUpdateTradingDays = new List<int>();
            if (isFill)
            {
                HashSet<int> set = new HashSet<int>(updatedTradingDays);
                for (int i = 0; i < newTradingDays.Count; i++)
                {
                    int newTradingDay = newTradingDays[i];
                    if (newTradingDay < start || newTradingDay > end)
                        continue;
                    if (set.Contains(newTradingDay))
                        continue;
                    notUpdateTradingDays.Add(newTradingDay);
                }
            }
            else
            {
                updatedTradingDays.Sort();
                int lastDay = updatedTradingDays.Count == 0 ? -1 : updatedTradingDays[updatedTradingDays.Count - 1];

                for (int i = 0; i < newTradingDays.Count; i++)
                {
                    int newTradingDay = newTradingDays[i];
                    if (newTradingDay < start || newTradingDay > end)
                        continue;
                    if (newTradingDay <= lastDay)
                        continue;
                    notUpdateTradingDays.Add(newTradingDay);
                }
            }
            return notUpdateTradingDays;
        }

        #endregion

        #region UpdatedData

        /// <summary>
        /// 得到所有品种的交易时间读取器
        /// </summary>
        /// <returns></returns>
        public ITradingTimeReader GetTradingSessionDetailReader()
        {
            return dataLoader_TradingSessionDetail;
        }

        public List<CodeInfo> GetUpdatedCodes(string variety)
        {
            return updatedDataLoader.GetCodeCache().GetCodesByCatelog(variety);
        }

        public List<CodeInfo> GetUpdatedCodes(string variety, int date)
        {
            return updatedDataLoader.GetCodeCache().GetCodesByCatelog(variety, date);
        }

        public ITradingDayReader GetUpdatedTradingDayReader()
        {
            return updatedDataLoader.GetTradingDayCache();
        }

        public List<TradingSession> GetUpdatedTradingSessions(string code)
        {
            return this.updatedDataLoader.GetTradingSessions(code);
        }

        public List<double[]> GetUpdatedTradingSessionDetail(string code, int date)
        {
            CodeIdParser parser = new CodeIdParser(code);
            string variety = parser.VarietyId;
            VarietyInfo varietyInfo = dataLoader_Variety.GetVariety(variety);
            if (varietyInfo == null)
                return null;
            string exchange = varietyInfo.Exchange;
            return dataLoader_TradingSessionDetail.GetTradingSessionDetail(exchange, variety, date);
        }

        /// <summary>
        /// 得到已经更新数据里的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ITickData GetUpdatedTickData(string code, int date)
        {
            return updatedDataLoader.GetTickData(code, date);
        }

        public List<int> GetUpdatedTickDataTradingDays(string code)
        {
            return updatedDataLoader.GetTickDataTradingDays(code);
        }

        public IKLineData GetUpdatedKLineData(string code, int date, KLinePeriod klinePeriod)
        {
            return updatedDataLoader.GetKLineData(code, date);
        }

        public List<int> GetUpdatedKLineDataTradingDays(string code)
        {
            return updatedDataLoader.GetKLineDataTradingDays(code);
        }
        #endregion
    }
}
