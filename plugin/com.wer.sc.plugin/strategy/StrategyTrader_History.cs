using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 基于历史的交易器
    /// </summary>
    public class StrategyTrader_History : IStrategyTrader
    {
        public IList<IStrategyTrader_Code> StrategyTraders
        {
            get
            {
                return null;
            }
        }

        public IList<string> GetAllCodes()
        {
            return null;
        }

        public IStrategyTrader_Code GetStrategyTrader(string code)
        {
            return null;
        }
    }
}
