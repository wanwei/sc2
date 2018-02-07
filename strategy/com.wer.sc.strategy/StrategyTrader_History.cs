using com.wer.sc.data;
using com.wer.sc.data.account;
using com.wer.sc.data.market;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTrader_History : IStrategyTrader
    {
        private IAccount account;

        public IAccount Account
        {
            get { return account; }
        }

        public StrategyTrader_History(IAccount account)
        {
            this.account = account;
        }

        /// <summary>
        /// 设置了该选项，则系统会按照一开一平信号执行
        /// 也就是开仓以后，在没有全部平仓时，系统会过滤掉后面的开仓信号
        /// </summary>
        public bool AutoFilter
        {
            get
            {
                return account.AccountSetting.AutoFilter;
            }
            set
            {
                account.AccountSetting.AutoFilter = value;
            }
        }

        /// <summary>
        /// 以当前价格发送开仓委托
        /// </summary>
        /// <param name="order"></param>
        public void Open(string code, OrderSide orderSide, int mount)
        {
            this.Account.Open(code, GetPrice(code, orderSide), orderSide, mount);
        }

        private double GetPrice(String code, OrderSide orderSide)
        {
            return orderSide == OrderSide.Buy ? GetSellPrice(code) : GetBuyPrice(code);
        }

        public double GetBuyPrice(string code)
        {

            ITickData tickData = this.Account.RealTimeDataReader.GetRealTimeData(code).GetTickData();
            if (tickData != null)
                return tickData.BuyPrice;
            return this.Account.RealTimeDataReader.GetRealTimeData(code).GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
        }

        public double GetSellPrice(string code)
        {
            ITickData tickData = this.Account.RealTimeDataReader.GetRealTimeData(code).GetTickData();
            if (tickData != null)
                return tickData.SellPrice;
            return GetRealTimeData_Code(code).GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
        }

        private IRealTimeData_Code GetRealTimeData_Code(string code)
        {
            return this.Account.RealTimeDataReader.GetRealTimeData(code);
        }


        /// <summary>
        /// 指定价格发送开仓委托
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="mount"></param>
        public void Open(string code, OrderSide orderSide, float price, int mount)
        {
            this.Account.Open(code, price, orderSide, mount);
        }

        /// <summary>
        /// 按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="percent"></param>
        public void OpenPercent(string code, OrderSide orderSide, float percent)
        {
            this.Account.OpenPercent(code, GetPrice(code, orderSide), orderSide, percent);
        }

        /// <summary>
        /// 指定价格，按照资金比例买入，如传入50，则是半仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        /// <param name="percent"></param>
        public void OpenPercent(string code, OrderSide orderSide, float price, float percent)
        {
            this.Account.OpenPercent(code, price, orderSide, percent);
        }

        /// <summary>
        /// 以当前价格全仓买入
        /// </summary>
        public void OpenAll(string code, OrderSide orderSide)
        {
            this.Account.OpenAll(code, GetPrice(code, orderSide), orderSide);
        }

        /// <summary>
        /// 指定价格全仓买入
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="price"></param>
        public void OpenAll(string code, OrderSide orderSide, float price)
        {
            this.Account.OpenAll(code, price, orderSide);
        }

        /// <summary>
        /// 发送卖出开仓委托
        /// </summary>
        /// <param name="mount"></param>
        public void Close(string code, OrderSide orderSide, int mount)
        {
            this.Account.Close(code, GetPrice(code, orderSide), orderSide, mount);
        }

        /// <summary>
        /// 按照持仓比例卖出，如传入50，则卖掉一半
        /// </summary>
        /// <param name="percent"></param>
        public void ClosePercent(string code, OrderSide orderSide, float percent)
        {
            this.Account.ClosePercent(code, GetPrice(code, orderSide), orderSide, percent);
        }


        /// <summary>
        /// 
        /// </summary>
        public void CloseAll(string code, OrderSide orderSide)
        {
            this.Account.CloseAll(code, GetPrice(code, orderSide), orderSide);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseAll(string code, OrderSide orderSide, float price)
        {
            this.Account.CloseAll(code, price, orderSide);
        }

        /// <summary>
        /// 以当前价格卖掉所有持仓
        /// </summary>
        public void CloseAll()
        {
            IList<PositionInfo> positions = Account.CurrentPositionInfo;
            for(int i = 0; i < positions.Count; i++)
            {
                PositionInfo position = positions[i];
                CloseAll(position.InstrumentID, position.Side == PositionSide.Long ? OrderSide.Sell : OrderSide.Buy);
            }
        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderid"></param>
        public void CancelOrder(string orderid)
        {
            this.Account.CancelOrder(orderid);
        }
    }
}