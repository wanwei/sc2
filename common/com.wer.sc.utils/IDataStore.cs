using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    /// <summary>
    /// 数据保存接口
    /// </summary>
    public interface IDataStore
    {
        void Save(string path);

        void Load(string path);
    }
}