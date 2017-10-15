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
        public bool UseTickData = false;

        public bool isReferTimeLineData = false;

        public List<KLinePeriod> UsedKLinePeriods = new List<KLinePeriod>();

        public KLinePeriod GetMinPeriod()
        {
            if (UsedKLinePeriods == null)
                return null;
            return UsedKLinePeriods.Min();
        }
    }
}
