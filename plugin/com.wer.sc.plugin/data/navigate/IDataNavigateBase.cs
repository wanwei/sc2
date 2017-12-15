using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public interface IDataNavigateBase
    {
        /// <summary>
        /// 跳转到指定时间
        /// 执行该操作后，GetKLineData等获取数据的操作都会返回该时间上的数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns>如果不能够导航到该时间，则返回false</returns>
        bool NavigateTo(double time);

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        bool Backward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 得到它所属的数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }

    }
}
