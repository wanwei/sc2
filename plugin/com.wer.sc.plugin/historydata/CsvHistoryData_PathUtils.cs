using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 提供历史数据的插件的数据保存路径
    /// 
    /// 提供给SC系统的历史数据可以以CSV保存在指定目录，通过插件系统会自动将这些数据更新成SC识别的格式
    /// 
    /// 数据目录：
    ///     --opendates.csv  开盘日期
    ///     --instruments.csv      所有品种信息
    ///     --m01
    ///         --tick  每日的tick数据
    ///             --M01_20040102.csv  
    ///             --M01_20040105.csv
    ///             --......
    ///         --kline  品种的K线数据
    ///             --1minute
    ///                 --m01_1minute_20040102.csv
    ///                 --m01_1minute_20040105.csv
    ///             --......
    ///         --m01_tradingsession.csv
    ///     --m03
    ///     --......
    /// </summary>
    public class CsvHistoryData_PathUtils
    {
        /// <summary>
        /// 得到保存所有股票或期货数据的路径
        /// </summary>
        /// <returns></returns>
        public static string GetInstrumentsPath(String csvDataPath)
        {
            return csvDataPath + "\\instruments.csv";
        }

        public static string GetTradingDaysPath(String csvDataPath)
        {
            return csvDataPath + "\\tradingdays.csv";
        }

        public static String GetTradingSessionPath(String csvDataPath, String code)
        {
            return csvDataPath + "\\" + code + "\\" + code + "_tradingsession" + ".csv";
        }

        public static String GetTickDataPath(String csvDataPath, String code, int date)
        {
            return csvDataPath + "\\" + code + "\\tick" + "\\" + code + "_" + date + ".csv";
        }

        public static String GetKLineDataPath(String csvDataPath, String code, int date, KLinePeriod period)
        {
            return csvDataPath + "\\" + code + "\\kline\\" + period.ToEngString() + "\\" + code + "_" + period.ToEngString() + "_" + date + ".csv";
        }
    }
}
