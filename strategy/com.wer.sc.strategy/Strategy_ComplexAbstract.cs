using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public abstract class Strategy_ComplexAbstract : StrategyAbstract, IStrategy
    {
        private StrategyReferedPeriods referedPeriods;

        public abstract List<IStrategy> ImportStrategies { get; }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            if (referedPeriods != null)
                return referedPeriods;
            referedPeriods = new StrategyReferedPeriods();
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    MergeReferedPeriods(referedPeriods, ImportStrategies[i].GetStrategyPeriods());
            StrategyReferedPeriods periods = GetStrategyPeriods_();
            MergeReferedPeriods(referedPeriods, periods);
            return referedPeriods;
        }

        private void MergeReferedPeriods(StrategyReferedPeriods periods, StrategyReferedPeriods mergePeriods)
        {
            if (referedPeriods == null)
                return;
            if (referedPeriods.isReferTimeLineData)
                periods.isReferTimeLineData = true;
            if (referedPeriods.UseTickData)
                periods.UseTickData = true;
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod klinePeriod = referedPeriods.UsedKLinePeriods[i];
                if (!periods.UsedKLinePeriods.Contains(klinePeriod))
                    periods.UsedKLinePeriods.Add(klinePeriod);
            }
        }

        public abstract StrategyReferedPeriods GetStrategyPeriods_();

        public override void OnBar(IRealTimeDataReader_Code currentData)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnBar(currentData);
            OnBar_(currentData);
        }

        public abstract void OnBar_(IRealTimeDataReader_Code currentData);

        public override void OnTick(IRealTimeDataReader_Code currentData)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnTick(currentData);
            OnTick_(currentData);
        }

        public abstract void OnTick_(IRealTimeDataReader_Code currentData);


        public override void StrategyEnd()
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].StrategyEnd();
            StrategyEnd_();
        }

        public abstract void StrategyEnd_();

        public override void StrategyStart()
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].StrategyStart();
            StrategyStart_();
        }

        public abstract void StrategyStart_();
    }
}