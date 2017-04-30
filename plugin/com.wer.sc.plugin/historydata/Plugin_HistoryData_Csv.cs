using com.wer.sc.data;
using com.wer.sc.data.reader.cache;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 历史数据获取插件的一个CSV文件方式的实现
    /// 用户只需要按照一定格式将CSV文件保存到指定目录，系统可以自动自动识别并生成数据中心需要的数据
    /// 
    /// 用户需要将数据保存成以下方式：
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
    ///     
    /// 注：默认周期超过1分钟的K线数据都用1分钟数据生成，秒级K线需要自己生成。
    /// </summary>
    public abstract class Plugin_HistoryData_Csv : IPlugin_HistoryData
    {
        protected PluginHelper pluginHelper;

        public Plugin_HistoryData_Csv(PluginHelper pluginHelper)
        {
            this.pluginHelper = pluginHelper;
        }

        /// <summary>
        /// 该插件提供的所有股票或期货信息
        /// </summary>
        /// <returns></returns>
        public virtual List<CodeInfo> GetInstruments()
        {
            return CsvUtils_Code.Load(CsvHistoryData_PathUtils.GetInstrumentsPath(GetCsvDataPath()));
        }

        /// <summary>
        /// 得到所有开盘日
        /// </summary>
        /// <returns></returns>
        public virtual List<int> GetTradingDays()
        {
            return CsvUtils_TradingDay.Load(CsvHistoryData_PathUtils.GetTradingDaysPath(GetCsvDataPath()));
        }

        /// <summary>
        /// 得到所有开盘日的开盘时间
        /// 实现该方法的原因：
        /// 系统需要有一个方法来获取指定日期的K线，比如获取20130106的1分钟K线
        /// 由于所有1分钟K线是保存在一个文件里的，系统无法获取20130106开盘那根K线的起始位置。
        /// 所以此处需要获取开盘时间数据
        /// 
        /// 各个市场的开盘时间数据很混乱：
        /// 比如中国期货市场就有夜盘，而夜盘在交易时间上算是第二天，所以20160105可能在20160104就开盘了
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual List<TradingSession> GetTradingSessions(String code)
        {
            return CsvUtils_TradingSession.Load(CsvHistoryData_PathUtils.GetTradingSessionPath(GetCsvDataPath(), code));
        }

        /// <summary>
        /// 得到股票或期货的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual ITickData GetTickData(String code, int date)
        {
            return CsvUtils_TickData.Load(CsvHistoryData_PathUtils.GetTickDataPath(GetCsvDataPath(), code, date));
        }

        /// <summary>
        /// 得到股票或期货的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public virtual IKLineData GetKLineData(String code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            List<int> tradingDays = GetTradingDays();
            TradingDayCache cache = new TradingDayCache(tradingDays);
            IList<int> resultOpenDates = cache.GetTradingDays(startDate, endDate);

            //如果存在该周期的源数据直接生成，否则用1分钟K线生成
            if (Exist(code, resultOpenDates[0], klinePeriod))
                return GetKLineData(code, klinePeriod, resultOpenDates);

            IKLineData oneMinuteKLine = GetKLineData(code, KLinePeriod.KLinePeriod_1Minute, resultOpenDates);
            return DataTransfer_KLine2KLine.Transfer(oneMinuteKLine, klinePeriod, new TradingSessionCache_Instrument(code, GetTradingSessions(code)));
        }

        private IKLineData GetKLineData(string code, KLinePeriod klinePeriod, IList<int> resultOpenDates)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            for (int i = 0; i < resultOpenDates.Count; i++)
            {
                IKLineData klineData = GetKLineData(code, resultOpenDates[i], klinePeriod);
                if (klineData != null)
                    klineDataList.Add(klineData);
            }

            return KLineData.Merge(klineDataList);
        }

        /// <summary>
        /// 得到单日的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public virtual IKLineData GetKLineData(string code, int date, KLinePeriod period)
        {
            string path = CsvHistoryData_PathUtils.GetKLineDataPath(GetCsvDataPath(), code, date, period);
            return CsvUtils_KLineData.Load(path);
        }

        private bool Exist(string code, int date, KLinePeriod period)
        {
            string path = CsvHistoryData_PathUtils.GetKLineDataPath(GetCsvDataPath(), code, date, period);
            return File.Exists(path);
        }

        /// <summary>
        /// 得到放置CSV文件的路径
        /// </summary>
        /// <returns></returns>
        public abstract string GetCsvDataPath();
    }
}
