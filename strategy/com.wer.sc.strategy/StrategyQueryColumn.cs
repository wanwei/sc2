using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils;

namespace com.wer.sc.strategy
{
    public class StrategyQueryColumn : IStrategyQueryColumn
    {
        public ObjectType DataType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
