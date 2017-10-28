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
        private Dictionary<KLinePeriod, ForwardOnbar_Info> dic_Period_Bar = new Dictionary<KLinePeriod, ForwardOnbar_Info>();

        private List<ForwardOnbar_Info> onBarInfos;

        public StrategyOnBarArgument(IRealTimeDataReader_Code realTimeDataReader_Code, List<ForwardOnbar_Info> onBarInfos) : base(realTimeDataReader_Code)
        {
            this.onBarInfos = onBarInfos;
            for (int i = 0; i < onBarInfos.Count; i++)
            {
                ForwardOnbar_Info bar = onBarInfos[i];
                this.dic_Period_Bar.Add(bar.KLinePeriod, bar);
            }
        }

        public List<ForwardOnbar_Info> StrategyOnBarInfos
        {
            get { return onBarInfos; }
        }

        public ForwardOnbar_Info GetBar(KLinePeriod period)
        {
            if (dic_Period_Bar.ContainsKey(period))
                return dic_Period_Bar[period];
            return null;
        }

        public ForwardOnbar_Info MainBarInfo
        {
            get { return onBarInfos[0]; }
        }
    }
}