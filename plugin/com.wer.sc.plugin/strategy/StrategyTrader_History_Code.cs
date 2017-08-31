using com.wer.sc.data.market;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTrader_History_Code : IStrategyTrader_Code
    {
        private StrategyTradeFee_Code tradeFee_Code;

        private string code;

        private double money;

        private IStrategyTrader strategyTrader = null;

        private IRealTimeDataReader realTimeDataReader;

        private bool autoFilter = false;

        private List<OrderInfo> orders = new List<OrderInfo>();

        public StrategyTrader_History_Code(string code, double money, IRealTimeDataReader realTimeDataReader, StrategyTradeFee_Code tradeFee_Code)
        {
            this.code = code;
            this.money = money;
            this.realTimeDataReader = realTimeDataReader;
            this.tradeFee_Code = tradeFee_Code;
        }

        /// <summary>
        /// 得到
        /// </summary>
        public IStrategyTrader OwnerTrader
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 得到该交易器对应的交易品种代码
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
        }

        /// <summary>
        /// 设置了该选项，则系统会按照一开一平信号执行
        /// 也就是开仓以后，在没有全部平仓时，系统会过滤掉后面的开仓信号
        /// </summary>
        public bool AutoFilter
        {
            get { return autoFilter; }
            set { this.autoFilter = value; }
        }

        /// <summary>
        /// 发送买入开仓委托
        /// </summary>
        /// <param name="order"></param>
        public void Open(OrderSide orderSide, int mount)
        {
            //this.realTimeDataReader
        }

        public void Open(OrderSide orderSide, float price, int mount)
        {

        }

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="percent"></param>
        public void OpenPercent(OrderSide orderSide, float percent)
        {

        }

        public void OpenPercent(OrderSide orderSide, float price, float percent)
        {

        }

        public void OpenAll(OrderSide orderSide, float price)
        {

        }

        /// <summary>
        /// 全仓买入
        /// </summary>
        public void OpenAll(OrderSide orderSide)
        {

        }

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="mount"></param>
        public void Close(OrderSide orderSide, int mount)
        {

        }

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="percent"></param>
        public void ClosePercent(OrderSide orderSide, float percent)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public void CloseAll(OrderSide orderSide)
        {

        }

        /// <summary>
        /// 卖掉所有持仓
        /// </summary>
        public void CloseAll()
        {
        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderid"></param>
        public void CancelOrder(string orderid)
        {

        }

        public void CloseAll(OrderSide orderSide, float price)
        {

        }

        /// <summary>
        /// 得到当前委托
        /// </summary>
        public IList<OrderInfo> CurrentOrderInfo
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 当前持仓
        /// </summary>
        public IList<PositionInfo> CurrentPositionInfo
        {
            get { return null; }
        }
        /// <summary>
        /// 当前交易信息
        /// </summary>
        public IList<TradeInfo> CurrentTradeInfo
        {
            get { return null; }
        }
    }
}