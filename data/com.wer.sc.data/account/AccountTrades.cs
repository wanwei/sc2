using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    public class AccountTrades
    {
        private Account account;

        private List<TradeInfo> historyTrades = new List<TradeInfo>();

        private List<TradeInfo> currentTrades = new List<TradeInfo>();

        public AccountTrades(Account account)
        {
            this.account = account;
        }

        public void AddTradeInfo(OrderInfo orderInfo, int tradeMount)
        {
            TradeInfo tradeInfo = new TradeInfo();
            tradeInfo.InstrumentID = orderInfo.Instrumentid;
            tradeInfo.OpenClose = orderInfo.OpenClose;
            //TODO 应该是当前价格
            tradeInfo.Price = orderInfo.Price;
            tradeInfo.Qty = tradeMount;
            tradeInfo.Side = orderInfo.Direction;
            tradeInfo.Time = account.Time;
            tradeInfo.TradeID = Guid.NewGuid().ToString();
            this.currentTrades.Add(tradeInfo);
        }

        private int GetTradeMount(OrderInfo orderInfo)
        {
            double orderPrice = orderInfo.Price;


            //ITickData tickData = realTimeDataReader.GetTickData();
            //if (tickData != null)
            //{
            //    if (orderInfo.Direction == OrderSide.Buy)
            //    {
            //        if (orderInfo.Price < tickData.BuyPrice)
            //            return 0;
            //        return tickData.BuyMount >= orderInfo.LeavesQty ? orderInfo.LeavesQty : tickData.BuyMount;
            //    }
            //    else
            //    {
            //        if (orderInfo.Price > tickData.SellPrice)
            //            return 0;
            //        return tickData.SellMount >= orderInfo.LeavesQty ? orderInfo.LeavesQty : tickData.SellMount;
            //    }
            //}
            //else
            //{
            //    double price = realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
            //    if (orderInfo.Direction == OrderSide.Buy)
            //        return orderInfo.Price >= price ? orderInfo.Volume : 0;
            //    else
            //        return orderInfo.Price <= price ? orderInfo.Volume : 0;
            //}
            return -1;
        }

        public void DoTradingDayChange()
        {

        }
    }
}
