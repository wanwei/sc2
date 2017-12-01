using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy
{
    public class StrategyOnTickInfo : ForwardTickInfo, IStrategyOnTickInfo
    {
        public StrategyOnTickInfo(ITickData_Extend tickData, int index) : base(tickData, index)
        {
        }        
    }
}
