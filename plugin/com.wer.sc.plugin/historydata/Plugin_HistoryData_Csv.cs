using com.wer.sc.data;
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
using com.wer.sc.plugin.data;
using com.wer.sc.utils;

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
    ///     --opentime.csv   缺省的开盘时间
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
    ///         --m01_tradingtime.csv
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

        public virtual TradingTime GetDefaultTradingTime()
        {
            List<TradingTime> tts = CsvUtils_TradingTime.Load(CsvHistoryData_PathUtils.GetDefaultTradingTimePath(GetCsvDataPath()));
            if (tts == null || tts.Count == 0)
                return null;
            return tts[0];
        }

        public virtual IList<TradingTime> GetTradingTime(string code)
        {
            return CsvUtils_TradingTime.Load(CsvHistoryData_PathUtils.GetTradingTimePath(GetCsvDataPath(), code));
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
        /// 得到现有的tick所有数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<int> GetTickDataDays(string code)
        {            
            string tickPath = CsvHistoryData_PathUtils.GetTickDataPath(GetCsvDataPath(), code);
            if (!Directory.Exists(tickPath))
                return null;
            string[] files = Directory.GetFiles(tickPath);
            List<int> tickDays = new List<int>(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startIndex = file.LastIndexOf('_') + 1;
                tickDays.Add(int.Parse(file.Substring(startIndex, 8)));
            }
            return tickDays;
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
            IList<int> tradingDays = GetTickDataDays(code);// GetTradingDays();
            if (tradingDays == null)
                return null;
            CacheUtils_TradingDay cache = new CacheUtils_TradingDay(tradingDays);
            IList<int> openDates = cache.GetTradingDays(startDate, endDate);
            if (openDates == null || openDates.Count == 0)
                return null;
            float lastEndPrice = -1;
            int lastEndHold = -1;
            int prevTradingDay = cache.GetPrevTradingDay(openDates[0]);
            //找到之前交易日的收盘价和收盘持仓
            if (prevTradingDay > 0)
            {
                ITickData tickData = GetTickData(code, prevTradingDay);
                lastEndPrice = tickData.Arr_Price[tickData.Length - 1];
                lastEndHold = tickData.Arr_Hold[tickData.Length - 1];
            }

            //如果存在该周期的源数据直接生成，否则用1分钟K线生成
            if (Exist(code, openDates[0], klinePeriod))
                return GetKLineData(code, klinePeriod, openDates, lastEndPrice, lastEndHold);

            IKLineData oneMinuteKLine = GetKLineData(code, KLinePeriod.KLinePeriod_1Minute, openDates, lastEndPrice, lastEndHold);
            if (oneMinuteKLine.Length == 0)
                return null;
            IList<TradingTime> sessions = GetTradingTime(code);
            if (sessions == null)
                return null;
            return DataTransfer_KLine2KLine.Transfer(oneMinuteKLine, klinePeriod, new CacheUtils_TradingTime(code, GetTradingTime(code)));
        }

        private IKLineData GetKLineData(string code, KLinePeriod klinePeriod, IList<int> openDates, float lastEndPrice, int lastEndHold)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                IKLineData klineData = GetKLineData(code, openDate, klinePeriod);
                if (klineData != null)
                {
                    klineDataList.Add(klineData);
                    lastEndPrice = klineData.Arr_End[klineData.Length - 1];
                    lastEndHold = klineData.Arr_Hold[klineData.Length - 1];
                }
                else
                {
                    List<double[]> tradingTime = GetTradingTime(code, openDate);
                    IList<double[]> klineTimes = TradingTimeUtils.GetKLineTimeList_Full(tradingTime, klinePeriod);
                    klineData = DataTransfer_Tick2KLine.GetEmptyKLineData(klineTimes, lastEndPrice, lastEndHold);
                    klineDataList.Add(klineData);
                }
            }

            return KLineData.Merge(klineDataList);
        }

        /// <summary>
        /// 得到现有的K线所有数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<int> GetKLineDataDays(string code)
        {
            string klinePath = CsvHistoryData_PathUtils.GetKLineDataPath(GetCsvDataPath(), code, KLinePeriod.KLinePeriod_1Minute);
            string[] files = Directory.GetFiles(klinePath);
            List<int> klineDays = new List<int>(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startIndex = file.LastIndexOf('_') + 1;
                klineDays.Add(int.Parse(file.Substring(startIndex, 8)));
            }
            return klineDays;
        }

        private List<double[]> GetTradingTime(string code, int date)
        {
            IList<TradingTime> tradingTime = GetTradingTime(code);
            if (tradingTime != null)
            {
                for (int i = 0; i < tradingTime.Count; i++)
                {
                    TradingTime tt = tradingTime[i];
                    if (tt.TradingDay == date)
                    {
                        return tt.TradingPeriods;
                    }
                }
            }
            return null;
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
            if (!File.Exists(path))
                return null;
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

        public virtual IList<MainContractInfo> GetMainContractInfos()
        {
            string mainFuturesPath = CsvHistoryData_PathUtils.GetMainFuturesPath(GetCsvDataPath());
            return TextExchangeUtils.Load<MainContractInfo>(mainFuturesPath, typeof(MainContractInfo));
        }
    }
}
