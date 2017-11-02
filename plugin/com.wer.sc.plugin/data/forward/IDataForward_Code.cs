using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 单支合约的历史数据导航器
    /// 
    /// </summary>
    public interface IDataForward_Code : IRealTimeDataReader_Code, IXmlExchange
    {
        #region 数据

        /// <summary>
        /// 得到起始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 得到数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }

        /// <summary>
        /// 附加其它数据信息
        /// 比如现在交易螺纹钢合约rb1801，想看螺纹钢指数rb0000的1分钟数据
        /// 那就用该方法将数据加进来
        /// </summary>
        /// <param name="code"></param>
        /// <param name="referedPeriods"></param>
        void AttachOtherData(string code);

        /// <summary>
        /// 附加数据读取器，附加进来的数据用该接口读取
        /// </summary>
        IRealTimeDataReader_Code GetAttachedDataReader(string code);

        /// <summary>
        /// 得到附加上来的codes
        /// </summary>
        /// <returns></returns>
        List<string> GetAttachedCodes();

        #endregion

        #region 直接跳转

        /// <summary>
        /// 跳转到指定时间
        /// </summary>
        /// <param name="time"></param>
        void NavigateTo(double time);

        //event DelegateOnNavigateTo OnNavigateTo;

        #endregion

        #region 历史数据模拟前进

        /// <summary>
        /// 得到下一个
        /// </summary>
        /// <returns></returns>
        double GetNextTime();

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward();

        /// <summary>
        /// 自动前进
        /// </summary>
        void Play();

        /// <summary>
        /// 停止自动前进
        /// </summary>
        void Pause();

        /// <summary>
        /// 每次前进的周期
        /// </summary>
        ForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 得到前进时的主K线，如果是以tick前进，则返回空
        /// </summary>
        /// <returns></returns>
        IKLineData GetKLineData();

        /// <summary>
        /// 前进器接收到一个新tick时，触发该事件
        /// 只有当前进器以tick为周期前进时才执行该事件
        /// </summary>
        event DelegateOnTick OnTick;

        /// <summary>
        /// 前进器在主K线的bar完全生成后后执行该事件
        /// 在前进器是tick或K线周期都会执行陔事件，但触发时机略有不同
        /// 注意如果前进器是以tick为周期前进，则到下一个bar生成的那一刻该委托才会触发
        /// 如果前进器是以K线为周期前进，那么在bar结束的时候触发该委托。
        /// 
        /// 如果在前进器有多个K线，那么onbar的时候只会执行那么bar完全生成的K线
        /// 比如现在时间是20170904.090500，那么该事件只会触发1分钟和5分钟的K线返回，15分钟不会触发
        /// </summary>
        event DelegateOnBar OnBar;

        #endregion

        #region 当前的前进信息

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        bool IsEnd { get; }

        /// <summary>
        /// 是否是一天的开始
        /// </summary>
        bool IsDayStart { get; }

        /// <summary>
        /// 是否是一天的结束
        /// </summary>
        bool IsDayEnd { get; }

        /// <summary>
        /// 是否是一个交易时段的开始
        /// </summary>
        bool IsTradingTimeStart { get; }

        /// <summary>
        /// 是否是一段交易时间的结束
        /// </summary>
        bool IsTradingTimeEnd { get; }

        #endregion
    }
}