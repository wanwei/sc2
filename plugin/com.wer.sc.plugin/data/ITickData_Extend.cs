using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// tick数据扩展包
    /// </summary>
    public interface ITickData_Extend : ITickData
    {


        /// <summary>
        /// 得到所有交易时间的结束barpos
        /// </summary>
        IList<int> TradingTimeEndBarPoses { get; }

        /// <summary>
        /// 是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingTimeStart(int barPos);

        /// <summary>
        /// 是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingTimeEnd(int barPos);
    }
}
