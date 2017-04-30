using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public interface IMarketData
    {
        /// <summary>
        /// 得到单支股票或期货的交易时间
        /// </summary>
        /// <returns></returns>
        List<double[]> GetTradingSession(string code, int date);

        /// <summary>
        /// 连接指定服务器
        /// </summary>
        void Connect(ConnectionInfo connectionInfo);

        /// <summary>
        /// 断开连接
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 重新连接
        /// </summary>
        void ReConnect();

        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        event DelegateOnConnectionStatus ConnectionStatusChanged;

        /// <summary>
        /// 订阅指定的品种
        /// </summary>
        /// <param name="instruments"></param>
        void Subscribe(string[] instruments);

        /// <summary>
        /// 取消订阅品种
        /// </summary>
        /// <param name="instruements"></param>
        void UnSubscribe(string[] instruements);

        /// <summary>
        /// tick数据接收事件
        /// </summary>
        event DelegateOnDataReceived DataReceived;

        /// <summary>
        /// 数据接收触发器，该触发器会在每次数据接收后触发
        /// </summary>
        List<IMarketDataReceiveTragger> Traggers { get; }
    }

    /// <summary>
    /// 数据接收委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tickBar"></param>
    public delegate void DelegateOnDataReceived(object sender, ITickData tickData);
}