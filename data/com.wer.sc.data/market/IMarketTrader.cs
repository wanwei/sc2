using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public interface IMarketTrader
    {
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
        /// 查询品种信息
        /// </summary>
        /// <param name="instruments"></param>
        void QueryInstruments(string[] instruments = null);

        /// <summary>
        /// 返回合约信息
        /// </summary>
        event DelegateOnReturnInstrument InstrumentsReturned;

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
        event DelegateOnReturnOrder OrderReturned;

        /// <summary>
        /// 设置或获取成交回调
        /// </summary>
        event DelegateOnReturnTrade TradeReturned;

        /// <summary>
        /// 查询仓位信息
        /// </summary>
        void QueryPosition();

        /// <summary>
        /// 设置或获取持仓信息回调
        /// </summary>
        event DelegateOnReturnInvestorPosition InvestorPositionReturned;

        /// <summary>
        /// 查询账户信息
        /// </summary>
        void QueryAccount();

        /// <summary>
        /// 设置或获取账号信息回调
        /// </summary>
        event DelegateOnReturnAccount AccountReturned;
    }
}