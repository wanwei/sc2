using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    public interface ITradingTimeReader
    {
        /// <summary>
        /// 得到指定开盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<double[]> GetTradingTime(String code, int date);
    }
}
