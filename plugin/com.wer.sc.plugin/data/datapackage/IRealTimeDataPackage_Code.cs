using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 实时数据包接口
    /// </summary>
    public interface IRealTimeDataPackage_Code
    {
        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        void ChangeTime(double time);

        /// <summary>
        /// 得到当前日期
        /// </summary>
        int TradingDay { get; }

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
        /// 如果是秒周期，则获得当日的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData_Extend GetKLineData(KLinePeriod period);

        /// <summary>
        /// 得到今日的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData_Extend GetTimeLineData();

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData_Extend GetTickData();
    }
}