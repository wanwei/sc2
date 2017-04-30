using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua.adjust
{

    public class TickInfo_Period
    {
        public int PeriodIndex;

        public double StartTime;

        public double EndTime;

        //该阶段在ticklist里的起始index
        public int StartIndex = -1;

        //该阶段在ticklist里的结束index
        public int EndIndex = -1;

        public TickPeriodAdjustInfo adjustInfo = new TickPeriodAdjustInfo();

        override
        public String ToString()
        {
            return PeriodIndex + "," + StartIndex + "," + EndIndex + "," + adjustInfo.ToString();
        }
    }
}
