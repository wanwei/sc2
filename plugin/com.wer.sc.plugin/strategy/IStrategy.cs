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
    /// 最好的实现方法是继承自StrategyAbstract，这样即使IStrategy接口有变化，受到的影响也会最小
    /// 
    /// 策略接口包括几个方面：
    /// 1.初始化属性：
    ///     StrategyReferedPeriods：设置策略侦听的数据，如tick，1分钟线。也可以返回空由外部决定数据
    ///     Parameters：参数属性，策略执行初始化时设置给策略，比如给均线设置均线周期
    /// 2.事件侦听：
    ///     OnTick  接收到一个新的tick数据后触发该方法啊
    ///     OnBar   接收数据时形成一个新的K线柱子触发该事件
    ///     OnStrategyStart 策略准备完成，开始接收数据前触发该方法
    ///     OnStrategyEnd   策略所有数据接收并处理完成后触发该方法
    /// 3.用户操作：
    ///     StrategyOperator  策略操作接口，用户可以执行画图，交易等操作
    /// 4.对其它策略的引用：
    ///     ReferedStrategies
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
        StrategyReferedPeriods GetStrategyPeriods();

        /// <summary>
        /// 得到该策略的参数
        /// 该属性会在策略执行初始化时设置给策略，给策略提供最大的灵活性
        /// 比如完成一个均线策略，但在策略里又不想把均线的周期固定，可以将均线周期作为参数，在执行时决定
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// 策略启动时执行该方法
        /// 可以用来设置策略的参数
        /// </summary>
        void OnStrategyStart(Object sender, StrategyOnStartArgument argument);

        /// <summary>
        /// 策略结束时执行该方法
        /// 可以用来执行画图等方法
        /// </summary>
        void OnStrategyEnd(Object sender, StrategyOnEndArgument argument);

        /// <summary>
        /// 每接收到一个tick触发该方法
        /// 需要在GetStrategyPeriods里面设置usetick=true
        /// </summary>
        /// <param name="currentData"></param>
        void OnTick(Object sender, StrategyOnTickArgument currentData);

        /// <summary>
        /// 每到一个bar结束触发该方法
        /// 需要在GetStrategyPeriods里面设置usetick=true
        /// </summary>
        /// <param name="currentData"></param>
        void OnBar(Object sender, StrategyOnBarArgument currentData);

        /// <summary>
        /// 得到执行策略帮助类，得到策略执行时用到的一些函数，还有绘图使用的一些方法。
        /// </summary>
        IStrategyOperator StrategyOperator { get; set; }

        /// <summary>
        /// 该策略引用的其它策略
        /// </summary>
        IList<IStrategy> GetReferedStrategies();
    }
}