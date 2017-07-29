using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyRunnerFactory
    {
        public static IStrategyRunner CreateStrategyRunner(IDataReader dataReader, StrategyRunnerArguments args)
        {
            return new StrategyRunner_History(dataReader, args);
        }

        public static IStrategyRunner CreateStrategyRunner(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            return new StrategyRunner_History(dataPackage, referedPeriods, forwardPeriod);
        }
    }
}
