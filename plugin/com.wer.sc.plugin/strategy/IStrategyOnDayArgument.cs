using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 执行策略时，当时间前进时主周期完成一个bar时
    /// </summary>
    public interface IStrategyOnDayArgument
    {
        /// <summary>
        /// 得到当前回归测试的股票或期货合约
        /// </summary>
        String Code { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到主周期的bar
        /// </summary>
        IStrategyOnBarInfo MainBar { get; }

        /// <summary>
        /// 得到触发OnBar事件时正好结束的K线柱子
        /// </summary>
        IList<IStrategyOnBarInfo> FinishedBars { get; }

        /// <summary>
        /// 得到指定周期的已结束的bar
        /// 如果该周期的bar未结束，则返回空
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IStrategyOnBarInfo GetFinishedBar(KLinePeriod klinePeriod);

        /// <summary>
        /// 得到所有的K线柱子
        /// </summary>
        IList<IStrategyOnBarInfo> Bars { get; }

        /// <summary>
        /// 得到当前数据
        /// </summary>
        IRealTimeData_Code CurrentData { get; }

        /// <summary>
        /// 得到其它数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeData_Code GetOtherData(string code);
    }   
}