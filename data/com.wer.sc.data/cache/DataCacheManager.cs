using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache
{
    public class DataCacheManager
    {
        public DataCacheManager()
        {
        }

        /// <summary>
        /// 创建一个数据读取器
        /// </summary>
        /// <param name="dataCenterUri"></param>
        /// <returns></returns>
        public IDataReader GetDataReader(string dataCenterUri)
        {
            if (!DataStoreFactory.CheckDataStore(dataCenterUri))
                return null;
            return new DataReader(dataCenterUri);
        }

        public static DataCacheManager Instance
        {
            get { return null; }
        }
    }
}
