using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineTimeListGetter
    {
        private ITradingDayReader openDateReader;
        private ITradingTimeReader openTimeReader;

        public KLineTimeListGetter(ITradingDayReader openDateReader, ITradingTimeReader openTimeReader)
        {
            this.openDateReader = openDateReader;
            this.openTimeReader = openTimeReader;
        }

        public List<double> GetKLineTimeList(string code, int date, KLinePeriod period)
        {
            List<double[]> openTimes = openTimeReader.GetTradingTime(code, date);
            return KLineTimeListUtils.GetKLineTimeList(date, openDateReader, openTimes, period);
        }
    }
}
