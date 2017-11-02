using com.wer.sc.data.market;
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
    public interface IStrategyTrader_Code
    {
        /// <summary>
        /// 得到
        /// </summary>
        IStrategyTrader OwnerTrader { get; }

        /// <summary>
        /// 得到该交易器对应的交易品种代码
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 设置了该选项，则系统会按照一开一平信号执行
        /// 也就是开仓以后，在没有全部平仓时，系统会过滤掉后面的开仓信号
        /// </summary>
        bool AutoFilter { get; set; }

        /// <summary>
        /// 以当前价格发送开仓委托
        /// </summary>
        /// <param name="order"></param>
        void Open(OrderSide orderSide, int mount);

        /// <summary>
        /// 指定价格发送开仓委托
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="mount"></param>
        void Open(OrderSide orderSide, float price, int mount);

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="percent"></param>
        void OpenPercent(OrderSide orderSide, float percent);

        /// <summary>
        /// 指定价格，按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="percent"></param>
        void OpenPercent(OrderSide orderSide, float price, float percent);

        /// <summary>
        /// 以当前价格全仓买入
        /// </summary>
        void OpenAll(OrderSide orderSide);

        /// <summary>
        /// 指定价格全仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        void OpenAll(OrderSide orderSide, float price);

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="mount"></param>
        void Close(OrderSide orderSide, int mount);

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="percent"></param>
        void ClosePercent(OrderSide orderSide, float percent);


        /// <summary>
        /// 
        /// </summary>
        void CloseAll(OrderSide orderSide);

        /// <summary>
        /// 
        /// </summary>
        void CloseAll(OrderSide orderSide, float price);

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
        /// 得到当前委托
        /// </summary>
        IList<OrderInfo> CurrentOrderInfo { get; }

        /// <summary>
        /// 当前持仓
        /// </summary>
        IList<PositionInfo> CurrentPositionInfo { get; }

        /// <summary>
        /// 当前交易信息
        /// </summary>
        IList<TradeInfo> CurrentTradeInfo { get; }
    }
}
