using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 数据模拟前进时应用到的数据类型
    /// </summary>
    public class ForwardReferedPeriods
    {
        private bool useTickData = false;

        private bool useTimeLineData = false;

        private List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();

        public ForwardReferedPeriods()
        {
        }

        public ForwardReferedPeriods(IList<KLinePeriod> usedKLinePeriods, bool useTick, bool useTimeLine)
        {
            this.UsedKLinePeriods.AddRange(usedKLinePeriods);
            this.useTickData = useTick;
            this.useTimeLineData = useTimeLine;
        }

        public bool UseTickData
        {
            get
            {
                return useTickData;
            }

            set
            {
                useTickData = value;
            }
        }

        public bool UseTimeLineData
        {
            get
            {
                return useTimeLineData;
            }

            set
            {
                useTimeLineData = value;
            }
        }

        public List<KLinePeriod> UsedKLinePeriods
        {
            get
            {
                return usedKLinePeriods;
            }

            set
            {
                usedKLinePeriods = value;
            }
        }

        public KLinePeriod GetMinPeriod()
        {
            if (UsedKLinePeriods == null)
                return null;
            return UsedKLinePeriods.Min();
        }
    }
}
