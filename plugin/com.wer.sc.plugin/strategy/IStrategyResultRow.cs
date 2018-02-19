using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyResultRow
    {
        string Code { get; }

        int Date { get; }

        IList<object> DataRow { get; }
    } 
}
