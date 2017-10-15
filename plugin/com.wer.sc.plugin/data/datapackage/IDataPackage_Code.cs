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
    /// 通过该接口可以获得该股票或期货在这段时间内的所有数据:包括K线，分时线，tick等
    /// 
    /// 整个系统里用到的数据建议都通过该接口完成，不建议直接调用IDataReader接口调用数据
    /// 系统对该接口做了很多优化工作，包括缓存，调用时初始化，数据共享等
    /// </summary>
    public interface IDataPackage_Code
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
        /// 得到数据包里的所有交易日读取器
        /// </summary>
        /// <returns></returns>
        ITradingDayReader GetTradingDayReader();

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData_Extend GetKLineData(KLinePeriod period);

        /// <summary>
        /// 创建一个实时K线数据
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData_RealTime CreateKLineData_RealTime(KLinePeriod period);

        /// <summary>
        /// 创建一批实时K线数据
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        Dictionary<KLinePeriod, IKLineData_RealTime> CreateKLineData_RealTimes(IList<KLinePeriod> periods);

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetTimeLineData(int date);

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData_Extend GetTickData(int date);

        /// <summary>
        /// 得到交易时间读取接口
        /// </summary>
        /// <returns></returns>
        ITradingTimeReader_Code GetTradingTimeReader();

        /// <summary>
        /// 得到昨日收盘价
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        float GetLastEndPrice(int date);
    }
}