using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class MainContractStore_File : IMainContractStore
    {
        private String path;

        public MainContractStore_File(DataPathUtils dataPathUtils)
        {
            this.path = dataPathUtils.GetMainContractsPath();
        }

        public void Save(IList<MainContractInfo> codes)
        {
            TextExchangeUtils.Write<MainContractInfo>(path, codes);
        }

        public IList<MainContractInfo> Load()
        {
            return TextExchangeUtils.Load<MainContractInfo>(path, typeof(MainContractInfo));
        }

        public void Delete()
        {
            File.Delete(path);
        }
    }
}
