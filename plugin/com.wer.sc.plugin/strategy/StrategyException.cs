using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyException : ApplicationException
    {
        public StrategyException() : base()
        {

        }

        public StrategyException(string message) : base(message)
        {

        }

        public StrategyException(string message, Exception innerException)
        {

        }
    }
}
