using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public interface ITimeLineBar
    {
        string Code { get; }

        double Time { get; }

        float Price { get; }

        int Mount { get; }

        int Hold { get; }

        float UpPercent { get; }

        float UpRange { get; }
    }
}
