using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyData : IStrategyData
    {
        public IStrategy Strategy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IStrategyInfo StrategyInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
