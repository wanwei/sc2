using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class RealTimeData_Code_Null : IRealTimeData_Code
    {
        public string Code
        {
            get
            {
                return null;
            }
        }

        public float Price
        {
            get
            {
                return 0;
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return null;
        }

        public ITickData GetTickData()
        {
            return null;
        }

        public ITimeLineData GetTimeLineData()
        {
            return null;
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        public static IRealTimeData_Code Instance = new RealTimeData_Code_Null();
    }
}
