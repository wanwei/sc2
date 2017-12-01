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
    /// 回测K线bar结束参数
    /// </summary>
    public interface IStrategyOnBarArgument
    {
        /// <summary>
        /// 得到当前回归测试的股票或期货合约
        /// </summary>
        String Code { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        IStrategyOnBarInfo MainBar { get; }

        /// <summary>
        /// 得到触发OnBar事件时正好结束的K线柱子
        /// </summary>
        IList<IStrategyOnBarInfo> FinishedBars { get; }

        /// <summary>
        /// 得到所有的K线柱子
        /// </summary>
        IList<IStrategyOnBarInfo> Bars { get; }

        /// <summary>
        /// 得到当前数据
        /// </summary>
        IRealTimeDataReader_Code CurrentData { get; }

        /// <summary>
        /// 得到其它数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeDataReader_Code GetOtherData(string code);
    }

    public interface IStrategyOnBarInfo
    {
        int BarPos { get; }

        IKLineBar KLineBar { get; }

        IKLineData_Extend KLineData { get; }

        KLinePeriod KLinePeriod { get; }
    }
}