using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// 策略启动时执行该方法
        /// </summary>
        void StrategyStart();

        /// <summary>
        /// 策略结束时执行该方法
        /// </summary>
        void StrategyEnd();

        /// <summary>
        /// 每接收到一个tick触发该方法
        /// 需要在GetStrategyPeriods里面设置usetick=true
        /// </summary>
        /// <param name="currentData"></param>
        void OnTick(IRealTimeDataReader_Code currentData);

        /// <summary>
        /// 每到一个bar结束触发该方法
        /// 需要在GetStrategyPeriods里面设置usetick=true
        /// </summary>
        /// <param name="currentData"></param>
        void OnBar(IRealTimeDataReader_Code currentData);

        /// <summary>
        /// 返回该策略使用的周期
        /// 该方法如果返回空，则通过在执行策略的时候设置参数得到策略周期
        /// </summary>
        /// <returns></returns>
        StrategyReferedPeriods GetStrategyPeriods();

        /// <summary>
        /// 得到执行策略帮助类，得到策略执行时用到的一些函数，还有绘图使用的一些方法。
        /// </summary>
        IStrategyHelper StrategyHelper { get; set; }

        /// <summary>
        /// 得到该策略的参数
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// 缺省主周期
        /// </summary>
        KLinePeriod DefaultMainPeriod { get; set; }

        /// <summary>
        /// 该策略引用的其它策略
        /// </summary>
        IList<IStrategy> GetReferedStrategies();
    }   
}