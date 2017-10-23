using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    public class TimeLineDataExtend_RealTime : TimeLineData_RealTime, ITimeLineData_RealTime
    {
        private ITimeLineData_Extend timeLineData;

        public TimeLineDataExtend_RealTime(ITimeLineData_Extend timeLineData) : base(timeLineData)
        {
            this.timeLineData = timeLineData;
        }

        public IList<int> TradingTimeEndBarPoses
        {
            get
            {
                return this.timeLineData.TradingTimeEndBarPoses;
            }
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            return this.timeLineData.IsTradingTimeEnd(barPos);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            return this.timeLineData.IsTradingTimeStart(barPos);
        }     
    }
}
