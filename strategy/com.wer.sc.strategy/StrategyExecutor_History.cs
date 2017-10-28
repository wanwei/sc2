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
    public class StrategyExecutor_History : IStrategyExecutor
    {
        private IStrategy strategy;

        private bool isRunning;

        private ForwardReferedPeriods referedPeriods;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private IDataPackage_Code dataPackage;

        private ForwardPeriod forwardPeriod;

        private StrategyOperator strategyHelper;

        private IStrategyReport report;

        private List<ForwardOnbar_Info> barInfos = new List<ForwardOnbar_Info>();

        public StrategyExecutor_History(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod) : this(dataPackage, referedPeriods, forwardPeriod, new StrategyOperator(null))
        {

        }

        public StrategyExecutor_History(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod, IStrategyOperator strategyHelper)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
            this.strategyHelper = (StrategyOperator)strategyHelper;
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

                //RealTimeReader_Strategy realTimeReader = new RealTimeReader_Strategy(dataPackage, referedPeriods, forwardPeriod);
                //realTimeReader.OnBar += RealTimeReader_OnBar;
                //realTimeReader.OnTick += RealTimeReader_OnTick;
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

                //this.strategyResults = this.strategyHelper.Results;
                //this.strategyReader_Code = this.strategyHelper.Trader;
            }
        }

        //private IStrategyTrader_Code strategyReader_Code;

        //private IStrategyResult strategyResults;

        //public IStrategyResult StrategyResults
        //{
        //    get
        //    {
        //        return strategyResults;
        //    }
        //}

        //public IStrategyTrader_Code StrategyTrader
        //{
        //    get
        //    {
        //        return strategyReader_Code;
        //    }
        //}

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

        private bool ExecuteStrategy(IDataForward_Code realTimeReader)
        {
            //if (forwardPeriod.IsTickForward)
            //    RealTimeReader_OnTick(realTimeReader, realTimeReader.GetTickData(), 0);
            //else
            //    RealTimeReader_OnBar(realTimeReader, realTimeReader.GetKLineData(), 0);

            //执行策略
            while (!realTimeReader.IsEnd)
            {
                try
                {
                    realTimeReader.Forward();
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
            strategy.OnStrategyStart(this, null);
            IList<IStrategy> strategies = strategy.GetReferedStrategies();
            if (strategies != null)
            {
                for (int i = 0; i < strategies.Count; i++)
                {
                    IStrategy refstrategy = strategies[i];
                    ExecuteReferStrategyStart(refstrategy);
                }
            }            
        }

        private void RealTimeReader_OnTick(object sender, ForwardOnTickArgument argument)
        {
            OnTick_ReferedStrategies(this.strategy, (IRealTimeDataReader_Code)sender);
        }

        private StrategyOnTickArgument argument;

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
            if (argument == null)
                argument = new StrategyOnTickArgument(realTimeDataReader);
            strategy.OnTick(this, argument);
        }

        private void RealTimeReader_OnBar(object sender, ForwardOnBarArgument argument)
        {
            OnBar_ReferedStrategies(this.strategy, (IRealTimeDataReader_Code)sender, argument);
        }

        private void OnBar_ReferedStrategies(IStrategy strategy, IRealTimeDataReader_Code realTimeDataReader, ForwardOnBarArgument argument)
        {
            IList<IStrategy> referedStrategies = strategy.GetReferedStrategies();
            if (referedStrategies != null)
            {
                for (int i = 0; i < referedStrategies.Count; i++)
                {
                    IStrategy referedStrategy = referedStrategies[i];
                    OnBar_ReferedStrategies(referedStrategy, realTimeDataReader, argument);
                }
            }
            barInfos.Clear();
            barInfos.AddRange(argument.ForwardOnBar_Infos);
            strategy.OnBar(this, new StrategyOnBarArgument(realTimeDataReader, barInfos));
        }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                return null;
            }
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