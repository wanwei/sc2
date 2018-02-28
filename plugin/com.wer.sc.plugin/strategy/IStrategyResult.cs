using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.utils;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行结果
    /// 
    /// 一个策略执行结果可能包含多支股票或期货在一段时间的执行结果
    /// </summary>
    public interface IStrategyResult : IXmlExchange_File
    {
        /// <summary>
        /// 策略里使用的
        /// </summary>
        IList<ICodePeriod> CodePeriods { get; }

        /// <summary>
        /// 策略执行开始时间
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 策略执行结束时间
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 回测前进的周期
        /// </summary>
        ForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 回测引用的周期
        /// </summary>
        StrategyReferedPeriods ReferedPeriods { get; }

        /// <summary>
        /// 回测使用的参数
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// 回测找到的总结果集
        /// </summary>
        IStrategyQueryResultManager StrategyQueryResults { get; }

        /// <summary>
        /// 得到代码周期的单独运行结果
        /// 有绘图或者交易的策略才会有单独的代码周期结果，否则没有
        /// </summary>
        IList<IStrategyResult_CodePeriod> StrategyResult_Codes { get; }

        /// <summary>
        /// 得到指定代码周期的运行结果
        /// </summary>
        /// <param name="codePeriod"></param>
        /// <returns></returns>
        IStrategyResult_CodePeriod GetStrategyResult_Code(ICodePeriod codePeriod);
    }
}