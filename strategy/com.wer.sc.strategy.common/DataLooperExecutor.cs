using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common
{
    public class DataLooperExecutor
    {
        public static void Execute(IDataLooper looper, int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                looper.Loop(i);
            }
        }
    }
}
