using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 创建图形显示组件
    /// </summary>
    public interface IChartComponentFactory
    {
        /// <summary>
        /// 创建组件，只传入组件要用到的数据中心
        /// </summary>
        /// <param name="dataCenter"></param>
        IChartComponent Create(IDataCenter dataCenter);

        /// <summary>
        /// 创建组件，传入数据中心，并且显示指定的品种K线图
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="code"></param>
        /// <param name="time"></param>
        IChartComponent Create(IDataCenter dataCenter, string code, double time);

        /// <summary>
        /// 初始化组件，传入数据中心和用来显示的数据包，并且显示指定的品种K线图
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        IChartComponent Create(IDataCenter dataCenter, IDataPackage_Code dataPackage, double time);
    }
}