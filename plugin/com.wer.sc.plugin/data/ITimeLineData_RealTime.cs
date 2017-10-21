using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线
    /// </summary>
    public interface ITimeLineData_RealTime : ITimeLineData
    {
        /// <summary>
        /// 清空当前时间数据bar的临时数据
        /// </summary>
        void ResetCurrentBar();

        /// <summary>
        /// 修改当前分时线的BarPos，并修改对应数据
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="barPos"></param>
        void ChangeCurrentBar(ITimeLineBar chart, int barPos);

        /// <summary>
        /// 修改当前数据
        /// </summary>
        /// <param name="chart"></param>
        void ChangeCurrentBar(ITimeLineBar chart);

        /// <summary>
        /// 得到当前原始的数据
        /// </summary>
        /// <returns></returns>
        //ITimeLineBar GetCurrentBar_Original();
    }
}
