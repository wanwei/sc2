using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 登录返回信息
    /// </summary>
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct LoginInfo
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public int TradingDay;

        /// <summary>
        /// 时间
        /// </summary>
        public int LoginTime;

        /// <summary>
        /// 该字串记录服务器端
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string SessionID;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserID;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string AccountID;

        /// <summary>
        /// 投资者名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 81)]
        public string InvestorName;

        /// <summary>
        /// 错误代码
        /// </summary>
        public int XErrorID;

        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public string Text;
    }
}
