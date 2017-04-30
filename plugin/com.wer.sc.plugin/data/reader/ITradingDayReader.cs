using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 开盘日读取接口
    /// </summary>
    public interface ITradingDayReader
    {
        /// <summary>
        /// 得到该日是否开盘
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        bool IsTrade(int day);

        /// <summary>
        /// 得到所有开盘日数据
        /// </summary>
        /// <returns></returns>
        List<int> GetAllTradingDays();

        /// <summary>
        /// 得到最近的开盘日index
        /// </summary>
        /// <returns></returns>
        int FirstTradingDay { get; }

        /// <summary>
        /// 得到最近的开盘日日期
        /// </summary>
        int LastTradingDay { get; }

        /// <summary>
        /// 获得start到end中的所有开盘的日期
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IList<int> GetTradingDays(int start, int end);

        /// <summary>
        /// 获取两日间的所有开盘日，如果beginDate或endDate开盘，那么会包含这两日
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        int GetTradingDayCount(int beginDate, int endDate);

        /// <summary>
        /// 通过index得到该开盘日期
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        int GetTradingDay(int index);

        /// <summary>
        /// 获得该日期在所有开盘日里的索引号，如果该日不开盘，则返回-1
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int GetTradingDayIndex(int date);

        /// <summary>
        /// 获得该日期在所有开盘日里的索引号，
        /// 如果该日不开盘，如果isFindPrev为true，则返回之前的第一个开盘日的index，否则返回之后的开盘日的index
        /// </summary>
        /// <param name="date"></param>
        /// <param name="isFindPrev"></param>
        /// <returns></returns>
        int GetTradingDayIndex(int date, bool isFindPrev);

        /// <summary>
        /// 得到下一个开盘日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int GetNextTradingDay(int date);

        /// <summary>
        /// 得到下面的第n个开盘日
        /// </summary>
        /// <param name="date"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int GetNextTradingDay(int date, int length);

        /// <summary>
        /// 得到之前的开盘日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int GetPrevTradingDay(int date);

        /// <summary>
        /// 得到之前的第n个开盘日
        /// </summary>
        /// <param name="date"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int GetPrevTradingDay(int date, int length);
    }
}