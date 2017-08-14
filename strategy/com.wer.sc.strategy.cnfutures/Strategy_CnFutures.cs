﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;

namespace com.wer.sc.strategy.cnfutures
{
    /// <summary>
    /// 策略：
    /// 看之前的趋势
    /// </summary>
    [Strategy("STRATEGY.CNFUTURES", "期货策略", "期货策略", "策略")]
    public class Strategy_CnFutures : StrategyAbstract, IStrategy
    {
        private StrategyReferedPeriods refered;

        //private List<>

        public Strategy_CnFutures()
        {
            refered = new StrategyReferedPeriods();
            refered.isReferTimeLineData = false;
            refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            //refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            //refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            refered.UseTickData = false;
        }

        public override void StrategyStart()
        {

        }

        public override void StrategyEnd()
        {

        }

        public override void OnBar(IRealTimeDataReader currentData)
        {
            IKLineData klineData = currentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            int currentBarPos = klineData.BarPos;
            int lastBarPos = currentBarPos - 5;
            if (lastBarPos < 0)
                return;

            //float endPrice = klineData.End;
            //float startPrice = klineData.GetBar(lastBarPos).Start;

            //int lowIndex = -1;
            //float lowPrice = float.MaxValue;
            //int highIndex = -1;
            //float highPrice = 0;
            //for (int i = lastBarPos; i <= currentBarPos; i++)
            //{

            //}

            //float percent = (float)Math.Round((highPrice - lowPrice) / startPrice * 100, 2);
            //5分钟内涨幅超过
            //if (percent > 0.5)
            //{

            //}
            //currentData.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
        }

        public override void OnTick(IRealTimeDataReader currentData)
        {

        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return refered;
        }
    }
}