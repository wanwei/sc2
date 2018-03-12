using com.wer.sc.data;
using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器抽象类
    /// </summary>
    public abstract class StrategyExecutorAbstract : IStrategyExecutor_Single
    {


        private IStrategyCenter strategyCenter;

        public IStrategyCenter StrategyCenter
        {
            get { return strategyCenter; }
        }

        public StrategyExecutorAbstract(IStrategyCenter strategyCenter, StrategyArgumentsAbstract strategyArguments)
        {
            this.strategyCenter = strategyCenter;
            this.forwardPeriod = strategyArguments.ForwardPeriod;
            this.referedPeriods = strategyArguments.ReferedPeriods;
            this.traderSetting = strategyArguments.TraderSetting;
            this.strategyHelper = strategyArguments.StrategyHelper;
            this.isSaveResult = strategyArguments.IsSaveResult;
        }

        #region Strategy

        private IStrategy strategy;

        public virtual IStrategy Strategy
        {
            get { return this.strategy; }
            set { SetStrategy(value); }
        }

        private void SetStrategy(IStrategy strategy)
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
                ((StrategyAbstract)strategy).StrategyHelper = StrategyHelper;
        }

        #endregion

        #region 参数

        private bool isSaveResult;

        private StrategyForwardPeriod forwardPeriod;

        private StrategyReferedPeriods referedPeriods;

        private StrategyTraderSetting traderSetting;

        protected IStrategyHelper strategyHelper;

        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }
        }

        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        private static StrategyTraderSetting defaultTraderSetting;

        public StrategyTraderSetting GetDefaultTraderSetting()
        {
            if (defaultTraderSetting != null)
                return defaultTraderSetting;
            defaultTraderSetting = new StrategyTraderSetting();
            defaultTraderSetting.InitMoney = 100000;
            defaultTraderSetting.AutoFilter = false;
            defaultTraderSetting.TradeType = data.account.AccountTradeType.IMMEDIATELY;
            return defaultTraderSetting;
        }

        public StrategyTraderSetting TraderSetting
        {
            get
            {
                return traderSetting;
            }
        }

        private StrategyHelper defaultStrategyHelper;

        public IStrategyHelper GetDefaultStrategyHelper()
        {
            if (defaultStrategyHelper != null)
                return defaultStrategyHelper;
            defaultStrategyHelper = new StrategyHelper();
            //初始化交易器
            StrategyTraderSetting traderSetting = GetDefaultTraderSetting();
            IAccount account = DataCenter.Default.AccountManager.CreateAccount(traderSetting.InitMoney);
            account.AccountSetting.AutoFilter = traderSetting.AutoFilter;
            account.AccountSetting.TradeType = traderSetting.TradeType;
            StrategyTrader_History trader = new StrategyTrader_History(account);
            defaultStrategyHelper.Trader = trader;
            return defaultStrategyHelper;
        }

        public virtual IStrategyHelper StrategyHelper
        {
            get
            {
                return strategyHelper;
            }
        }

        public bool IsSaveResult
        {
            get
            {
                return isSaveResult;
            }

            set
            {
                isSaveResult = value;
            }
        }
        #endregion

        #region 运行状态

        protected StrategyExecutorInfo strategyExecutorInfo;

        public virtual IStrategyExecutorInfo StrategyExecutorInfo
        {
            get { return strategyExecutorInfo; }
        }

        protected StrategyExecutorState state;

        public virtual StrategyExecutorState State
        {
            get
            {
                return state;
            }
        }

        #endregion

        #region 策略执行

        public virtual void Execute()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        protected bool isCancel = false;

        public virtual void Cancel()
        {
            this.isCancel = true;
        }

        public abstract void Run();

        #endregion

        #region 执行结果

        protected StrategyResult strategyResult;

        public virtual IStrategyResult StrategyResult
        {
            get
            {
                return strategyResult;
            }
        }

        private StrategyResult BuildStrategyResult()
        {
            StrategyResult strategyResult = new StrategyResult();
            int startDate = this.StrategyExecutorInfo.CodePeriod.StartDate;
            int endDate = this.StrategyExecutorInfo.CodePeriod.EndDate;
            strategyResult.Name = GetResultName(startDate, endDate);
            strategyResult.StartDate = startDate;
            strategyResult.EndDate = endDate;
            strategyResult.ReferedPeriods = this.ReferedPeriods;
            strategyResult.ForwardPeriod = this.ForwardPeriod;
            strategyResult.Parameters = Strategy.Parameters;
            return strategyResult;
        }

        public virtual void SaveStrategyResult()
        {
            int day = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            IStrategyResultStore store = this.strategyCenter.StrategyResultStore;
            store.Save(day, strategyResult);
            store.SaveQueryResult(day, strategyResult);
            for (int i = 0; i < strategyResult.StrategyResult_Codes.Count; i++)
                store.Save(day, strategyResult.Name, strategyResult.StrategyResult_Codes[i]);
        }

        protected string GetResultName(int start, int end)
        {
            string resultName = "";
            if (Strategy is StrategyAbstract)
            {
                resultName += ((StrategyAbstract)Strategy).Name;
            }
            else
                resultName += Strategy.GetType().ToString();

            resultName += "_" + DateTime.Now.ToString("HHmmss") + "_" + start + "-" + end;
            return resultName;
        }

        #endregion

        #region Event

        protected void DealStartEvent(StrategyStartArguments startArguments)
        {
            if (OnStart != null)
                OnStart(this, startArguments);
        }

        protected void DealBarFinishEvent(StrategyBarFinishedArguments barFinishedArguments)
        {
            if (OnBarFinished != null)
                OnBarFinished(this, barFinishedArguments);
        }

        protected bool HasBarFinishedEvent()
        {
            return OnBarFinished != null;
        }

        protected void DealDayFinishEvent(StrategyDayFinishedArguments dayFinishedArguments)
        {
            if (OnDayFinished != null)
                OnDayFinished(this, dayFinishedArguments);
        }

        protected bool HasDayFinishedEvent()
        {
            return OnDayFinished != null;
        }

        protected void DealCancelEvent(StrategyCanceledArguments canceledArguments)
        {
            if (OnCanceled != null)
                OnCanceled(this, canceledArguments);
        }

        protected void DealFinishedEvent(StrategyFinishedArguments finishedArguments)
        {
            if (OnFinished != null)
                OnFinished(this, finishedArguments);
        }

        public event StrategyStart OnStart;

        public event StrategyBarFinished OnBarFinished;

        public event StrategyDayFinished OnDayFinished;

        public event StrategyCanceled OnCanceled;

        public event StrategyFinished OnFinished;

        #endregion
    }
}
