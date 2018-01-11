using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common
{
    public interface IDataLooper
    {
        void Loop(int barPos);
    }
}
