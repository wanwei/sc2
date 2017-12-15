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
    /// 组件，可显示K线、分时线、tick线
    /// 功能：
    /// 1.切换显示的code
    /// 2.切换显示的time
    /// 3.
    /// 
    /// 显示K线时可以切换正显示的K线
    /// </summary>
    public interface IChartComponent
    {
    }

    /// <summary>
    /// chart改变事件的参数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arguments"></param>
    public delegate void DelegateOnChartComponentChanged(object sender, CompChartChangeArguments arguments);

    /// <summary>
    /// 组件改变
    /// </summary>
    public class ChartComponentChangeArguments
    {
        private IChartComponentData prevCompChartInfo;

        private IChartComponentData currentCompChartInfo;

        public IChartComponentData PrevCompChartInfo
        {
            get
            {
                return prevCompChartInfo;
            }
        }

        public IChartComponentData CurrentCompChartInfo
        {
            get
            {
                return currentCompChartInfo;
            }
        }

        public ChartComponentChangeArguments(IChartComponentData prevCompChartInfo, IChartComponentData currentCompChartInfo)
        {
            this.prevCompChartInfo = prevCompChartInfo;
            this.currentCompChartInfo = currentCompChartInfo;
        }
    }
}
