using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.strategy;
using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// Chart组件接口
    /// </summary>
    public interface ICompChart : ICompChartInfo
    {
        /// <summary>
        /// 得到当前显示图形用到的数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }       

        #region init

        /// <summary>
        /// 初始化组件，传入组件要用到的数据中心
        /// </summary>
        /// <param name="dataCenter"></param>
        void Init(DataCenter dataCenter);

        /// <summary>
        /// 初始化组件，传入数据中心，并且显示指定的品种K线图
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="code"></param>
        /// <param name="time"></param>
        void Init(DataCenter dataCenter, string code, double time);

        /// <summary>
        /// 初始化组件，传入数据中心，并且显示指定的品种K线图
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        void Init(DataCenter dataCenter, IDataPackage_Code dataPackage, double time);

        #endregion

        #region event
                    
        /// <summary>
        /// 时间修改事件
        /// </summary>
        event DelegateOnCompChartChanged OnCompChartTimeChanged;

        #endregion

        #region controller

        /// <summary>
        /// 
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
        /// 切换图中显示的图形，K线、分时线或tick线
        /// </summary>
        /// <param name="chartType"></param>
        void ChangeChartType(ChartType chartType);

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间
        /// </summary>
        /// <param name="cnt"></param>
        void ForwardView(int cnt);

        /// <summary>
        /// 视图向前进或后退，
        /// </summary>
        /// <param name="forwardPeriod"></param>
        void ForwardTime(ForwardPeriod forwardPeriod);

        void Refresh();

        void Play();

        void Pause();

        #endregion
    }

    public interface ICompChartInfo
    {
        /// <summary>
        /// 当前显示的Code
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到当前显示的时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到当前显示的图形
        /// </summary>
        ChartType ChartType { get; }

        /// <summary>
        /// 当前显示的K线周期
        /// </summary>
        KLinePeriod KLinePeriod { get; }

        /// <summary>
        /// 当前十字星选中的时间
        /// </summary>
        double CrossSelectedTime { get; }
    }

    /// <summary>
    /// chart改变事件的参数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arguments"></param>
    public delegate void DelegateOnCompChartChanged(object sender, CompChartChangeArguments arguments);

    /// <summary>
    /// 组件改变
    /// </summary>
    public class CompChartChangeArguments
    {
        private ICompChartInfo prevCompChartInfo;

        private ICompChartInfo currentCompChartInfo;

        public ICompChartInfo PrevCompChartInfo
        {
            get
            {
                return prevCompChartInfo;
            }
        }

        public ICompChartInfo CurrentCompChartInfo
        {
            get
            {
                return currentCompChartInfo;
            }
        }

        public CompChartChangeArguments(ICompChartInfo prevCompChartInfo, ICompChartInfo currentCompChartInfo)
        {
            this.prevCompChartInfo = prevCompChartInfo;
            this.currentCompChartInfo = currentCompChartInfo;
        }
    }
}