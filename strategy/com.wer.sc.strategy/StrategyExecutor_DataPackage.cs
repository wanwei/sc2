using com.wer.sc.data;
using com.wer.sc.data.account;
using com.wer.sc.data.codeperiod;
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
    /// 数据包策略执行器
    /// 使用该执行器必须提供一个
    /// </summary>
    public class StrategyExecutor_DataPackage : StrategyExecutorAbstract, IStrategyExecutor
    {
        private object lockRunObject = new object();

        private ICodePeriod codePeriod;

        public ICodePeriod CodePeriod
        {
            get
            {
                return codePeriod;
            }
        }

        //执行时使用的数据包
        private IDataPackage_Code dataPackage;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private StrategyDayFinishedArguments tempDayFinishedArguments = null;

        private StrategyBarFinishedArguments tempBarFinishedArguments = null;

        public StrategyExecutor_DataPackage(IStrategyCenter strategyCenter, StrategyArguments_DataPackage strategyArguments) : this(strategyCenter, strategyArguments, null)
        {
        }

        public StrategyExecutor_DataPackage(IStrategyCenter strategyCenter, StrategyArguments_DataPackage strategyArguments, StrategyExecutorInfo strategyExecutorInfo) : base(strategyCenter, strategyArguments)
        {
            this.dataPackage = strategyArguments.DataPackage;
            this.codePeriod = new CodePeriod(dataPackage.Code, dataPackage.StartDate, dataPackage.EndDate);
            if (strategyExecutorInfo == null)
                this.InitStrategyExecutorInfo();
            else
                this.strategyExecutorInfo = strategyExecutorInfo;
        }

        private void InitStrategyExecutorInfo()
        {
            this.strategyExecutorInfo = new StrategyExecutorInfo(codePeriod, dataPackage.GetTradingDays().Count);
            this.strategyExecutorInfo.CurrentDay = dataPackage.GetTradingDays()[0];
            this.strategyExecutorInfo.CurrentDayIndex = 0;

            this.tempBarFinishedArguments = new StrategyBarFinishedArguments(this.strategyExecutorInfo);
            this.tempDayFinishedArguments = new StrategyDayFinishedArguments(this.strategyExecutorInfo);
        }

        private IDataForward_Code dataForward;

        public override void Run()
        {
            if (this.state != StrategyExecutorState.NotStart)
                return;
            lock (lockRunObject)
            {
                if (this.state != StrategyExecutorState.NotStart)
                    return;

                if (ReferedPeriods == null)
                    throw new ApplicationException("策略运行时引用周期为空" + dataPackage);
                if (ForwardPeriod == null)
                    throw new ApplicationException("策略运行时前进周期为空" + dataPackage);

                //创建前进器
                this.dataForward = PrepareDataForward();
                //创建StrategyHelper
                this.strategyHelper = PrepareStrategyHelper();
                if (Strategy is StrategyAbstract)
                    ((StrategyAbstract)Strategy).StrategyHelper = StrategyHelper;

                //开始执行
                this.state = StrategyExecutorState.Running;
                ExecuteStrategyStart(dataForward);
                if (isCancel)
                {
                    DealCancelEvent();
                    return;
                }
                ExecuteStrategy(dataForward);
                if (isCancel)
                {
                    DealCancelEvent();
                    return;
                }
                ExecuteStrategyEnd(dataForward);
            }
        }

        private IDataForward_Code PrepareDataForward()
        {
            IDataForward_Code dataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(dataPackage, ReferedPeriods, ForwardPeriod);
            dataForward.OnBar += RealTimeReader_OnBar;
            dataForward.OnTick += RealTimeReader_OnTick;
            return dataForward;
        }

        private IStrategyHelper PrepareStrategyHelper()
        {
            //如果在参数里设定了strategyHelper，则不再更改
            if (this.strategyHelper != null)
            {
                this.strategyHelper.Trader.Account.BindRealTimeReader(dataForward);
                return this.strategyHelper;
            }

            this.strategyHelper = GetDefaultStrategyHelper();
            this.strategyHelper.Trader.Account.BindRealTimeReader(dataForward);
            return this.strategyHelper;
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
            IStrategyOnStartArgument argument = new StrategyOnStartArgument(dataForward, ReferedPeriods, ForwardPeriod);
            ExecuteReferStrategyStart(Strategy, argument);
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
                ExecuteReferStrategyEnd(Strategy, argument);
                this.BuildStrategyResult();
                if (IsSaveResult)
                    this.SaveStrategyResult();
                DealFinishedEvent(new StrategyFinishedArguments(this.Strategy, this.strategyExecutorInfo, this.strategyResult));
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

        private void BuildStrategyResult()
        {
            StrategyResult strategyResult = new StrategyResult();
            strategyResult.Name = GetResultName();
            strategyResult.CodePeriods.Add(this.CodePeriod);
            strategyResult.StartDate = dataPackage.StartDate;
            strategyResult.EndDate = dataPackage.EndDate;
            strategyResult.ReferedPeriods = this.ReferedPeriods;
            strategyResult.ForwardPeriod = this.ForwardPeriod;
            strategyResult.Parameters = Strategy.Parameters;
            strategyResult.StrategyQueryResultManager = strategyHelper.QueryResultManager;

            //绘图暂时不处理，绘图需要特别处理，不是一个container能解决的
            IStrategyGraphicContainer shapeContainer = null;
            StrategyResult_CodePeriod strategyResult_CodePeriod = new StrategyResult_CodePeriod(CodePeriod, ForwardPeriod, ReferedPeriods, shapeContainer, StrategyHelper.Trader);
            strategyResult.AddStrategyResult_Code(strategyResult_CodePeriod);
            this.strategyResult = strategyResult;
        }

        private string GetResultName()
        {
            string resultName = "";
            if (Strategy is StrategyAbstract)
            {
                resultName += ((StrategyAbstract)Strategy).Name;
            }
            else
                resultName += Strategy.GetType().ToString();

            resultName += "_" + DateTime.Now.ToString("HHmmss") + "_" + dataPackage.StartDate + "-" + dataPackage.EndDate;
            return resultName;
        }

        private void RealTimeReader_OnTick(object sender, IForwardOnTickArgument argument)
        {
            OnTick_ReferedStrategies(this.Strategy, argument);
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
            OnBar_ReferedStrategies(this.Strategy, argument);
            IKLineData_Extend mainKLineData = argument.MainBar.KLineData;
            if (this.strategyExecutorInfo != null)
                this.strategyExecutorInfo.CurrentKLineData = mainKLineData;
            if (HasBarFinishedEvent())
            {
                if (tempBarFinishedArguments == null)
                    tempBarFinishedArguments = new StrategyBarFinishedArguments(this.strategyExecutorInfo);
                DealBarFinishEvent(tempBarFinishedArguments);
            }
            if (HasDayFinishedEvent() && mainKLineData.IsDayEnd())
            {
                if (tempDayFinishedArguments == null)
                    tempDayFinishedArguments = new StrategyDayFinishedArguments(this.strategyExecutorInfo);
                DealDayFinishEvent(tempDayFinishedArguments);
            }

            this.strategyExecutorInfo.CurrentKLineData = mainKLineData;
            this.strategyExecutorInfo.CurrentDay = mainKLineData.GetTradingTime().TradingDay;

            if (mainKLineData.IsDayStart())
            {
                this.strategyExecutorInfo.CurrentDayIndex++;
                //this.strategyExecutorInfo.CurrentDay = mainKLineData.GetTradingTime().TradingDay;
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

        private void DealCancelEvent()
        {
            this.state = StrategyExecutorState.Canceled;
            DealCancelEvent(new StrategyCanceledArguments(this.strategyExecutorInfo));
        }
    }
}