using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class DataReaderFactory
    {
        public static IDataReader CreateDataReader(DataCenter dataCenter)
        {
            string dataCenterUri = dataCenter.DataCenterInfo.Uri;
            if (!DataStoreFactory.CheckDataStore(dataCenterUri))
                return null;
            return new DataReader(dataCenterUri);
        }


        /// <summary>
        /// 创建一个数据读取器
        /// </summary>
        /// <param name="dataCenterUri"></param>
        /// <returns></returns>
        public static IDataReader CreateDataReader(string dataCenterUri)
        {
            if (!DataStoreFactory.CheckDataStore(dataCenterUri))
                return null;
            return new DataReader(dataCenterUri);
        }

        public static IDataReader CreateDataReader_ShortCode(string dataCenterUri)
        {
            return new DataReader(dataCenterUri);
        }
    }
}
