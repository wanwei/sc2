﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy.common.sample
{
    [Strategy("STRATEGY.SAMPLE.QUERY", "测试查询", "测试查询", "例子")]
    public class Strategy_Query : StrategyAbstract
    {
        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.GetKLineData(MainKLinePeriod);
            if (klineData.BarPos == 0)
                return;
            float start = klineData.Start;
            float lastEnd = klineData.Arr_End[klineData.BarPos - 1];
            float upPercent = (start - lastEnd) * 100 / lastEnd;
            if (upPercent > 0.5 || upPercent < -0.5)
            {
                string code = klineData.Code;
                double time = klineData.Time;
                //IStrategyResult result = new StrategyResult(code, time);
                this.StrategyOperator.AddStrategyResult(code, time, "", "");
            }
        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {

        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {

        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {

        }
    }
}