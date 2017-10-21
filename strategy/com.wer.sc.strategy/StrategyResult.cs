using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyResult : IStrategyQueryResult
    {
        private IList<IStrategyQueryResult_Single> list = new List<IStrategyQueryResult_Single>();

        public IList<IStrategyQueryResult_Single> StrategyResults
        {
            get
            {
                return list;
            }
        }
    }
}
