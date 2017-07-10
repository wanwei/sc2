using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 历史数据前进器
    /// </summary>
    public interface IHistoryDataForward
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Forward();

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 前进周期
        /// </summary>
        ForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 得到前进时包含的所有code
        /// </summary>
        /// <returns></returns>
        IList<string> GetAllCodes();

        /// <summary>
        /// 得到指定的
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IHistoryDataForward_Code GetHistoryDataForward(string code);

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        bool IsEnd { get; }

        /// <summary>
        /// 是否到一天的结束
        /// </summary>
        bool IsDayEnd { get; }

        /// <summary>
        /// 接收到了tick数据触发该响应
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// </summary>
        event DelegateOnTick OnTick;

        /// <summary>
        /// 接收到了新K线数据触发该响应
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// </summary>
        event DelegateOnBar OnBar;
    }
}
