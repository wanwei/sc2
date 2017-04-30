using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    /// <summary>
    /// 数据接收器
    /// 该接口用于接收数据
    /// </summary>
    public interface IDataReceiver2
    {
        /// <summary>
        /// 订阅所有品种
        /// </summary>
        void SubscribeAll();

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
        /// 得到所有连接
        /// </summary>
        /// <returns></returns>
        List<ConnectionInfo> GetAllConnections();

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="connectionInfo"></param>
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
        /// 
        /// </summary>
        DelegateOnConnectionStatus OnMarketDataConnectionStatus { get; set; }


        DelegateOnConnectionStatus OnMarketTraderConnectionStatus { get; set; }

        DelegateOnReturnMarketData OnReturnMarketData { get; set; }

    }
}
