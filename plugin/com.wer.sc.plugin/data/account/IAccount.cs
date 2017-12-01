using com.wer.sc.data.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 策略交易器
    /// </summary>
    public interface IAccount : IXmlExchange
    {
        /// <summary>
        /// 账号设置
        /// </summary>
        AccountSetting AccountSetting { get; }

        /// <summary>
        /// 得到交易费用
        /// </summary>
        ITradeFee Fee { get; }

        /// <summary>
        /// 得到账户的初始资金
        /// </summary>
        double InitMoney { get; }

        /// <summary>
        /// 得到账户现金金额
        /// </summary>
        double Money { get; }

        /// <summary>
        /// 得到账户现在的总资产
        /// </summary>
        double Asset { get; }

        /// <summary>
        /// 获得账号的描述信息
        /// </summary>
        String Description { get; }

        /// <summary>
        /// 以当前价格发送开仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="mount"></param>
        void Open(string code, double price, OrderSide orderSide, int mount);

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="percent"></param>
        void OpenPercent(string code, double price, OrderSide orderSide, float percent);

        /// <summary>
        /// 指定价格全仓买入
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        void OpenAll(string code, double price, OrderSide orderSide);

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="mount"></param>
        void Close(string code, double price, OrderSide orderSide, int mount);

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="percent"></param>
        void ClosePercent(string code, double price, OrderSide orderSide, float percent);

        /// <summary>
        /// 将指定品种平仓
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        void CloseAll(string code, double price, OrderSide orderSide);

        ///// <summary>
        ///// 以指定价格卖掉所有持仓
        ///// </summary>
        //void CloseAll(string code, double price);

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderid"></param>
        void CancelOrder(string orderid);

        /// <summary>
        /// 得到当前委托
        /// </summary>
        IList<OrderInfo> CurrentOrderInfo { get; }

        /// <summary>
        /// 得到历史委托
        /// </summary>
        IList<OrderInfo> HistoryOrderInfo { get; }

        /// <summary>
        /// 当前持仓
        /// </summary>
        IList<PositionInfo> CurrentPositionInfo { get; }

        /// <summary>
        /// 当前交易信息
        /// </summary>
        IList<TradeInfo> CurrentTradeInfo { get; }

        /// <summary>
        /// 委托成功事件
        /// </summary>
        event DelegateOnReturnOrder OnReturnOrder;

        /// <summary>
        /// 成交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="trade"></param>
        event DelegateOnReturnTrade OnReturnTrade;
    }
}
