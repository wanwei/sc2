using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public interface IGraphicData
    {
        event DelegateOnGraphicDataChange OnGraphicDataChange;
    }

    public delegate void DelegateOnGraphicDataChange(object sender, GraphicDataChangeArgument arg);

    public class GraphicDataChangeArgument
    {

    }
}
