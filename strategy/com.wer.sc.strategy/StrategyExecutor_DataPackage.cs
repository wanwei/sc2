using com.wer.sc.data;
using com.wer.sc.data.account;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// 策略执行前进周期：tick或者K线
    /// </summary>
    public class StrategyExecutor_DataPackage : IStrategyExecutor
    {
        private StrategyDayFinishedArguments dayFinishedArguments = null;

        private StrategyBarFinishedArguments barFinishedArguments = null;

        private StrategyFinishedArguments finishedArguments = null;

        private ICodePeriod codePeriod;

        private IStrategy strategy;

        private StrategyExecutorInfo strategyExecutorInfo;

        public IStrategy Strategy
        {
            get { return this.strategy; }
            set { SetStrategy(value); }
        }

        private IDataPackage_Code dataPackage;

        private StrategyReferedPeriods referedPeriods;

        private StrategyForwardPeriod forwardPeriod;

        private StrategyHelper strategyHelper;

        private bool isRunning;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private IStrategyResult report;

        public StrategyExecutor_DataPackage(StrategyArguments_DataPackage strategyArguments) : this(strategyArguments, new StrategyHelper(null))
        {

        }

        public StrategyExecutor_DataPackage(StrategyArguments_DataPackage strategyArguments, IStrategyHelper strategyHelper)
        {
            this.dataPackage = strategyArguments.DataPackage;
            this.referedPeriods = strategyArguments.ReferedPeriods;
            this.forwardPeriod = strategyArguments.ForwardPeriod;
            this.codePeriod = new CodePeriod(dataPackage.Code, dataPackage.StartDate, dataPackage.EndDate);
            this.InitStrategyExecutorInfo();
            this.strategyHelper = (StrategyHelper)strategyHelper;
        }

        private void InitStrategyExecutorInfo()
        {
            this.strategyExecutorInfo = new StrategyExecutorInfo(codePeriod, dataPackage.GetTradingDays().Count);
            this.strategyExecutorInfo.CurrentDay = dataPackage.GetTradingDays()[0];
            this.strategyExecutorInfo.CurrentDayIndex = 0;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            InitStrategy(this.strategy);
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            if (referedPeriods != null)
                this.referedPeriods = referedPeriods;
            IList<IStrategy> referedStrategies = this.strategy.GetReferedStrategies();
            if (referedStrategies != null)
            {
                for (int i = 0; i < referedStrategies.Count; i++)
                {
                    InitStrategy(referedStrategies[i]);
                }
            }
        }

        private void InitStrategy(IStrategy strategy)
        {
            if (strategy is StrategyAbstract)
                ((StrategyAbstract)strategy).StrategyOperator = strategyHelper;
        }

        private object lockObj = new object();

        public void Execute()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private bool isCancel = false;

        public void Cancel()
        {
            this.isCancel = true;
        }

        public void Run()
        {
            lock (lockObj)
            {
                if (isRunning)
                    return;
                isRunning = true;
                isCancel = false;

                IDataForward_Code dataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
                dataForward.OnBar += RealTimeReader_OnBar;
                dataForward.OnTick += RealTimeReader_OnTick;

                IAccount account = DataCenter.Default.AccountManager.CreateAccount(100000);
                account.BindRealTimeReader(dataForward);
                StrategyTrader_History trader = new StrategyTrader_History(account);
                this.strategyHelper.Trader = trader;//.GetStrategyTrader(dataPackage.Code);

                ExecuteStrategyStart(dataForward);
                if (isCancel)
                    return;
                bool continueExecute = ExecuteStrategy(dataForward);
                if (!continueExecute)
                    return;
                if (isCancel)
                    return;
                ExecuteStrategyEnd(dataForward);
            }
        }

        public IStrategyResult StrategyReport
        {
            get
            {
                return report;
            }
        }

        private void BuildStrategyReport()
        {
            StrategyReport report = new StrategyReport();
            report.code = dataPackage.Code;
            report.startDate = dataPackage.StartDate;
            report.endDate = dataPackage.EndDate;
            report.forwardPeriod = forwardPeriod;
            report.parameters = strategy.Parameters;
            report.strategyResultManager = strategyHelper.QueryResultManager;
            report.strategyTrader = strategyHelper.Trader;
            this.report = report;
        }

        private bool ExecuteStrategy(IDataForward_Code realTimeReader)
        {
            //执行策略
            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
                if (isCancel)
                    return false;
            }
            return true;
        }

        private void ExecuteStrategyStart(IDataForward_Code dataForward)
        {
            IStrategyOnStartArgument argument = new StrategyOnStartArgument(dataForward, referedPeriods, forwardPeriod);
            ExecuteReferStrategyStart(strategy, argument);
        }

        private void ExecuteReferStrategyStart(IStrategy strategy, IStrategyOnStartArgument argument)
        {
            strategy.OnStart(this, argument);
            IList<IStrategy> strategies = strategy.GetReferedStrategies();
            if (strategies != null)
            {
                for (int i = 0; i < strategies.Count; i++)
                {
                    IStrategy refstrategy = strategies[i];
                    ExecuteReferStrategyStart(refstrategy, argument);
                }
            }
        }

        private void ExecuteStrategyEnd(IDataForward_Code dataForward)
        {
            //策略执行完毕
            try
            {
                IStrategyOnEndArgument argument = new StrategyOnEndArgument(dataForward);
                ExecuteReferStrategyEnd(strategy, argument);
                this.BuildStrategyReport();
                OnFinished?.Invoke(this.strategy, new StrategyFinishedArguments(this.strategy, this.strategyExecutorInfo, this.report));
            }
            catch (Exception e)
            {
                LogHelper.Warn(this.GetType(), e);
            }
        }

        private void ExecuteReferStrategyEnd(IStrategy strategy, IStrategyOnEndArgument argument)
        {
            strategy.OnEnd(this, argument);
            IList<IStrategy> strategies = strategy.GetReferedStrategies();
            if (strategies != null)
            {
                for (int i = 0; i < strategies.Count; i++)
                {
                    IStrategy refstrategy = strategies[i];
                    ExecuteReferStrategyEnd(refstrategy, argument);
                }
            }
        }

        private void RealTimeReader_OnTick(object sender, IForwardOnTickArgument argument)
        {
            OnTick_ReferedStrategies(this.strategy, argument);
        }

        private void OnTick_ReferedStrategies(IStrategy strategy, IForwardOnTickArgument argument)
        {
            IList<IStrategy> referedStrategies = strategy.GetReferedStrategies();
            if (referedStrategies != null)
            {
                for (int i = 0; i < referedStrategies.Count; i++)
                {
                    IStrategy referedStrategy = referedStrategies[i];
                    OnTick_ReferedStrategies(referedStrategy, argument);
                }
            }

            IForwardTickInfo forwardTickInfo = argument.TickInfo;
            StrategyOnTickArgument strategyArgument = new StrategyOnTickArgument((ForwardOnTickArgument)argument);
            strategy.OnTick(this, strategyArgument);
        }

        private void RealTimeReader_OnBar(object sender, IForwardOnBarArgument argument)
        {
            OnBar_ReferedStrategies(this.strategy, argument);
            IKLineData_Extend mainKLineData = argument.MainBar.KLineData;
            if (this.strategyExecutorInfo != null)
                this.strategyExecutorInfo.CurrentKLineData = mainKLineData;
            if (OnBarFinished != null)
            {
                if (barFinishedArguments == null)
                    barFinishedArguments = new StrategyBarFinishedArguments(this.strategyExecutorInfo);
                OnBarFinished(strategy, barFinishedArguments);
            }
            if (OnDayFinished != null && mainKLineData.IsDayEnd())
            {
                if (dayFinishedArguments == null)
                    dayFinishedArguments = new StrategyDayFinishedArguments(this.strategyExecutorInfo);
                OnDayFinished(strategy, dayFinishedArguments);
            }
            if (mainKLineData.IsDayStart())
            {
                this.strategyExecutorInfo.CurrentDayIndex++;
                this.strategyExecutorInfo.CurrentDay = mainKLineData.GetTradingTime().TradingDay;

            }
        }

        private void OnBar_ReferedStrategies(IStrategy strategy, IForwardOnBarArgument argument)
        {
            IList<IStrategy> referedStrategies = strategy.GetReferedStrategies();
            if (referedStrategies != null)
            {
                for (int i = 0; i < referedStrategies.Count; i++)
                {
                    IStrategy referedStrategy = referedStrategies[i];
                    OnBar_ReferedStrategies(referedStrategy, argument);
                }
            }
            strategy.OnBar(this, new StrategyOnBarArgument((ForwardOnBarArgument)argument));
        }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                return this.strategyExecutorInfo;
            }
        }

        public ICodePeriod CodePeriod
        {
            get
            {
                return codePeriod;
            }
        }

        public event StrategyStart OnStart;

        /// <summary>
        /// 执行完每一个bar
        /// </summary>
        public event StrategyBarFinished OnBarFinished;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyDayFinished OnDayFinished;

        /// <summary>
        /// 执行完
        /// </summary>
        public event StrategyFinished OnFinished;
    }
}