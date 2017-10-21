using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.data.forward;

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
            if (referedPeriods.UseTimeLineData)
                periods.UseTimeLineData = true;
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

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnBar(sender, currentData);
            OnBar_(currentData);
        }

        public abstract void OnBar_(IRealTimeDataReader_Code currentData);

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnTick(sender, currentData);
            OnTick_(currentData);
        }

        public abstract void OnTick_(IRealTimeDataReader_Code currentData);


        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnStrategyEnd(sender, argument);
            StrategyEnd_();
        }

        public abstract void StrategyEnd_();

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            if (ImportStrategies != null)
                for (int i = 0; i < ImportStrategies.Count; i++)
                    ImportStrategies[i].OnStrategyStart(sender, argument);
            StrategyStart_();
        }

        public abstract void StrategyStart_();
    }
}