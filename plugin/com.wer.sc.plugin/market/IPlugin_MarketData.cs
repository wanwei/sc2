using com.wer.sc.data.market;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场数据接口
    /// </summary>
    public interface IPlugin_MarketData
    {
        /// <summary>
        /// 得到单支股票或期货的交易时间
        /// </summary>
        /// <returns></returns>
        List<double[]> GetTradingSession(string code, int date);        

        /// <summary>
        /// 得到所有的连接信息
        /// </summary>
        /// <returns></returns>
        List<ConnectionInfo> GetAllConnections();

        /// <summary>
        /// 连接市场服务器
        /// </summary>
        void Connect(ConnectionInfo connectionInfo);

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 连接状态改变
        /// </summary>
        DelegateOnConnectionStatus OnConnectionStatus { get; set; }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="codes"></param>
        void Subscribe(string[] codes);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="codes"></param>
        void UnSubscribe(string[] codes);

        /// <summary>
        /// 订阅数据返回
        /// </summary>
        DelegateOnReturnMarketData OnReturnMarketData { get; set; }
    }
}
