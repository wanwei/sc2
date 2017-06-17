using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata.utils
{
    /// <summary>
    /// 该类表示等待更新的数据
    /// </summary>
    public class InstrumentDatesInfo
    {
        public String instrument;

        public List<int> dates;

        public override string ToString()
        {
            string str = instrument + "," + dates.Count;
            if (dates.Count > 0)
                return str + "," + dates[0] + "," + dates[dates.Count - 1];
            return str;
        }
    }
}