using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public interface IChartComponentController
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
        /// 视图向前进或后退，
        /// </summary>
        /// <param name="forwardPeriod"></param>
        void ForwardTime(ForwardPeriod forwardPeriod);

        /// <summary>
        /// 切换图中显示的图形，K线、分时线或tick线
        /// </summary>
        /// <param name="chartType"></param>
        void ChangeChartType(ChartType chartType);

        /// <summary>
        /// 修改K线每一个bar显示的宽度
        /// </summary>
        /// <param name="width"></param>
        void ChangeBarWidth(double width);

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间，只会改变当前显示的K线
        /// </summary>
        /// <param name="cnt"></param>
        void ForwardView(int cnt);


        /// <summary>
        /// 刷新图形，如果是K线，则返回当前时间显示的K线
        /// </summary>
        void Refresh();

        /// <summary>
        /// 自动前进，会自动改变当前时间，模拟真实交易场景
        /// </summary>
        void Play();

        /// <summary>
        /// 停止自动前进
        /// </summary>
        void Pause();
    }
}