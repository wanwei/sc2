using com.wer.sc.data.datacenter;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public class TestMarket
    {
        //public static IMarketData GetMarket()
        //{
        //    IPlugin_Market plugin_market = PluginMgrFactory.DefaultPluginMgr.GetPluginObject<IPlugin_Market>("MARKET.CNFUTURES");
        //    List<ConnectionInfo> conns = plugin_market.MarketData.GetAllConnections();
        //    //AssertUtils.PrintLineList(conns);
        //    //IMarketData market = MarketFactory.CreateMarket(plugin_market, plugin_market.MarketData.GetAllConnections()[0]);
        //    //return market;
        //    return null;
        //}

        ///// <summary>
        ///// 接收器
        ///// </summary>
        //public void TestMarket_ReceiveAll()
        //{
        //    IMarketData market = GetMarket();

        //    IMarketData marketData = market.MarketData;
        //    IMarketTrader marketTrader = market.MarketTrader;

        //    //marketTrader.OnReturnInstruments = null;
        //    //自动写入数据
        //    TickDataReceiveTragger_Writer tragger_Writer = new TickDataReceiveTragger_Writer(@"d:\sctest\datareceiver\");
        //    market.MarketData.Traggers.Add(tragger_Writer);            

        //    market.MarketData.Connect();
        //    market.MarketTrader.Connect();
        //}



        ////[TestMethod]
        //public void TestTragger_RealTimeBuilder()
        //{
        //    IMarketData market = GetMarket();
        //    //market.SubscribeAll();

        //    //自动生成realtimedata数据
        //    DataCenter dataCenter = DataCenterManager.Create("").GetDataCenter("");
        //    TickDataReceiveTragger_RealTimeBuilder tragger_RealTime = new TickDataReceiveTragger_RealTimeBuilder(dataCenter.DataReader);
        //    market.MarketData.Traggers.Add(tragger_RealTime);
        //    tragger_RealTime.RealTimeDataChanged += Tragger_RealTime_RealTimeDataChanged;

        //    market.MarketTrader.Connect();
        //}

        //private void Tragger_RealTime_RealTimeDataChanged(object sender, reader.IRealTimeDataReader realTimeDataReader)
        //{
        //    String code = realTimeDataReader.GetCode();
        //    if (code.ToUpper().Equals("M05"))
        //    {
        //        Console.WriteLine(realTimeDataReader.GetTickData());
        //        Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        //        Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
        //        Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Hour));
        //        Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Day));

        //        Console.WriteLine(realTimeDataReader.GetTimeLineData());
        //    }
        //}

        ////[TestMethod]
        //public void TestReceiveTickData()
        //{
        //    IMarketData market = GetMarket();
        //    market.Subscribe(new string[] { "m05", "rb05" });
        //    market.OnDataReceived += DataReceiver_DataReceived;

        //    market.Connect();
        //    Thread.Sleep((int)(0.1 * 60 * 1000));
        //}

        //private void DataReceiver_DataReceived(object sender, ITickBar tickBar)
        //{
        //    Console.WriteLine(tickBar);
        //}
    }
}