using com.wer.sc.data;
using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 历史数据插件
    /// 该插件的作用是给系统提供市场的历史数据，系统会将插件提供的数据更新到系统的数据中心。
    /// 这些数据可以用来做回归测试，也可以在实盘交易中根据历史数据进行分析。
    /// 
    /// 现在提供以下数据：
    /// 1.所有股票或期货的信息
    /// 2.所有交易的日期
    /// 3.一只股票的所有开盘日及每日的开始时间
    /// 4.Tick数据：对于期货市场，是1秒两次或四次的交易数据。股票数据基本是成交明细数据
    /// 5.K线数据
    /// 6.确认哪些数据需要更新
    /// </summary>
    public interface IPlugin_HistoryData
    {
        /// <summary>
        /// 该插件提供的所有股票或期货信息
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> GetInstruments();

        /// <summary>
        /// 得到所有开盘日
        /// </summary>
        /// <returns></returns>
        List<int> GetTradingDays();

        /// <summary>
        /// 得到该市场默认的开盘时间
        /// </summary>
        /// <returns></returns>
        List<TradingSession> GetTradingSessions();

        /// <summary>
        /// 得到所有交易日的交易时间
        /// 实现该方法的原因：
        /// 系统需要有一个方法来获取指定日期的K线，比如获取20130106的1分钟K线
        /// 由于所有1分钟K线是保存在一个文件里的，系统无法获取20130106开盘那根K线的起始位置。
        /// 所以此处需要获取开盘时间数据
        /// 
        /// 各个市场的开盘时间数据很混乱：
        /// 比如中国期货市场就有夜盘，而夜盘在交易时间上算是第二天，所以20160105在20160104.21就开盘了
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<TradingSession> GetTradingSessions(String code);

        /// <summary>
        /// 得到指定合约和日期的开盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        IList<ITradingTime> GetTradingTime(string code);

        /// <summary>
        /// 得到该市场默认的开盘时间
        /// </summary>
        /// <returns></returns>
        ITradingTime GetDefaultTradingTime();
      
        /// <summary>
        /// 得到股票或期货的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData GetTickData(String code, int date);

        /// <summary>
        /// 得到现有的tick所有数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<int> GetTickDataDays(string code);

        /// <summary>
        /// 得到股票或期货的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IKLineData GetKLineData(String code, int startDate, int endDate, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到现有的K线所有数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<int> GetKLineDataDays(string code);

        /// <summary>
        /// 获得主力合约信息
        /// </summary>
        /// <returns></returns>
        IList<MainContractInfo> GetMainContractInfos();
    }

    public class NeedsToUpdate
    {
        private bool isTickUpdate = false;

        private List<KLinePeriod> klinePeriods = new List<KLinePeriod>();

        public bool IsTickUpdate
        {
            get
            {
                return isTickUpdate;
            }

            set
            {
                isTickUpdate = value;
            }
        }

        public List<KLinePeriod> KlinePeriods
        {
            get
            {
                return klinePeriods;
            }
        }
    }
}