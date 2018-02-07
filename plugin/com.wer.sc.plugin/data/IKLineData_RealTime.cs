using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 实时K线数据接口
    /// 该接口用于显示实时的K线数据，如要看20170802.095121的1分钟数据
    /// 原始的1分钟数据是无法表示的，需要通过该接口完成
    /// </summary>
    public interface IKLineData_RealTime : IKLineData_Extend
    {
        /// <summary>
        /// 清空当前时间数据bar的临时数据
        /// </summary>
        void ResetCurrentBar();

        /// <summary>
        /// 修改指定的barpos数据，并且跳转到指定的barpos
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="barPos"></param>
        void ChangeCurrentBar(IKLineBar chart, int barPos);

        /// <summary>
        /// 修改当前数据
        /// </summary>
        /// <param name="chart"></param>
        void ChangeCurrentBar(IKLineBar chart);

        /// <summary>
        /// 得到当前原始的数据
        /// </summary>
        /// <returns></returns>
        IKLineBar GetCurrentBar_Original();

        /// <summary>
        /// 得到原始
        /// </summary>
        /// <returns></returns>
        IKLineData_Extend GetKLineData_Original();
    }
}