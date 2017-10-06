using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public interface IHistoryDataForwardFactory
    {
        IHistoryDataForward_Code CreateHistoryDataForward_Code(string code, HistoryDataForwardArguments args);

        IHistoryDataForward_Code CreateHistoryDataForward_Code(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod);
    }
}
