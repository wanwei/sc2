using com.wer.sc.data;
using com.wer.sc.data.market;
using com.wer.sc.mockdata;
using com.wer.sc.strategy.mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategy_Trader
    {
        [TestMethod]
        public void TestTrade()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170603;

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory_History().CreateExecutor(code, startDate, endDate, referedPeriods, forwardPeriod, null);

            StrategyAbstract strategy = (StrategyAbstract)StrategyGetter.GetStrategy(typeof(MockStrategy_Trade));
            executor.SetStrategy(strategy);
            executor.Run();

            StrategyTrader_History trader =((StrategyTrader_History)strategy.StrategyOperator.Trader);
            IList<TradeInfo> tradeInfos = trader.Account.CurrentTradeInfo;
            AssertUtils.PrintLineList((IList)tradeInfos);
            AssertUtils.AssertEqual_List("StrategyTrade", GetType(), tradeInfos);
            Assert.AreEqual(96250, trader.Account.Asset);
            //Assert.AreEqual(98870, trader.Account.Asset);
            //IList<OrderInfo> orderInfos = strategy.StrategyOperator.Trader.CurrentOrderInfo;
            //for (int i = 0; i < tradeInfos.Count; i++)
            //{
            //    Console.WriteLine(tradeInfos[i]);
            //}
            //Console.WriteLine(strategy.StrategyOperator.Trader.OwnerTrader.Account.Asset);
        }

        [TestMethod]
        public void TestTrade2()
        {
            string code = "RB1710";
            int startDate = 20170101;
            int endDate = 20170603;

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory_History().CreateExecutor(code, startDate, endDate, referedPeriods, forwardPeriod, null);

            StrategyAbstract strategy = (StrategyAbstract)StrategyGetter.GetStrategy(typeof(MockStrategy_Trade));
            executor.SetStrategy(strategy);
            executor.Run();

            StrategyTrader_History trader = ((StrategyTrader_History)strategy.StrategyOperator.Trader);
            IList<TradeInfo> tradeInfos = trader.Account.CurrentTradeInfo;
            AssertUtils.PrintLineList((IList)tradeInfos);
            Console.WriteLine(trader.Account.Asset);
            //AssertUtils.AssertEqual_List("StrategyTrade", GetType(), tradeInfos);
            //Assert.AreEqual(98870, trader.Account.Asset);
            //Assert.AreEqual(98870, trader.Account.Asset);
            //IList<OrderInfo> orderInfos = strategy.StrategyOperator.Trader.CurrentOrderInfo;
            //for (int i = 0; i < tradeInfos.Count; i++)
            //{
            //    Console.WriteLine(tradeInfos[i]);
            //}
            //Console.WriteLine(strategy.StrategyOperator.Trader.OwnerTrader.Account.Asset);
        }
    }
}
