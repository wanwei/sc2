using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public interface IRealTimeDataNavigaterFactory
    {
        IRealTimeDataNavigater CreateNavigater(string code, double time, int startDate, int endDate);

        IRealTimeDataNavigater CreateNavigater(string code, double time);
    }
}
