using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public class StrategyOnTickArgument : ForwardOnTickArgument, IStrategyOnTickArgument
    {
        private IStrategyOnTickInfo onTickInfo;

        public StrategyOnTickArgument(ForwardOnTickArgument argument) : this(argument.TickInfo.TickData, argument.TickInfo.Index, argument.DataForward_Code)
        {

        }

        public StrategyOnTickArgument(ITickData_Extend tickData, int index, IDataForward_Code dataForward_Code) : base(tickData, index, dataForward_Code)
        {
            this.onTickInfo = new StrategyOnTickInfo(tickData, index);
        }

        public IStrategyOnTickInfo Tick
        {
            get
            {
                return this.onTickInfo;
            }
        }
    }
}
