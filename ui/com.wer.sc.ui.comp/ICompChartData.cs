using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public interface ICompChartData
    {
        /// <summary>
        /// 得到组件显示的Code
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到组件显示图形当前的时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到周期
        /// </summary>
        KLinePeriod KLinePeriod { get; }


    }
}
