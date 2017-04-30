using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{

    public enum InstLifePhaseType : byte
    {
        /// <summary>
        /// 未上市
        /// </summary>
        NotStart,

        /// <summary>
        /// 上市
        /// </summary>
        Started,

        /// <summary>
        /// 停牌
        /// </summary>
        Pause,

        /// <summary>
        /// 到期
        /// </summary>
        Expired,

        /// <summary>
        /// 发行,参考于XSpeed
        /// </summary>
        Issue,

        /// <summary>
        /// 首日上市,参考于XSpeed
        /// </summary>
        FirstList,

        /// <summary>
        /// 退市,参考于XSpeed
        /// </summary>
        UnList,
    };
}
