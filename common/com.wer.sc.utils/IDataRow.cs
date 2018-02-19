using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public interface IDataRow
    {
        string Code { get; }

        int Date { get; }

        IList<object> DataRow { get; }
    }
}
