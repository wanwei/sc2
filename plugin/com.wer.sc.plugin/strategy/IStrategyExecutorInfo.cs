using com.wer.sc.data;
using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 单个策略的执行信息类
    /// </summary>
    public interface IStrategyExecutorInfo
    {
        /// <summary>
        /// 得到策略执行器里执行的品种ID及时间
        /// </summary>
        ICodePeriod CodePeriod { get; }

        /// <summary>
        /// 得到当前的K线
        /// </summary>
        IKLineData CurrentKLineData { get; }

        /// <summary>
        /// 总共要执行的天数
        /// </summary>
        int TotalDayCount { get; }

        /// <summary>
        /// 当前执行的日期
        /// </summary>
        int CurrentDay { get; }

        /// <summary>
        /// 当前执行的日期索引号
        /// </summary>
        int CurrentDayIndex { get; }

        /// <summary>
        /// 执行是否结束
        /// </summary>
        bool IsFinished { get; }
    }
}