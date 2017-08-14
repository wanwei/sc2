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
    public class Strategy_Demo_MinuteImport : Strategy_ComplexAbstract
    {
        private StrategyReferedPeriods referedPeriods;

        private Strategy_MaList strategy_Ma_5Minute_5 = new Strategy_MaList(KLinePeriod.KLinePeriod_5Minute, 5);

        private List<IStrategy> importStrategies = new List<IStrategy>();

        public Strategy_Demo_MinuteImport()
        {
            referedPeriods = new StrategyReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);

            this.ImportStrategies.Add(strategy_Ma_5Minute_5);
        }

        public override List<IStrategy> ImportStrategies
        {
            get
            {
                return importStrategies;
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

        }

        public override void StrategyStart_()
        {

        }
    }
}