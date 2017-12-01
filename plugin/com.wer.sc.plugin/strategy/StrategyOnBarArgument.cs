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
        private Dictionary<KLinePeriod, IForwardKLineBarInfo> dic_Period_Bar = new Dictionary<KLinePeriod, IForwardKLineBarInfo>();

        private List<IForwardKLineBarInfo> onBarInfos;

        public StrategyOnBarArgument(IRealTimeDataReader_Code realTimeDataReader_Code, List<IForwardKLineBarInfo> onBarInfos) : base(realTimeDataReader_Code)
        {
            this.onBarInfos = onBarInfos;
            for (int i = 0; i < onBarInfos.Count; i++)
            {
                IForwardKLineBarInfo bar = onBarInfos[i];
                this.dic_Period_Bar.Add(bar.KLinePeriod, bar);
            }
        }

        public List<IForwardKLineBarInfo> StrategyOnBarInfos
        {
            get { return onBarInfos; }
        }

        public IForwardKLineBarInfo GetBar(KLinePeriod period)
        {
            if (dic_Period_Bar.ContainsKey(period))
                return dic_Period_Bar[period];
            return null;
        }

        public IForwardKLineBarInfo MainBarInfo
        {
            get { return onBarInfos[0]; }
        }
    }
}