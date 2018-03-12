using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTestUtils
    {
        public static IStrategyExecutor_Single CreateExecutor_CodePeriod(string code, int start, int end)
        {
            IStrategyExecutorFactory executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory();
            StrategyReferedPeriods referedPeriods = GetReferedPeriods();
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

            StrategyArguments_CodePeriod strategyCodePeriod = new StrategyArguments_CodePeriod(code, start, end, referedPeriods, forwardPeriod);
            IStrategyExecutor_Single executor = executorFactory.CreateExecutor_History(strategyCodePeriod);
            return executor;
        }

        public static IStrategyExecutor_Single CreateExecutor_DataPackage(string code, int start, int end)
        {
            IStrategyExecutorFactory executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory();
            StrategyReferedPeriods referedPeriods = GetReferedPeriods();
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end);
            StrategyArguments_DataPackage strategyCodePeriod = new StrategyArguments_DataPackage(dataPackage, referedPeriods, forwardPeriod);
            IStrategyExecutor_Single executor = executorFactory.CreateExecutor_History(strategyCodePeriod);
            return executor;
        }

        private static StrategyReferedPeriods GetReferedPeriods()
        {
            List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods(usedKLinePeriods, true, true);
            return referedPeriods;
        }
    }
}
