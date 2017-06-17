using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider
{

    public class CodeInfoComparer : IComparer<CodeInfo>
    {
        public int Compare(CodeInfo x, CodeInfo y)
        {
            return x.Code.CompareTo(y.Code);
        }
    }
}
