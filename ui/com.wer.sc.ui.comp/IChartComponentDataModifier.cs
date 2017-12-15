using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件的数据读取器
    /// </summary>
    public interface IChartComponentDataModifier
    {
        /// <summary>
        /// 切换数据包，并切换时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        void Change(IDataPackage_Code dataPackage, double time);

        /// <summary>
        /// 修改图中显示的品种
        /// </summary>
        /// <param name="code"></param>
        void Change(string code);

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        void Change(double time);

        /// <summary>
        /// 修改图中显示的品种和当前时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        void Change(String code, double time);

        /// <summary>
        /// 视图前进或后退一个周期
        /// </summary>
        /// <param name="forwardPeriod"></param>
        void ForwardTime(ForwardPeriod forwardPeriod);
    }
}