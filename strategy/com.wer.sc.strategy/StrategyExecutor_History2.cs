using com.wer.sc.data;
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
    /// 
    /// </summary>
    public class StrategyExecutor_History2 : IStrategyExecutor
    {
        private IStrategy strategy;

        private bool isRunning;

        private ForwardReferedPeriods referedPeriods;

        private IDataPackage_Code dataPackage;

        private ForwardPeriod forwardPeriod;

        private StrategyOperator strategyHelper;

        private IStrategyReport report;

        public StrategyExecutor_History2(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod) : this(dataPackage, referedPeriods, forwardPeriod, new StrategyOperator(null))
        {

        }

        public StrategyExecutor_History2(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod, StrategyOperator strategyHelper)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
            this.strategyHelper = strategyHelper;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            this.strategy.StrategyOperator = strategyHelper;
            ForwardReferedPeriods rPeriods = strategy.GetStrategyPeriods();
            if (rPeriods != null)
                this.referedPeriods = rPeriods;
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

                StrategyTrader trader = new StrategyTrader(100000, dataForward);
                this.strategyHelper.Trader = trader.GetStrategyTrader(dataPackage.Code);

                ExecuteStrategyStart();
                if (isCancel)
                    return;
                bool continueExecute = ExecuteStrategy(dataForward);
                if (!continueExecute)
                    return;
                if (isCancel)
                    return;
                ExecuteStrategyEnd();
            }
        }

        public IStrategyReport StrategyReport
        {
            get
            {
                return report;
            }
        }

        private void ExecuteStrategyEnd()
        {
            //策略执行完毕
            try
            {
                this.strategy.OnStrategyEnd(this, null);
                this.BuildStrategyReport();
                if (ExecuteFinished != null)
                    ExecuteFinished(this.strategy, new StrategyExecuteFinishedArguments(this.report));
            }
            catch (Exception e)
            {
                LogHelper.Warn(GetType(), e);
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
            report.strategyResult = strategyHelper.Results;
            report.strategyTrader = strategyHelper.Trader.OwnerTrader;
            this.report = report;
        }

        private bool ExecuteStrategy(IDataForward_Code dataForward)
        {
            //执行策略
            while (!dataForward.IsEnd)
            {
                try
                {
                    dataForward.Forward();
                    if (isCancel)
                        return false;
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                }
            }
            return true;
        }

        private void ExecuteStrategyStart()
        {
            //策略执行前操作
            try
            {
                ExecuteReferStrategyStart(strategy);
            }
            catch (Exception e)
            {
                LogHelper.Warn(GetType(), e);
            }
        }

        private void ExecuteReferStrategyStart(IStrategy strategy)
        {
            IList<IStrategy> strategies = strategy.GetReferedStrategies();
            if (strategies != null)
            {
                for (int i = 0; i < strategies.Count; i++)
                {
                    IStrategy refstrategy = strategies[i];
                    ExecuteReferStrategyStart(refstrategy);
                }
            }
            strategy.OnStrategyStart(this, null);
        }

        private void RealTimeReader_OnTick(object sender, ForwardOnTickArgument argument)
        {
            OnTick_ReferedStrategies(this.strategy, (IRealTimeDataReader_Code)sender);
        }

        private void OnTick_ReferedStrategies(IStrategy strategy, IRealTimeDataReader_Code realTimeDataReader)
        {
            IList<IStrategy> referedStrategies = strategy.GetReferedStrategies();
            if (referedStrategies != null)
            {
                for (int i = 0; i < referedStrategies.Count; i++)
                {
                    IStrategy referedStrategy = referedStrategies[i];
                    OnTick_ReferedStrategies(referedStrategy, realTimeDataReader);
                }
            }
            strategy.OnTick(this, new StrategyOnTickArgument(realTimeDataReader));
        }

        private void RealTimeReader_OnBar(object sender, ForwardOnBarArgument argument)
        {
            //IRealTimeDataReader_Code realtimeData =argument
            List<StrategyOnBarInfo> strategies = new List<StrategyOnBarInfo>();
            for (int i = 0; i < argument.ForwardOnBar_Infos.Count; i++)
            {
                //argument.ForwardOnBar_Infos[i].
            }
            //StrategyOnBarArgument strategyArgument = new StrategyOnBarArgument((IRealTimeDataReader_Code)sender,;
            //OnBar_ReferedStrategies(this.strategy, , strategyArgument);
        }

        private void OnBar_ReferedStrategies(IStrategy strategy, StrategyOnBarArgument argument)
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
            strategy.OnBar(this, argument);
        }

        /// <summary>
        /// 
        /// </summary>
        public event StrategyExecuteBarFinished BarFinished;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyExecuteDayFinished DayFinished;

        /// <summary>
        /// 执行完
        /// </summary>
        public event StrategyExecuteFinished ExecuteFinished;
    }
}