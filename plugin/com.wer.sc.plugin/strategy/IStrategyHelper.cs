using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyHelper
    {
        /// <summary>
        /// 得到
        /// </summary>
        IDrawHelper DrawHelper { get; }
    }
}