using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 单张合约交易器
    /// </summary>
    public class MarketTrader_Code
    {
        //交易所在的账号
        //private IAccount2 tradeAccount;

        ////历史委托，每过一天就将所有合约放入历史委托里
        //private List<OrderInfo> historyOrderInfos;

        ////今日委托
        //private List<OrderInfo> currentOrderInfos;

        ////多仓持仓
        //private PositionInfo positionInfo_Buy = new PositionInfo();

        ////空仓持仓
        //private PositionInfo positionInfo_Sell = new PositionInfo();

        //private IHistoryDataForward_Code historyDataForward;

        //public MarketTrader_Code(IAccount2 account, IHistoryDataForward_Code historyDataForward)
        //{
        //    this.historyDataForward = historyDataForward;
        //    if (this.historyDataForward.ForwardPeriod.IsTickForward)
        //        this.historyDataForward.OnTick += HistoryDataForward_OnTick;
        //    else
        //        this.historyDataForward.OnBar += HistoryDataForward_OnBar;
        //    this.currentOrderInfos = new List<OrderInfo>();
        //    this.historyOrderInfos = new List<OrderInfo>();
        //}

        //public MarketTrader_Code(double money, IHistoryDataForward_Code historyDataForward) : this(new Account2(money), historyDataForward)
        //{
        //}

        //private void HistoryDataForward_OnTick(object sender, ITickData tickData, int index)
        //{
        //    for (int i = 0; i < currentOrderInfos.Count; i++)
        //    {
        //        OrderInfo orderInfo = currentOrderInfos[i];
        //        DoTrade_Tick(orderInfo, tickData);
        //    }
        //}

        //private void DoTrade_Tick(OrderInfo orderInfo, ITickData tickData)
        //{
        //    if (orderInfo.OpenClose == OpenCloseType.Open)
        //    {
        //        DoTrade_Tick_Open(orderInfo, tickData);
        //    }
        //    else
        //    {
        //        DoTrade_Tick_Close(orderInfo, tickData);
        //    }
        //}

        //private void DoTrade_Tick_Open(OrderInfo orderInfo, ITickData tickData)
        //{
        //    if (orderInfo.Direction == OrderSide.Buy)
        //    {
        //        if (orderInfo.Price >= tickData.SellPrice)
        //        {
        //            int qty = orderInfo.LeavesQty;
        //            int tradeMount = qty <= tickData.SellMount ? qty : tickData.SellMount;

        //            orderInfo.CumQty += tradeMount;
        //            orderInfo.LeavesQty -= tradeMount;
        //        }
        //    }
        //    else
        //    {
        //        if (orderInfo.Price <= tickData.BuyPrice)
        //        {
        //            int qty = orderInfo.LeavesQty;
        //            int tradeMount = qty <= tickData.BuyMount ? qty : tickData.BuyMount;

        //            orderInfo.CumQty += tradeMount;
        //            orderInfo.LeavesQty -= tradeMount;
        //        }
        //    }
        //}

        //private void DoTrade_Tick_Close(OrderInfo orderInfo, ITickData tickData)
        //{
        //    if (orderInfo.Direction == OrderSide.Buy)
        //    {
        //        if (orderInfo.Price >= tickData.SellPrice)
        //        {
        //            int qty = orderInfo.LeavesQty;
        //            int tradeMount = qty <= tickData.SellMount ? qty : tickData.SellMount;

        //            orderInfo.CumQty += tradeMount;
        //            orderInfo.LeavesQty -= tradeMount;
        //        }
        //    }
        //    else
        //    {
        //        if (orderInfo.Price <= tickData.BuyPrice)
        //        {
        //            int qty = orderInfo.LeavesQty;
        //            int tradeMount = qty <= tickData.BuyMount ? qty : tickData.BuyMount;

        //            orderInfo.CumQty += tradeMount;
        //            orderInfo.LeavesQty -= tradeMount;
        //        }
        //    }
        //}

        //private void HistoryDataForward_OnBar(object sender, IKLineData klineData, int index)
        //{

        //}

        //private void DoTrade_KLine(OrderInfo orderInfo, IKLineData klineData)
        //{
        //    if (orderInfo.Direction == OrderSide.Buy)
        //    {
        //        int qty = orderInfo.LeavesQty;
        //    }
        //    else
        //    {

        //    }
        //}

        //public void SendOrder(OrderInfo order)
        //{
        //    this.currentOrderInfos.Add(order);

        //    //按照当前价格执行一次
        //    ITickData tickData = historyDataForward.GetTickData();
        //    if (tickData != null)
        //        DoTrade_Tick(order, tickData);
        //    else
        //        DoTrade_KLine(order, historyDataForward.GetKLineData());
        //}

        //public void CancelOrder(OrderInfo order)
        //{
        //    order.LeavesQty = 0;
        //    if (onReturnOrder != null)
        //        onReturnOrder(this, ref order);
        //}

        //public void QueryPosition()
        //{

        //}

        //private DelegateOnReturnOrder onReturnOrder;

        //public DelegateOnReturnOrder OnReturnOrder
        //{
        //    get
        //    {
        //        return onReturnOrder;
        //    }

        //    set
        //    {
        //        this.onReturnOrder = value;
        //    }
        //}

        //private DelegateOnReturnTrade onReturnTrade;

        //public DelegateOnReturnTrade OnReturnTrade
        //{
        //    get
        //    {
        //        return onReturnTrade;
        //    }

        //    set
        //    {
        //        onReturnTrade = value;
        //    }
        //}

        //private DelegateOnReturnInvestorPosition onReturnInvestorPosition;

        //public DelegateOnReturnInvestorPosition OnReturnInvestorPosition
        //{
        //    get
        //    {
        //        return onReturnInvestorPosition;
        //    }

        //    set
        //    {
        //        this.onReturnInvestorPosition = value;
        //    }
        //}

        //public PositionInfo PositionInfo_Buy
        //{
        //    get
        //    {
        //        return positionInfo_Buy;
        //    }
        //}

        //public PositionInfo PositionInfo_Sell
        //{
        //    get
        //    {
        //        return positionInfo_Sell;
        //    }
        //}

        //public void SaveToXml(XmlElement xmlelem)
        //{
        //    xmlelem.SetAttribute("money", this.tradeAccount.Available.ToString());
        //}
    }
}