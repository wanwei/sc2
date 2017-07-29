using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 一只股票或期货在一段时间内的数据包
    /// </summary>
    public interface IDataPackage
    {
        /// <summary>
        /// 得到股票或期货的ID
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到开始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 得到这段时间内的所有交易日
        /// </summary>
        /// <returns></returns>
        IList<int> GetTradingDays();

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetKLineData(KLinePeriod period);

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetTimeLineData(int date);

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData GetTickData(int date);

        /// <summary>
        /// 得到交易时间
        /// </summary>
        /// <returns></returns>
        ITradingSessionReader_Instrument GetTradingSessionReader();

        float GetLastEndPrice(int date);
    }
}