using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// K线数据存储
    /// </summary>
    public interface IKLineDataStore
    {
        /// <summary>
        /// 保存K线数据，该方法会覆盖掉之前的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="data"></param>
        void Save(string code, KLinePeriod klinePeriod, IKLineData data);

        /// <summary>
        /// 保存新的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="data"></param>
        void Append(string code, KLinePeriod klinePeriod, IKLineData data);

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        KLineData Load(string code, int startDate, int endDate, KLinePeriod klinePeriod);

        /// <summary>
        /// 装载所有的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        KLineData LoadAll(string code, KLinePeriod klinePeriod);

        /// <summary>
        /// 删掉某个品种一个周期的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        void Delete(string code, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到所有
        /// </summary>
        /// <returns></returns>
        List<int> GetAllTradingDay(string code, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到保存的第一个交易日
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        int GetFirstTradingDay(string code, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到保存的最后一个交易日
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        int GetLastTradingDay(string code, KLinePeriod klinePeriod);

        /// <summary>
        /// 得到保存的最后一个交易时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        double GetLastTradingTime(string code, KLinePeriod klinePeriod);
    }
}