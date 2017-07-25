using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public interface IGraphicData_CurrentInfo : IGraphicData
    {
        /// <summary>
        /// 得到当前信息
        /// </summary>
        /// <returns></returns>
        CurrentInfo GetCurrentInfo();

        /// <summary>
        /// 得到当前的tick数据
        /// </summary>
        /// <returns></returns>
        ITickData GetCurrentTickData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentInfo"></param>
        /// <param name="tickData"></param>
        void ChangeData(CurrentInfo currentInfo, ITickData tickData);
    }
}