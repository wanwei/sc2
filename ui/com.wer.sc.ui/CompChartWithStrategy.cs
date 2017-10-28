using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui
{
    public class CompChartWithStrategy
    {
        private CompChart compChart1;

        public CompChartWithStrategy(CompChart compChart1)
        {
            this.compChart1 = compChart1;
            
            //IDataPackage dataPackage = compChart1.CompChartData.DataPackage;
            //KLinePeriod period = compChart1.GetKLinePeriod();
            //StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            //referedPeriods.UsedKLinePeriods.Add(period);
            //ForwardPeriod forwardPeriod = new ForwardPeriod(false, period);
            //IStrategyExecutor strategyRunner = StrategyExecutorFactory.CreateHistoryExecutor(dataPackage, referedPeriods, forwardPeriod, compChart1.StrategyHelper);
        }

        public IStrategyExecutor CreateExecutor(IDataPackage_Code dataPackage, IStrategy strategy, KLinePeriod period)
        {
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(period);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, period);
            return StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutorByDataPackage(dataPackage, referedPeriods, forwardPeriod, compChart1.StrategyHelper);
        }

        public void Run()
        {

        }
    }
}