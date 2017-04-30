using com.wer.sc.data.datacenter;
using com.wer.sc.data.market.receiver;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    [TestClass]
    public class TestMarketData
    {
        //[TestMethod]
        public void TestTragger_Writer()
        {
            MarketFactory fac = new MarketFactory(MarketType.CnFutures);
            IMarket market = fac.CreateMarket();
            IMarketData marketData = market.MarketData;

            marketData.ConnectionStatusChanged += MarketData_ConnectionStatusChanged;
            //订阅所有数据
            //market.SubscribeAll();

            //自动写入数据
            MarketDataReceiveTragger_TickWriter tragger_Writer = new MarketDataReceiveTragger_TickWriter(@"d:\sctest\datareceiver\", 200);
            market.MarketData.Traggers.Add(tragger_Writer);

            //market.Connect();
            //Thread.Sleep(1 * 60 * 1000);
        }

        private void MarketData_ConnectionStatusChanged(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {

        }


        //[TestMethod]
        public void TestTragger_RealTimeBuilder()
        {
            //IMarketData market = GetMarket();
            ////market.SubscribeAll();

            ////自动生成realtimedata数据
            //DataCenter dataCenter = DataCenterManager.Create("").GetDataCenter("");
            //TickDataReceiveTragger_RealTimeBuilder tragger_RealTime = new TickDataReceiveTragger_RealTimeBuilder(dataCenter.DataReader);
            //market.Traggers.Add(tragger_RealTime);
            //tragger_RealTime.RealTimeDataChanged += Tragger_RealTime_RealTimeDataChanged;

            //market.Connect();
            //Thread.Sleep(1 * 60 * 1000);
        }

        private void Tragger_RealTime_RealTimeDataChanged(object sender, reader.IRealTimeDataReader realTimeDataReader)
        {
            String code = realTimeDataReader.GetCode();
            if (code.ToUpper().Equals("M05"))
            {
                Console.WriteLine(realTimeDataReader.GetTickData());
                Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
                Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
                Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Hour));
                Console.WriteLine(realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Day));

                Console.WriteLine(realTimeDataReader.GetTimeLineData());
            }
        }

        //[TestMethod]
        public void TestReceiveTickData()
        {
            //IMarketData market = GetMarket();
            //market.Subscribe(new string[] { "m05", "rb05" });
            //market.OnDataReceived += DataReceiver_DataReceived;

            //market.Connect();
            //Thread.Sleep((int)(0.1 * 60 * 1000));
        }

        private void DataReceiver_DataReceived(object sender, ITickBar tickBar)
        {
            Console.WriteLine(tickBar);
        }
    }
}