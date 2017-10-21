using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy
{
    public class StrategyOnTickArgument : RealTimeDataReader_CodeWrapper
    {
        public StrategyOnTickArgument(IRealTimeDataReader_Code realTimeDataReader_Code) : base(realTimeDataReader_Code)
        {
        }
    }
}
