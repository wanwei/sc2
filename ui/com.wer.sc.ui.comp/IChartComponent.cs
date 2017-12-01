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
    /// 组件
    /// </summary>
    public interface IChartComponent
    {
        IChartComponentView View { get; }

        IChartComponentController Controller { get; }

        IChartComponentInfo Data { get; }
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
        private IChartComponentInfo prevCompChartInfo;

        private IChartComponentInfo currentCompChartInfo;

        public IChartComponentInfo PrevCompChartInfo
        {
            get
            {
                return prevCompChartInfo;
            }
        }

        public IChartComponentInfo CurrentCompChartInfo
        {
            get
            {
                return currentCompChartInfo;
            }
        }

        public ChartComponentChangeArguments(IChartComponentInfo prevCompChartInfo, IChartComponentInfo currentCompChartInfo)
        {
            this.prevCompChartInfo = prevCompChartInfo;
            this.currentCompChartInfo = currentCompChartInfo;
        }
    }
}
