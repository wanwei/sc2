using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略启动参数
    /// </summary>
    public interface IStrategyOnStartArgument
    {
        /// <summary>
        /// 得到数据长度
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        int GetKLineLength(KLinePeriod klinePeriod);

        int GetTimeLineLength();
    }
}