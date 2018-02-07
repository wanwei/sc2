using com.wer.sc.data;
using com.wer.sc.graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略绘图器
    /// </summary>
    public interface IStrategyDrawer
    {
        /// <summary>
        /// 得到K线的画图帮助接口
        /// 每个K线周期都有各自的画图接口
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IShapeDrawer_PriceRect GetDrawer_KLine(KLinePeriod klinePeriod);

        /// <summary>
        /// 得到分时线画图帮助类
        /// </summary>
        /// <returns></returns>
        IShapeDrawer_PriceRect GetDrawer_TimeLine();

        /// <summary>
        /// 得到tick线画图帮助类
        /// </summary>
        /// <returns></returns>
        IShapeDrawer_PriceRect GetDrawer_Tick();
    }
}
