﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.strategy.common.ma;
using com.wer.sc.data;

namespace com.wer.sc.strategy.common.sample
{
    [Strategy("STRATEGY.SAMPLE.REFER", "测试引用", "测试引用", "例子")]
    public class Strategy_Refer : StrategyAbstract
    {
        private IList<IStrategy> referedStrategies = new List<IStrategy>();
        private Strategy_MultiMa strategy_MA_1Minute;
        private Strategy_MultiMa strategy_MA_15Minute;

        public Strategy_Refer()
        {
            strategy_MA_1Minute = new Strategy_MultiMa();
            strategy_MA_1Minute.MainKLinePeriod = KLinePeriod.KLinePeriod_1Minute;

            strategy_MA_15Minute = new Strategy_MultiMa();
            strategy_MA_15Minute.MainKLinePeriod = KLinePeriod.KLinePeriod_15Minute;
            referedStrategies.Add(strategy_MA_1Minute);
            referedStrategies.Add(strategy_MA_15Minute);
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            //Console(strategy_MA_1Minute.)
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {

        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return this.referedStrategies;
        }
    }
}
