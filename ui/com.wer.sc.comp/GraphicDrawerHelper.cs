using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp
{
    public interface GraphicDrawerHelper
    {
        /// <summary>
        /// 
        /// </summary>
        IGraphicDataProvider_Candle DataProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="period"></param>
        void Show(String code, int start, int end, KLinePeriod period);

        void Show(String code, int start, int end, KLinePeriod period, float time);

        /// <summary>
        /// 切换code
        /// </summary>
        /// <param name="code"></param>
        void ChangeCode(String code);

        /// <summary>
        /// 切换时间
        /// </summary>
        /// <param name="time"></param>
        void ChangeTime(double time);

        /// <summary>
        /// 切换索引号
        /// </summary>
        /// <param name="index"></param>
        void ChangeIndex(int index);

        /// <summary>
        /// 切换周期
        /// </summary>
        /// <param name="period"></param>
        void ChangePeriod(KLinePeriod period);

        /// <summary>
        /// 切换到选定的block
        /// </summary>
        void ChangeToSelectBlock();

        /// <summary>
        /// 运行模型
        /// </summary>
        /// <param name="model"></param>
        void Run(IPlugin_KLineModel model);

        void Run(IKLineData data, IPlugin_KLineModel model);

        void Run(String code, int start, int end, KLinePeriod period, IPlugin_KLineModel model);
    }

    /// <summary>
    /// 画图控件
    /// </summary>
    public class DrawControlHelperImpl
    {
        public DrawControlHelperImpl()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        IGraphicDataProvider_Candle DataProvider { get { return null; } }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="code"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="period"></param>
        public void Show(String code, int start, int end, KLinePeriod period)
        {

        }

        public void Show(String code, int start, int end, KLinePeriod period, float time)
        {

        }

        /// <summary>
        /// 切换code
        /// </summary>
        /// <param name="code"></param>
        public void ChangeCode(String code)
        {

        }

        /// <summary>
        /// 切换时间
        /// </summary>
        /// <param name="time"></param>
        public void ChangeTime(double time)
        {

        }

        /// <summary>
        /// 切换索引号
        /// </summary>
        /// <param name="index"></param>
        public void ChangeIndex(int index)
        {

        }

        /// <summary>
        /// 切换周期
        /// </summary>
        /// <param name="period"></param>
        public void ChangePeriod(KLinePeriod period)
        {

        }

        /// <summary>
        /// 切换到选定的block
        /// </summary>
        public void ChangeToSelectBlock()
        {

        }

        public void Run(IPlugin_KLineModel model)
        {

        }

        public void Run(IKLineData data, IPlugin_KLineModel model)
        {

        }

        public void Run(String code, int start, int end, KLinePeriod period, IPlugin_KLineModel model)
        {

        }
    }
}