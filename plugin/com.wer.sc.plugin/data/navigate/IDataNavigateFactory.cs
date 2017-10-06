using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public interface IDataNavigateFactory
    {
        IDataNavigate_Code CreateDataNavigate(string code, double time, int beforeDays, int afterDays);

        IDataNavigate_Code CreateDataNavigate(string code, double time);
    }
}
