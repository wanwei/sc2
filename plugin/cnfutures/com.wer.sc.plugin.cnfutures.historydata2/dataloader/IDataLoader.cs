using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    /// <summary>
    /// 该接口用来加载已有的期货历史数据
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// 得到所有的交易合约
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> LoadAllInstruments();

        /// <summary>
        /// 获得某品种的所有合约
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        List<CodeInfo> LoadInstruments(string variety);

        /// <summary>
        /// 得到交易日读取器
        /// </summary>
        /// <returns></returns>
        ITradingDayReader LoadTradingDayReader();

        /// <summary>
        /// 得到一个品种的所有交易时段
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<TradingSession> LoadTradingSessions(string code);

        /// <summary>
        /// 得到交易时段明细
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<double[]> LoadTradingSessionDetail(String code, int date);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITradingTimeReader LoadTradingSessionDetailReader();

        /// <summary>
        /// 装载tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData LoadTickData(string code, int date);

        IKLineData LoadKLineData(string code, int date, KLinePeriod period, float lastEndPrice, int lastEndHold);

        ITickData LoadUpdatedTickData(string code, int date);


        IKLineData LoadUpdatedKLineData(string code, int date, KLinePeriod period);

        List<TradingSession> LoadUpdatedTradingSessions(string code);
    }
}
