using com.wer.sc.data.navigate.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigateFactory
    {
        public static IDataNavigate_Code CreateDataNavigate(IDataReader dataReader, string code, double time)
        {
            return new DataNavigate_Code(dataReader, code, time);
        }

        public static IDataNavigate_Code CreateDataNavigate(String dataCenterUri, string code, double time)
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
            return new DataNavigate_Code(dataReader, code, time);
        }
    }
}
