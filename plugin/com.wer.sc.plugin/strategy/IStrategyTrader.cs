using com.wer.sc.data.account;
using com.wer.sc.data.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略交易器
    /// </summary>
    public interface IStrategyTrader : IXmlExchange
    {
        /// <summary>
        /// 设置了该选项，则系统会按照一开一平信号执行
        /// 也就是开仓以后，在没有全部平仓时，系统会过滤掉后面的开仓信号
        /// </summary>
        bool AutoFilter { get; set; }

        /// <summary>
        /// 以当前价格发送开仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orderSide"></param>
        /// <param name="mount"></param>
        void Open(string code, OrderSide orderSide, int mount);

        /// <summary>
        /// 指定价格发送开仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="mount"></param>
        void Open(string code, OrderSide orderSide, float price, int mount);

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="percent"></param>
        void OpenPercent(string code, OrderSide orderSide, float percent);

        /// <summary>
        /// 指定价格，按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="percent"></param>
        void OpenPercent(string code, OrderSide orderSide, float price, float percent);

        /// <summary>
        /// 以当前价格全仓买入
        /// </summary>
        void OpenAll(string code, OrderSide orderSide);

        /// <summary>
        /// 指定价格全仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        void OpenAll(string code, OrderSide orderSide, float price);

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="mount"></param>
        void Close(string code, OrderSide orderSide, int mount);

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="percent"></param>
        void ClosePercent(string code, OrderSide orderSide, float percent);


        /// <summary>
        /// 以当前市价卖出code的所有持仓
        /// </summary>
        void CloseAll(string code, OrderSide orderSide);

        /// <summary>
        /// 
        /// </summary>
        void CloseAll(string code, OrderSide orderSide, float price);

        /// <summary>
        /// 以当前价格卖掉所有持仓
        /// </summary>
        void CloseAll();

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderid"></param>
        void CancelOrder(string orderid);

        /// <summary>
        /// 得到策略交易账户
        /// </summary>
        IAccount Account { get; }
    }
}