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
    /// 策略是整个系统的核心接口，负责进行数据分析，量化交易等。
    /// 不建议直接实现该接口，最好的实现策略的方法是继承自StrategyAbstract，这样即使IStrategy接口有变化，实现的策略也可能不需要修改
    /// 
    /// 策略接口包括几个方面：
    /// 1.初始化属性：
    ///     StrategyReferedPeriods：设置策略侦听的数据，如tick，1分钟线。也可以返回空由外部决定数据
    ///     ReferedStrategies：设定对其它策略的引用
    ///     ReferedCodes：设定对其它股票或期货合约的引用
    ///     Parameters：参数属性，策略执行初始化时设置给策略，比如给均线设置均线周期
    /// 2.事件侦听：
    ///     OnTick  接收到一个新的tick数据后触发该方法啊
    ///     OnBar   接收数据时形成一个新的K线柱子触发该事件
    ///     OnStrategyStart 策略准备完成，开始接收数据前触发该方法
    ///     OnStrategyEnd   策略所有数据接收并处理完成后触发该方法
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// 该方法用来设置策略引用的周期，比如tick，1分钟K线等
        /// 在执行策略时，执行器会根据该属性准备数据；策略接收数据时也会根据该方法决定策略的数据接收周期
        /// 如UseTickData=True才会触发OnTick事件，UsedKLinePeriods里面最小周期决定了OnBar事件触发频率
        /// 
        /// 该方法如果返回空，那么执行器会在外部自己准备数据，这种使用方式一般有两种：
        ///     1.一些只会被引用的策略，根据引用它的策略决定执行周期，如Strategy_Ma
        ///     2.专门界面画图的策略，会根据显示的K线图周期确定画图形式
        /// </summary>
        /// <returns></returns>
        StrategyReferedPeriods GetReferedPeriods();

        /// <summary>
        /// 得到策略引用的其它股票或期货数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<string> GetStrategyReferedCodes(string code);

        /// <summary>
        /// 该策略引用的其它策略
        /// </summary>
        IList<IStrategy> GetReferedStrategies();

        /// <summary>
        /// 得到该策略的参数
        /// 该属性会在策略执行初始化时设置给策略，给策略提供最大的灵活性
        /// 比如完成一个均线策略，但在策略里又不想把均线的周期固定，可以将均线周期作为参数，在执行时决定
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// 策略启动时执行该方法
        /// 可以用来初始化策略的参数
        /// </summary>
        void OnStart(Object sender, IStrategyOnStartArgument argument);

        /// <summary>
        /// 策略结束时执行该方法
        /// 可以用来执行画图等方法
        /// </summary>
        void OnEnd(Object sender, IStrategyOnEndArgument argument);

        /// <summary>
        /// 每接收到一条tick数据触发该方法
        /// 如果策略仅引用K线，则不会触发该事件
        /// 要触发该事件有两个方法：
        /// 1.在GetReferedPeriods()方法里配置对tick的引用
        /// 2.执行事件时设置引用tick数据
        /// </summary>
        /// <param name="data"></param>
        void OnTick(Object sender, IStrategyOnTickArgument data);

        /// <summary>
        /// 每当该bar的结束时间到了触发该事件
        /// 对于主合约一般是接收到最后一条tick数据触发该方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void OnBar(Object sender, IStrategyOnBarArgument data);
    }
}