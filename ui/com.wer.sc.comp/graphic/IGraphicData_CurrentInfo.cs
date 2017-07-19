using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public interface IGraphicData_CurrentInfo : IGraphicData
    {
        DetailInfo GetCurrentInfo();

        ITickData GetCurrentTickData();

        int CurrentTickIndex { get; }

        void ChangeData(DetailInfo currentInfo);
    }
}