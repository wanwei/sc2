using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.info
{
    /// <summary>
    /// 当前数据操作
    /// </summary>
    public interface IGraphicOperator_CurrentInfo
    {
        void Change(String code, double time);
    }
}
