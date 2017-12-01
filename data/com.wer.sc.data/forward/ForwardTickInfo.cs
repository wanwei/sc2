using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class ForwardTickInfo : IForwardTickInfo
    {
        private int index;

        private ITickData_Extend tickData;

        private ITickBar tickBar;

        public ForwardTickInfo(ITickData_Extend tickData, int index)
        {
            this.index = index;
            this.tickData = tickData;
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public ITickBar TickBar
        {
            get
            {
                if (tickBar == null)
                    tickBar = tickData.GetBar(index);
                return tickBar;
            }
        }

        public ITickData_Extend TickData
        {
            get
            {
                return tickData;
            }
        }
    }
}