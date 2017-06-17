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
        /// 得到新的交易合约
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> LoadNewInstruments();

        /// <summary>
        /// 获得某品种的所有合约
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        List<CodeInfo> LoadInstruments(string variety);

        /// <summary>
        /// 根据id名称得到对应品种
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CodeInfo GetInstrument(string code);

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
        /// 获得交易明细读取器
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

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="period"></param>
        /// <param name="lastEndPrice"></param>
        /// <param name="lastEndHold"></param>
        /// <returns></returns>
        IKLineData LoadKLineData(string code, int date, KLinePeriod period, float lastEndPrice, int lastEndHold);

        /// <summary>
        /// 目标目录
        /// </summary>
        /// <returns></returns>
        String GetTargetDataPath();

        /// <summary>
        /// 装载已经更新好的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData LoadUpdatedTickData(string code, int date);

        /// <summary>
        /// 装载已经更新的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData LoadUpdatedKLineData(string code, int date, KLinePeriod period);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<TradingSession> LoadUpdatedTradingSessions(string code);

    }
}
