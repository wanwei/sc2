using com.wer.sc.data.store.file;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class DataStoreFactory
    {
        public static bool CheckDataStore(string dataCenterUri)
        {
            Uri uri = new Uri(dataCenterUri);
            if (uri.IsFile)
            {
                string path = uri.LocalPath;
                DataPathUtils dataPathUtils = new DataPathUtils(path);
                string codePath = dataPathUtils.GetInstrumentPath();
                //如果连股票代码文件都不存在，可以认为该数据中心没建立   
                if (!File.Exists(codePath))
                    return false;
                return true;
            }
            return true;
        }

        public static IDataStore CreateDataStore(string dataCenterUri)
        {
            Uri uri = new Uri(dataCenterUri);
            if (uri.IsFile)
            {
                return new DataStore_File(uri.LocalPath);
            }
            return null;
        }
    }
}
