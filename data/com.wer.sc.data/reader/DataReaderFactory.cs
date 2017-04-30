using com.wer.sc.data.reader.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class DataReaderFactory
    {
        public static IDataReader CreateDataReader(string dataCenterUri)
        {
            return new DataReader(dataCenterUri);
        }
    }
}
