using com.wer.sc.data.store.file;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class DataStoreFactory
    {
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
