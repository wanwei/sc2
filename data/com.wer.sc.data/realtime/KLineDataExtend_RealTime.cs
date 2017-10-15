using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    public class KLineDataExtend_RealTime : KLineData_RealTime, IKLineData_RealTime
    {
        private IKLineData_Extend klineData_Extend;
        public KLineDataExtend_RealTime(IKLineData_Extend klineData) : base(klineData)
        {
            this.klineData_Extend = klineData;
        }

        public double GetEndTime(int barPos)
        {
            return klineData_Extend.GetEndTime(barPos);
        }

        public IList<int> GetTradingDayEndBarPoses()
        {
            return klineData_Extend.GetTradingDayEndBarPoses();
        }

        public IList<int> GetTradingTimeEndBarPoses()
        {
            return klineData_Extend.GetTradingTimeEndBarPoses();
        }

        public bool IsDayEnd(int barPos)
        {
            return klineData_Extend.IsDayEnd(barPos);
        }

        public bool IsDayStart(int barPos)
        {
            return klineData_Extend.IsDayStart(barPos);
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            return klineData_Extend.IsTradingTimeEnd(barPos);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            return klineData_Extend.IsTradingTimeStart(barPos);
        }
    }
}
