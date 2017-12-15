using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 数据导航
    /// </summary>
    public interface IDataNavigate : IDataNavigate_Code
    {
        /// <summary>
        /// 修改当前Code
        /// </summary>
        /// <param name="code"></param>
        void Change(string code);

        /// <summary>
        /// 修改当前Code和时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        void Change(string code, double time);

        /// <summary>
        /// 使用新的数据包修改当前code和时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        void ChangeByDataPackage(IDataPackage_Code dataPackage, double time);

        bool IsPlaying { get; }

        void Play();

        void Pause();
    }
}
