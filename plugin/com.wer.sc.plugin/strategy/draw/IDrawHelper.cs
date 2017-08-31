using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.draw
{
    public interface IDrawHelper
    {
        /// <summary>
        /// 得到K线的画图帮助类
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IDrawer GetDrawer_KLine(KLinePeriod klinePeriod);

        /// <summary>
        /// 得到分时线画图帮助类
        /// </summary>
        /// <returns></returns>
        IDrawer GetDrawer_TimeLine();

        /// <summary>
        /// 得到tick线画图帮助类
        /// </summary>
        /// <returns></returns>
        IDrawer GetDrawer_Tick();
    }
}
