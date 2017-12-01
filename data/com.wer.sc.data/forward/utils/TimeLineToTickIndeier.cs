using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    public class TimeLineToTickIndeier
    {
        private ITickData tickData;

        private ITimeLineData timeLineData;

        public TimeLineToTickIndeier(ITickData tickData, ITimeLineData timeLineData)
        {

        }

        public void ChangeTradingDay(ITickData tickData)
        {

        }

        public void ChangeTradingDay(ITickData tickData, IKLineData klineData)
        {

        }

        public int GetTimeLineBarPosIfFinished(int tickBarPos, out int lastTimeLineBarPos)
        {
            lastTimeLineBarPos = -1;
            return -1;
        }
    }
}
