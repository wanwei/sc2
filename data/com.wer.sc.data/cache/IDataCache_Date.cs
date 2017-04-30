using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache
{
    public interface IDataCache_Date
    {
        String Date { get; }

        List<String> GetAllCodes();

        IDataCache_CodeDate GetCache_CodeDate(int code);
    }
}
