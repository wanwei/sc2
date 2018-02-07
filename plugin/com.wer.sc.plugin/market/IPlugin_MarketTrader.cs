using com.wer.sc.data.market;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场交易接口
    /// </summary>
    public interface IPlugin_MarketTrader
    {
        /// <summary>
        /// 得到所有连接信息
        /// </summary>
        /// <returns></returns>
        List<ConnectionInfo> GetAllConnections();

        /// <summary>
        /// 连接市场服务器
        /// </summary>
        void Connect(ConnectionInfo connectionInfo);

        /// <summary>
        /// 设置或获取市场连接后的回调
        /// </summary>
        DelegateOnConnectionStatus OnConnectionStatus { get; set; }

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 查询品种
        /// </summary>
        /// <param name="instruments"></param>
        void QueryInstruments(string[] instruments = null);

        /// <summary>
        /// 返回合约信息
        /// </summary>
        DelegateOnReturnInstrument OnReturnInstruments { get; set; }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string SendOrder(OrderInfo order);

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        string CancelOrder(string orderid);

        /// <summary>
        /// 设置或获取交易委托回调
        /// </summary>
        DelegateOnReturnOrder OnReturnOrder { get; set; }

        /// <summary>
        /// 设置或获取成交回调
        /// </summary>
        DelegateOnReturnTrade OnReturnTrade { get; set; }

        /// <summary>
        /// 查询仓位信息
        /// </summary>
        void QueryPosition();

        /// <summary>
        /// 查询所有委托
        /// </summary>
        void QueryOrders();

        /// <summary>
        /// 查询所有交易
        /// </summary>
        void QueryTrades();

        /// <summary>
        /// 设置或获取持仓信息回调
        /// </summary>
        DelegateOnRspInvestorPosition OnRspInvestorPosition { get; set; }

        DelegateOnRspOrder OnRspOrder { get; set; }

        DelegateOnRspTrade OnRspTrade { get; set; }

        /// <summary>
        /// 查询账户信息
        /// </summary>
        void QueryAccount();

        /// <summary>
        /// 设置或获取账号信息回调
        /// </summary>
        DelegateOnReturnAccount OnReturnAccount { get; set; }
    }
}
