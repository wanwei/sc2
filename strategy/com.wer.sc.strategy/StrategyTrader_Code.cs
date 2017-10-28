using com.wer.sc.data;
using com.wer.sc.data.market;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTrader_Code : IStrategyTrader_Code
    {
        private StrategyTrader strategyTrader;

        private string code;

        private bool autoFilter;

        public StrategyTrader_Code(StrategyTrader strategyTrader, string code)
        {
            this.strategyTrader = strategyTrader;
            this.code = code;
        }

        /// <summary>
        /// 得到
        /// </summary>
        public IStrategyTrader OwnerTrader
        {
            get
            {
                return strategyTrader;
            }
        }

        /// <summary>
        /// 得到该交易器对应的交易品种代码
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// 设置了该选项，则系统会按照一开一平信号执行
        /// 也就是开仓以后，在没有全部平仓时，系统会过滤掉后面的开仓信号
        /// </summary>
        public bool AutoFilter
        {
            get
            {
                return autoFilter;
            }
            set
            {
                this.autoFilter = value;
            }
        }

        /// <summary>
        /// 以当前价格发送开仓委托
        /// </summary>
        /// <param name="order"></param>
        public void Open(OrderSide orderSide, int mount)
        {
            this.strategyTrader.Account.Open(code, GetPrice(orderSide), orderSide, mount);
        }

        private double GetPrice(OrderSide orderSide)
        {
            return orderSide == OrderSide.Buy ? SellPrice : BuyPrice;
        }

        public double BuyPrice
        {
            get
            {
                ITickData tickData = this.strategyTrader.RealTimeDataReader.GetTickData();
                if (tickData != null)
                    return tickData.BuyPrice;
                return this.strategyTrader.RealTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
            }
        }

        public double SellPrice
        {
            get
            {
                ITickData tickData = this.strategyTrader.RealTimeDataReader.GetTickData();
                if (tickData != null)
                    return tickData.SellPrice;
                return this.strategyTrader.RealTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
            }
        }

        /// <summary>
        /// 指定价格发送开仓委托
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="mount"></param>
        public void Open(OrderSide orderSide, float price, int mount)
        {
            this.strategyTrader.Account.Open(code, price, orderSide, mount);
        }

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="percent"></param>
        public void OpenPercent(OrderSide orderSide, float percent)
        {
            this.strategyTrader.Account.OpenPercent(code, GetPrice(orderSide), orderSide, percent);
        }

        /// <summary>
        /// 指定价格，按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="percent"></param>
        public void OpenPercent(OrderSide orderSide, float price, float percent)
        {
            this.strategyTrader.Account.OpenPercent(code, price, orderSide, percent);
        }

        /// <summary>
        /// 以当前价格全仓买入
        /// </summary>
        public void OpenAll(OrderSide orderSide)
        {
            this.strategyTrader.Account.OpenAll(code, GetPrice(orderSide), orderSide);
        }

        /// <summary>
        /// 指定价格全仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        public void OpenAll(OrderSide orderSide, float price)
        {
            this.strategyTrader.Account.OpenAll(code, price, orderSide);
        }

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="mount"></param>
        public void Close(OrderSide orderSide, int mount)
        {
            this.strategyTrader.Account.Close(code, GetPrice(orderSide), orderSide, mount);
        }

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="percent"></param>
        public void ClosePercent(OrderSide orderSide, float percent)
        {
            this.strategyTrader.Account.ClosePercent(code, GetPrice(orderSide), orderSide, percent);
        }


        /// <summary>
        /// 
        /// </summary>
        public void CloseAll(OrderSide orderSide)
        {
            this.strategyTrader.Account.CloseAll(code, GetPrice(orderSide), orderSide);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseAll(OrderSide orderSide, float price)
        {
            this.strategyTrader.Account.CloseAll(code, price, orderSide);
        }

        /// <summary>
        /// 以当前价格卖掉所有持仓
        /// </summary>
        public void CloseAll()
        {
            CloseAll(OrderSide.Buy);
            CloseAll(OrderSide.Sell);
        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderid"></param>
        public void CancelOrder(string orderid)
        {
            this.strategyTrader.Account.CancelOrder(orderid);
        }

        /// <summary>
        /// 得到当前委托
        /// </summary>
        public IList<OrderInfo> CurrentOrderInfo
        {
            get
            {
                return this.strategyTrader.Account.CurrentOrderInfo;
            }
        }

        /// <summary>
        /// 当前持仓
        /// </summary>
        public IList<PositionInfo> CurrentPositionInfo
        {
            get
            {
                return this.strategyTrader.Account.CurrentPositionInfo;
            }
        }

        /// <summary>
        /// 当前交易信息
        /// </summary>
        public IList<TradeInfo> CurrentTradeInfo
        {
            get
            {
                return this.strategyTrader.Account.CurrentTradeInfo;
            }
        }
    }
}