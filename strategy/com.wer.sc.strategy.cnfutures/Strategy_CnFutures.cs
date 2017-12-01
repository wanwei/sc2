using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.data.forward;

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
            refered.UseTimeLineData = false;
            refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            //refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            //refered.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            refered.UseTickData = false;
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {

        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
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

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return refered;
        }
    }
}