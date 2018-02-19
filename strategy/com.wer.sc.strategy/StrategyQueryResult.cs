using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyQueryResult : IStrategyQueryResult
    {
        private IList<IStrategyQueryResultRow> list = new List<IStrategyQueryResultRow>();

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IList<IStrategyQueryResultRow> StrategyResults
        {
            get
            {
                return list;
            }
        }

        public string[] Title
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
