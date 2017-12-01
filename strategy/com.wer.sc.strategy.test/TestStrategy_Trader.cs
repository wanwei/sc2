using com.wer.sc.data;
using com.wer.sc.data.market;
using com.wer.sc.mockdata;
using com.wer.sc.strategy.mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Trade));
            executor.SetStrategy(strategy);
            executor.Run();

            IList<TradeInfo> tradeInfos = strategy.StrategyOperator.Trader.CurrentTradeInfo;
            AssertUtils.AssertEqual_List("StrategyTrade", GetType(), tradeInfos);
            Assert.AreEqual(98870, executor.StrategyReport.StrategyTrader.Account.Asset);
            //Assert.AreEqual(98870, strategy.StrategyOperator.Trader.OwnerTrader.Account.Asset);
            //IList<OrderInfo> orderInfos = strategy.StrategyOperator.Trader.CurrentOrderInfo;
            //for (int i = 0; i < tradeInfos.Count; i++)
            //{
            //    Console.WriteLine(tradeInfos[i]);
            //}
            //Console.WriteLine(strategy.StrategyOperator.Trader.OwnerTrader.Account.Asset);
        }
    }
}
