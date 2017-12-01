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
    public interface IStrategyOnTickArgument
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
        /// 得到触发OnTick的tick数据
        /// </summary>
        IStrategyOnTickInfo Tick { get; }

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

    public interface IStrategyOnTickInfo
    {
        ITickBar TickBar { get; }

        ITickData_Extend TickData { get; }
    }
}