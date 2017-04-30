using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.cnfutures.historydata.dataloader.taobao1;
using System;
using System.Collections.Generic;

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
    public class DataLoader
    {
        private DataLoader_InstrumentInfo dataLoader_Instrument;

        private DataLoader_TradingDay dataLoader_TradingDay;

        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        private DataLoader_TickData dataLoader_TickData;

        private Plugin_HistoryData_CnFutures plugin_HistoryData_CnFutures;

        private string srcDataPath;

        private string csvDataPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="srcDataPath">提供最原始数据的路径</param>
        /// <param name="csvDataPath">提供给</param>
        public DataLoader(String srcDataPath, string csvDataPath, string dataCenterUri)
        {
            this.srcDataPath = srcDataPath;
            this.csvDataPath = csvDataPath;
            this.dataLoader_Instrument = new DataLoader_InstrumentInfo();
            this.dataLoader_TradingDay = new DataLoader_TradingDay(srcDataPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(this.dataLoader_Instrument);
            this.dataLoader_TickData = new DataLoader_TickData(srcDataPath, dataLoader_Instrument);
            this.plugin_HistoryData_CnFutures = new Plugin_HistoryData_CnFutures(csvDataPath, dataCenterUri);
        }

        public string SrcDataPath
        {
            get
            {
                return srcDataPath;
            }
        }

        public string CsvDataPath
        {
            get
            {
                return csvDataPath;
            }
        }

        public DataLoader_InstrumentInfo DataLoader_Instrument
        {
            get
            {
                return dataLoader_Instrument;
            }
        }

        public DataLoader_TradingDay DataLoader_OpenDate
        {
            get
            {
                return dataLoader_TradingDay;
            }
        }

        public DataLoader_TradingSessionDetail DataLoader_OpenTime
        {
            get
            {
                return dataLoader_TradingSessionDetail;
            }
        }

        public DataLoader_TickData DataLoader_TickData
        {
            get
            {
                return dataLoader_TickData;
            }
        }

        public ITickData LoadUpdatedTickData(string code, int date)
        {
            return plugin_HistoryData_CnFutures.GetTickData(code, date);
        }

        public IKLineData LoadUpdatedKLineData(string code, int date, KLinePeriod period)
        {
            return plugin_HistoryData_CnFutures.GetKLineData(code, date, period);
        }

        public List<TradingSession> LoadUpdatedTradingSessions(string code)
        {
            return plugin_HistoryData_CnFutures.GetTradingSessions(code);
        }
    }
}