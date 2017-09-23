﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.strategy.common.ma;

namespace com.wer.sc.strategy.common.sample
{
    /// <summary>
    /// 日线：
    /// </summary>
    [Strategy("STRATEGY.SAMPLE.TRADER", "测试交易策略", "测试交易策略", "例子")]
    public class Strategy_Trader : StrategyAbstract
    {
        private Strategy_Ma strategy_MA_1Minute;
        private Strategy_Ma strategy_MA_15Minute;

        private List<IStrategy> referedStrategies = new List<IStrategy>();

        public Strategy_Trader()
        {
            strategy_MA_1Minute = new Strategy_Ma();
            strategy_MA_1Minute.DefaultMainPeriod = KLinePeriod.KLinePeriod_1Minute;

            strategy_MA_15Minute = new Strategy_Ma();
            strategy_MA_15Minute.DefaultMainPeriod = KLinePeriod.KLinePeriod_1Minute;
            this.referedStrategies.Add(strategy_MA_1Minute);
            this.referedStrategies.Add(strategy_MA_15Minute);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            StrategyReferedPeriods referPeriods = new StrategyReferedPeriods();
            referPeriods.UseTickData = false;
            referPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            return referPeriods;
        }

        public override void OnBar(IRealTimeDataReader currentData)
        {
            //15分钟K线图ma5>ma10
            if (strategy_MA_15Minute.MAPrice_1 > strategy_MA_15Minute.MAPrice_2)
            {
                if (strategy_MA_1Minute.MAPrice_2 > strategy_MA_1Minute.MAPrice_3)
                {
                    StrategyHelper.Trader.Open(data.market.OrderSide.Buy, 10);
                }
                else
                    StrategyHelper.Trader.Close(data.market.OrderSide.Sell, 10);
            }
            else
            {
                StrategyHelper.Trader.Close(data.market.OrderSide.Sell, 10);
            }
        }

        public override void OnTick(IRealTimeDataReader currentData)
        {

        }

        public override void StrategyEnd()
        {

        }

        public override void StrategyStart()
        {
            StrategyHelper.Trader.AutoFilter = true;

            //strategy_MA_1Hour = new Strategy_Ma();
            //strategy_MA_1Hour.DefaultMainPeriod = KLinePeriod.KLinePeriod_1Hour;

            //strategy_MA_1Day = new Strategy_Ma();
            //strategy_MA_1Day.DefaultMainPeriod = KLinePeriod.KLinePeriod_1Day;
        }
    }
}
