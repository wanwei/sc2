using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 用做数据前进的数据包
    /// </summary>
    public interface IDataForForward_Code : IXmlExchange
    {
        /// <summary>
        /// 数据包
        /// </summary>
        IDataPackage_Code DataPackage
        {
            get;
        }

        bool UseTickData
        {
            get;
        }

        bool UseTimeLineData
        {
            get;
        }

        /// <summary>
        /// 数据包里的K线周期
        /// </summary>
        IList<KLinePeriod> ReferedKLinePeriods { get; }

        /// <summary>
        /// 得到主K线周期
        /// </summary>
        KLinePeriod MainKLinePeriod { get; }

        /// <summary>
        /// 获取数据的code
        /// </summary>
        string Code
        {
            get;
        }

        /// <summary>
        /// 获取当前交易日
        /// </summary>
        int TradingDay
        {
            get;
        }

        /// <summary>
        /// 获取交易日读取器
        /// </summary>
        ITradingDayReader TradingDayReader
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        int StartDate
        {
            get;
        }

        int EndDate
        {
            get;
        }

        IKLineData_RealTime GetMainKLineData();

        IKLineData_RealTime GetKLineData(KLinePeriod klinePeriod);

        ITickData_Extend CurrentTickData
        {
            get;
        }

        ITimeLineData_RealTime CurrentTimeLineData
        {
            get;
        }
    }
}