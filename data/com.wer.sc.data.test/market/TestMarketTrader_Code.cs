using com.wer.sc.data.forward;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    [TestClass]
    public class TestMarketTrader_Code
    {
        //private MarketTrader_Code marketTrader;
        //[TestMethod]
        //public void TestTrade_Code()
        //{
        //    string code = "rb1710";
        //    int startDate = 20170601;
        //    int endDate = 20170601;
        //    IHistoryDataForward_Code historyDataForward = CommonData.GetRealTimeReader(code, startDate, endDate, true);
        //    marketTrader = new MarketTrader_Code(100000, historyDataForward);

        //    historyDataForward.OnTick += HistoryDataForward_OnTick;

        //    while (historyDataForward.Forward())
        //    {

        //    }

        //    Console.WriteLine(marketTrader);
        //}

        //private void HistoryDataForward_OnTick(object sender, ITickData tickData, int index)
        //{
        //    if (tickData.Price >= 3115)
        //    {
        //        OrderInfo order = new OrderInfo(tickData.Code, 20170601.093101, OpenCloseType.Open, 3115, 10, OrderSide.Buy);
        //        marketTrader.SendOrder(order);
        //    }
        //}
    }

    //class MockTrade
    //{
    //    private bool isBuyed;

    //    private MarketTrader_Code marketTrader;

    //    public void Run()
    //    {
    //        string code = "rb1710";
    //        int startDate = 20170601;
    //        int endDate = 20170601;
    //        IHistoryDataForward_Code historyDataForward = CommonData.GetRealTimeReader(code, startDate, endDate, true);
    //        marketTrader = new MarketTrader_Code(100000, historyDataForward);

    //        historyDataForward.OnTick += HistoryDataForward_OnTick;

    //        while (historyDataForward.Forward())
    //        {

    //        }

    //        Console.WriteLine(marketTrader);
    //    }

    //    private void HistoryDataForward_OnTick(object sender, ITickData tickData, int index)
    //    {
    //        if (!isBuyed)
    //        {
    //            if (tickData.Price >= 3115)
    //            {
    //                OrderInfo order = new OrderInfo(tickData.Code, 20170601.093101, OpenCloseType.Close, 3115, 10, OrderSide.Sell);
    //                marketTrader.SendOrder(order);
    //            }
    //        }
    //        else
    //        {
    //            if (tickData.Price >= 3110)
    //            {
    //                OrderInfo order = new OrderInfo(tickData.Code, 20170601.093101, OpenCloseType.Open, 3110, 10, OrderSide.Buy);
    //                marketTrader.SendOrder(order);
    //            }
    //        }
    //    }
    //}
}