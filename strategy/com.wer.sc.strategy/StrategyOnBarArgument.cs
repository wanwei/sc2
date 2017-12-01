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
    public class StrategyOnBarArgument : ForwardOnBarArgument, IStrategyOnBarArgument
    {
        private IStrategyOnBarInfo mainBar;

        private List<IStrategyOnBarInfo> finishedBars = new List<IStrategyOnBarInfo>();

        private List<IStrategyOnBarInfo> bars = new List<IStrategyOnBarInfo>();

        public StrategyOnBarArgument(ForwardOnBarArgument forwardOnBarArgument) : base(forwardOnBarArgument.AllFinishedBars, forwardOnBarArgument.DataForward_Code)
        {
            this.mainBar = new StrategyOnBarInfo((ForwardOnbar_Info)forwardOnBarArgument.MainBar);
            for (int i = 0; i < forwardOnBarArgument.AllFinishedBars.Count; i++)
            {
                this.finishedBars.Add(new StrategyOnBarInfo((ForwardOnbar_Info)forwardOnBarArgument.AllFinishedBars[i]));
            }
            //for (int i = 0; i < forwardOnBarArgument..Count; i++)
            //{
            //    this.finishedBars.Add(new StrategyOnBarInfo((ForwardOnbar_Info)forwardOnBarArgument.AllFinishedBars[i]));
            //}
        }

        public IStrategyOnBarInfo MainBar
        {
            get
            {
                return mainBar;
            }
        }

        public IList<IStrategyOnBarInfo> FinishedBars
        {
            get
            {
                return finishedBars;
            }
        }

        public IList<IStrategyOnBarInfo> Bars
        {
            get
            {
                return bars;
            }
        }
    }
}