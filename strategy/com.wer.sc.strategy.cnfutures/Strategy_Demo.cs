using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.strategy.cnfutures.import;

namespace com.wer.sc.strategy.cnfutures
{
    public class Strategy_Demo : Strategy_ComplexAbstract
    {
        private StrategyReferedPeriods referedPeriods;

         

        private Strategy_Ma strategy_Ma_ = new Strategy_Ma(KLinePeriod.KLinePeriod_5Minute, 1);

        public Strategy_Demo()
        {
            referedPeriods = new StrategyReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);

            //this.ImportStrategies.Add()
        }

        public override List<IStrategy> ImportStrategies
        {
            get
            {
                return null;
            }
        }

        public override StrategyReferedPeriods GetStrategyPeriods_()
        {
            return referedPeriods;
        }

        public override void OnBar_(IRealTimeDataReader currentData)
        {

        }

        public override void OnTick_(IRealTimeDataReader currentData)
        {

        }


        public override void StrategyEnd_()
        {
            throw new NotImplementedException();
        }

        public override void StrategyStart_()
        {
            throw new NotImplementedException();
        }
    }
}