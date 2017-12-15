using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 在chart组件上执行策略，实现以下功能：
    /// 将一个策略绑定到chart组件上，每当该chart上数据发生变化时
    /// 策略会重新执行并重新在chart组件上绘图。
    /// </summary>
    public class CompChartStrategyBinder
    {
        //绑定的画图组件
        private CompChart compChart1;

        public CompChart CompChart
        {
            get { return compChart1; }
        }

        //private StrategyInfo strategyInfo;

        //当前使用的策略
        private IStrategy strategy;

        //当前数据包
        private IDataPackage_Code dataPackage;

        public CompChartStrategyBinder(CompChart compChart1)
        {
            this.compChart1 = compChart1;
            this.dataPackage = compChart1.CompChartData.DataPackage;
            this.compChart1.OnChartRefresh += CompChart1_OnChartRefresh;
        }

        private void CompChart1_OnChartRefresh(object sender, ChartRefreshArguments arg)
        {
            if (arg.DataRefreshed)
            {
                if (strategy == null)
                    return;

                if (arg.IsDataPackageChange)
                {
                    this.dataPackage = compChart1.CompChartData.DataPackage;
                    ExecuteStrategy(dataPackage, strategy);
                    return;
                }

                /*
                 * 当前是K线时可能需要重新计算
                 * 1.对于通用周期策略，重新计算策略。
                 * 2.对于非通用的，直接刷新即可。
                 */
                if (arg.CurrentChartState.ChartDataState.chartType == ChartType.KLine && arg.IsKLinePeriodChange)
                {
                    ForwardReferedPeriods referedPeriods = strategy.GetReferedPeriods();
                    if (referedPeriods != null)
                    {
                        this.compChart1.PaintChart();
                        return;
                    }
                    ExecuteStrategy(dataPackage, strategy);
                    return;
                }

                this.compChart1.PaintChart();
            }
        }

        public CompChartStrategyBinder(CompChart compChart1, IStrategy strategy) : this(compChart1)
        {
            BindStrategy(strategy);
        }

        /// <summary>
        /// 在新的数据包下执行策略
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="strategy"></param>
        public void BindStrategy(IDataPackage_Code dataPackage, IStrategy strategy)
        {
            this.strategy = strategy;
            this.dataPackage = dataPackage;
            ExecuteStrategy(dataPackage, strategy);
        }

        private CompChartStrategyExecuter strategyExecutor;

        public IStrategyExecutor StrategyExecutor
        {
            get
            {
                if (strategyExecutor == null)
                    return null;
                return strategyExecutor.StrategyExecutor;
            }
        }

        private void ExecuteStrategy(IDataPackage_Code dataPackage, IStrategy strategy)
        {
            IStrategy newStrategy = StrategyInfo.CreateNewStrategyWithParameters(strategy);
            strategyExecutor = new CompChartStrategyExecuter(this.compChart1, dataPackage, newStrategy);
            if (this.compChart1.ChartType == ChartType.KLine)
            {
                KLinePeriod currentPeriod = this.compChart1.KlinePeriod;
                compChart1.StrategyHelper.DrawOperator.GetDrawer_KLine(currentPeriod).ClearShapes();
            }
            strategyExecutor.Execute();
        }

        public void BindStrategy(IDataPackage_Code dataPackage)
        {
            if (this.strategy == null)
                return;
            this.dataPackage = dataPackage;
            //CompChartStrategyExecuter strategyExecutor = new CompChartStrategyExecuter(this.compChart1, dataPackage, this.strategy);
            //strategyExecutor.Execute();
            ExecuteStrategy(dataPackage, strategy);
        }

        public void BindStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            ExecuteStrategy(dataPackage, strategy);
        }

        //public void BindStrategy(IStrategy strategy)
        //{
        //    this.strategy = strategy;
        //    //CompChartStrategyExecuter strategyExecutor = new CompChartStrategyExecuter(this.compChart1, this.dataPackage, strategy);
        //    //strategyExecutor.Execute();
        //    ExecuteStrategy(dataPackage, strategy);
        //}

        //private void StrategyExecutor_ExecuteFinished(IStrategy strategy)
        //{
        //    //策略执行完后刷新chart
        //    this.compChart1.Refresh();
        //}

        //private IStrategyExecutor GetStrategyExecutor(IDataPackage dataPackage, IStrategy strategy)
        //{
        //    StrategyReferedPeriods referPeriods = GetKLinePeriod(strategy);
        //    KLinePeriod period = referPeriods.GetMinPeriod();
        //    if (period == null)
        //        period = KLinePeriod.KLinePeriod_1Minute;
        //    ForwardPeriod forwardPeriod = new ForwardPeriod(false, period);
        //    if (dataPackage == null)
        //        dataPackage = compChart1.CompChartData.DataPackage;
        //    return StrategyExecutorFactory.CreateHistoryExecutor(dataPackage, referPeriods, forwardPeriod, GetStrategyHelper());
        //}

        //private StrategyHelper GetStrategyHelper()
        //{
        //    return compChart1.StrategyHelper;
        //}

        //private StrategyReferedPeriods GetKLinePeriod(IStrategy strategy)
        //{
        //    StrategyReferedPeriods referedPeriods = strategy.GetStrategyPeriods();
        //    if (referedPeriods != null)
        //        return referedPeriods;
        //    referedPeriods = new StrategyReferedPeriods();
        //    //compChart1.KlinePeriod
        //    KLinePeriod period = compChart1.GetKLinePeriod();
        //    referedPeriods.UsedKLinePeriods.Add(period);
        //    return referedPeriods;
        //}

        //private IStrategyExecutor CreateExecutor(IDataPackage dataPackage, IStrategy strategy, KLinePeriod period)
        //{
        //    StrategyReferedPeriods referedPeriods = strategy.GetStrategyPeriods();
        //    ForwardPeriod forwardPeriod;
        //    if (referedPeriods == null)
        //    {
        //        referedPeriods = new StrategyReferedPeriods();
        //        referedPeriods.UsedKLinePeriods.Add(period);
        //        forwardPeriod = new ForwardPeriod(false, period);
        //    }
        //    else
        //    {
        //        bool useTickData = referedPeriods.UseTickData;
        //        if (referedPeriods.UsedKLinePeriods != null && referedPeriods.UsedKLinePeriods.Count != 0)
        //            period = referedPeriods.UsedKLinePeriods.Min<KLinePeriod>();
        //        forwardPeriod = new ForwardPeriod(useTickData, period);
        //    }
        //    return StrategyExecutorFactory.CreateHistoryExecutor(dataPackage, referedPeriods, forwardPeriod, compChart1.StrategyHelper);
        //}
    }

    public class CompChartStrategyExecuter
    {
        private CompChart compChart;
        private IDataPackage_Code dataPackage;
        private IStrategy strategy;
        private IStrategyExecutor strategyExecutor;

        public IStrategyExecutor StrategyExecutor
        {
            get { return strategyExecutor; }
        }

        public CompChartStrategyExecuter(CompChart compChart, IDataPackage_Code dataPackage, IStrategy strategy)
        {
            this.compChart = compChart;
            this.dataPackage = dataPackage;
            this.strategy = strategy;
        }

        private bool IsRefreshCompChartData()
        {
            return !IsSameDataPackage(compChart.CompChartData.DataPackage, this.dataPackage);
        }

        public static bool IsSameDataPackage(IDataPackage_Code dataPackage1, IDataPackage_Code dataPackage2)
        {
            if (dataPackage1 == null || dataPackage2 == null)
                return false;
            return dataPackage1.Code == dataPackage2.Code && dataPackage1.StartDate == dataPackage2.StartDate && dataPackage1.EndDate == dataPackage2.EndDate;
        }

        public void Execute()
        {
            strategyExecutor = GetStrategyExecutor(this.compChart.CompChartData.DataPackage, strategy);
            strategyExecutor.ExecuteFinished += StrategyExecutor_ExecuteFinished;
            strategyExecutor.Execute();
        }

        private IStrategyExecutor GetStrategyExecutor(IDataPackage_Code dataPackage, IStrategy strategy)
        {
            StrategyReferedPeriods referPeriods = GetKLinePeriod(strategy);
            KLinePeriod period = referPeriods.GetMinPeriod();
            if (period == null)
                period = KLinePeriod.KLinePeriod_1Minute;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, period);
            if (dataPackage == null)
                dataPackage = compChart.CompChartData.DataPackage;
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory_History().CreateExecutorByDataPackage(dataPackage, referPeriods, forwardPeriod, GetStrategyHelper());
            executor.SetStrategy(strategy);
            return executor;
        }

        private StrategyReferedPeriods GetKLinePeriod(IStrategy strategy)
        {
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            if (referedPeriods != null)
                return referedPeriods;
            //strategy.MainKLinePeriod = compChart.CompChartData.KlinePeriod;
            referedPeriods = new StrategyReferedPeriods();
            KLinePeriod period = compChart.GetKLinePeriod();
            referedPeriods.UsedKLinePeriods.Add(period);
            return referedPeriods;
        }

        private StrategyOperator GetStrategyHelper()
        {
            return compChart.StrategyHelper;
        }

        private void StrategyExecutor_ExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments args)
        {
            bool isRefreshCompChart = IsRefreshCompChartData();
            if (isRefreshCompChart)
            {
                //this.compChart.CompChartData.DataPackage =dataPackage;
            }
            else
                //this.compChart.Refresh();
                this.compChart.PaintChart();
        }
    }
}