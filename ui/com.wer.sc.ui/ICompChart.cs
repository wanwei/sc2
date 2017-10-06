using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui
{
    public interface ICompChart
    {
        void Change(String code);

        void Change(double time);

        void Change(string code, double time);

        //KLinePeriod klinePeriod { }
    }
}
