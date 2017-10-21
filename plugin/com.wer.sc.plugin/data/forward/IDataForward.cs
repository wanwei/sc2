using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 历史数据前进器接口
    /// 
    /// 该接口可以同时跟踪多个股票或期货
    /// </summary>
    public interface IDataForward : IRealTimeDataReader
    {
        /// <summary>
        /// 前进
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
        IDataForward_Code GetHistoryDataForward(string code);

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
        /// 会接收这里面所有codes的前进事件
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// 
        /// 如果只希望侦听一个
        /// </summary>
        event DelegateOnTick OnTick;

        /// <summary>
        /// 接收到了新K线数据触发该响应
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// </summary>
        event DelegateOnBar OnBar;
    }
}