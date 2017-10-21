using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    public class StrategyGetter
    {
        public static IStrategy GetStrategy(Type type)
        {
            IStrategy strategy = (IStrategy)Activator.CreateInstance(type);
            return strategy;
        }
    }
}