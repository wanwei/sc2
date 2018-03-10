using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    /// <summary>
    /// 该策略是为了测试策略执行结果的保存，生成一下数据
    /// 1.生成结果集，查找多头吞噬
    /// 2.账号：碰到多头吞噬即买入，然后赚或亏超过吞噬K线高度即平仓
    /// 3.绘图：绘出MA，吞噬的K线的rect
    /// </summary>
    public class MockStrategy_Results : StrategyAbstract
    {
        private IStrategyQueryResult strategyResult;

        private bool isOpen = false;

        private float openPrice;

        public MockStrategy_Results()
        {
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            string name = "测试结果集-多头吞噬";
            string[] titles = new string[] { "多头上涨幅度", "空头下跌幅度" };
            ObjectType[] types = new ObjectType[] { ObjectType.DOUBLE, ObjectType.DOUBLE };
            strategyResult = StrategyHelper.QueryResultManager.NewQueryResult(name, titles, types);
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            if (isOpen)
                return;
            IStrategyOnBarInfo barInfo = currentData.MainBar;
            IKLineBar currentBar = barInfo.KLineBar;
            IKLineBar lastBar = barInfo.KLineData.GetBar(barInfo.BarPos - 1);
            //多头吞噬
            if (currentBar.BlockHigh > lastBar.BlockHigh
                && currentBar.BlockLow < lastBar.BlockLow
                && currentBar.isRed()
                && currentBar.BlockHeight >= 20)
            {
                openPrice = currentBar.End;
                StrategyHelper.Trader.Open(currentData.Code, data.market.OrderSide.Buy, openPrice, 10);
                isOpen = true;
                strategyResult.AddRow(currentBar.Code, barInfo.KLineBar.Time, new object[] { currentBar.BlockHeight, lastBar.BlockHeight });
            }
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
            if (isOpen)
            {
                if (Math.Abs(currentData.Tick.TickBar.Price - openPrice) > 20)
                {
                    StrategyHelper.Trader.Close(currentData.Code, data.market.OrderSide.Sell, 10);
                    isOpen = false;
                }
            }
        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {

        }
    }
}