using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.info
{
    public class GraphicOperator_CurrentInfo_Nav : IGraphicOperator_CurrentInfo
    {
        private IDataNavigate dataNavigate;

        public GraphicOperator_CurrentInfo_Nav(IDataNavigate dataNavigate)
        {
            this.dataNavigate = dataNavigate;
        }

        public void Change(string code, double time)
        {
            this.dataNavigate.Change(code, time, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
        }
    }
}
