using com.wer.sc.data.forward.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class HistoryDataForwardFactory
    {
        public static IHistoryDataForward CreateHistoryDataForward(IDataReader dataReader, IList<string> codes, HistoryDataForwardArguments args)
        {
            return new HistoryDataForward(dataReader, codes, args);
        }

        public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataReader dataReader, string code, HistoryDataForwardArguments args)
        {
            return new HistoryDataForward_Code(dataReader, code, args);
        }
    }
}