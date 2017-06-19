using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.update;

namespace com.wer.sc.data.store.file
{
    public class UpdateInfoStore_File : IUpdateInfoStore
    {
        private String path;

        public UpdateInfoStore_File(String path)
        {
            this.path = path;
        }

        public UpdatedDataInfo Load()
        {
            return new UpdatedDataInfo(path);
        }

        public void Save(UpdatedDataInfo updatedDataInfo)
        {
            updatedDataInfo.Save();
        }
    }
}