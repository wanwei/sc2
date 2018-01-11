using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.daytrade
{
    public class Strategy_DayTrade : StrategyAbstract
    {
        private double prevTime = -1;

        //白天开盘时间
        private double dayStartTime = 0.09;

        private int barPos;

        private double currentDayStart;

        private Dictionary<KLinePeriod, int> dic_Period_DayStartPos = new Dictionary<KLinePeriod, int>();

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            //计算中枢价格
            if (prevTime <= 0)
                prevTime = currentData.Time;
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
            ITickData tickData = currentData.Tick.TickData;
            if (tickData.BarPos == 0)
            {

            }
        }
    }
}