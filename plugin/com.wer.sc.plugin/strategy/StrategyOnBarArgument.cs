using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy
{
    public class StrategyOnBarArgument : RealTimeDataReader_CodeWrapper
    {
        private List<ForwardOnbar_Info> onBarInfos;

        public StrategyOnBarArgument(IRealTimeDataReader_Code realTimeDataReader_Code, List<ForwardOnbar_Info> onBarInfos) : base(realTimeDataReader_Code)
        {
            this.onBarInfos = onBarInfos;
        }

        public List<ForwardOnbar_Info> StrategyOnBarInfos
        {
            get { return onBarInfos; }
        }
    }
}