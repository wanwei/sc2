using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件运行接口
    /// </summary>
    public class CompStrategyRunner
    {
        //策略
        private IStrategy strategy;

        //画图器
        private IDrawOperator drawOperator;

        private CompChart2 compChart;

        public CompStrategyRunner(CompChart2 compChart)
        {
            this.compChart = compChart;         
            this.compChart.OnChartRefresh += CompChart_OnChartRefresh;
            //this.compChart.Controller.OnDataChanged += Controller_OnDataChanged;
        }

        private void CompChart_OnChartRefresh(object sender, CompChartRefreshArguments arg)
        {
            //if (!arg.DataRefreshed)
            //    return;
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            //当K线图上数据变化，需要修改策略画的图形
            //如果画的是K线，且K线周期发生了变化，那么适应全周期的策略需要重新计算
            if (!arg.PrevCompData.KlinePeriod.Equals(arg.CurrentCompData.KlinePeriod) && referedPeriods == null)
            {
                Run();
            }
            else
            {
                Refresh();
            }
        }

        private void Controller_OnDataChanged(object sender, CompDataChangeArgument arg)
        {
            //if (strategy == null)
            //    return;
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            //当K线图上数据变化，需要修改策略画的图形
            //如果画的是K线，且K线周期发生了变化，那么适应全周期的策略需要重新计算
            if (!arg.PrevCompData.KlinePeriod.Equals(arg.CurrentCompData.KlinePeriod) && referedPeriods == null)
            {
                Run();
            }
            else
            {
                Refresh();
            }
        }

        /// <summary>
        /// 运行策略，并且将策略运行结果绑定到该
        /// </summary>
        public void Run()
        {
            Strategy_MultiMa strategy = new Strategy_MultiMa();
            strategy.MainKLinePeriod = this.compChart.Controller.CompData.KlinePeriod;
            this.strategy = strategy;
            if (this.strategy == null)
                return;
            StrategyReferedPeriods referedPeriods = this.strategy.GetReferedPeriods();
            if (referedPeriods == null)
            {
                referedPeriods = new StrategyReferedPeriods();
                KLinePeriod currentPeriod = this.compChart.Controller.CompData.KlinePeriod;
                referedPeriods.UsedKLinePeriods.Add(currentPeriod);
            }
            KLinePeriod mainPeriod = referedPeriods.MainPeriod;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(referedPeriods.UseTickData, mainPeriod);

            IStrategyExecutorFactory_History executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory_History();
            IDataPackage_Code dataPackage = this.compChart.Controller.CurrentNavigater.DataPackage;

            Dictionary<KLinePeriod, int> dic_KLinePeriod_StartPos = new Dictionary<KLinePeriod, int>();
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
                dic_KLinePeriod_StartPos.Add(period, dataPackage.GetKLineData(period).BarPos);
            }

            drawOperator = new StrategyDrawOperator(this.compChart.Drawer, dic_KLinePeriod_StartPos, 0, 0);
            IStrategyOperator strategyOperator = new StrategyOperator(drawOperator);
            IStrategyExecutor executor = executorFactory.CreateExecutorByDataPackage(dataPackage, referedPeriods, forwardPeriod, strategyOperator);

            executor.SetStrategy(strategy);
            executor.Run();
        }

        public void Refresh()
        {
            CompData compData = this.compChart.Controller.CompData;
            ChartType chartType = compData.ChartType;
            if (chartType == ChartType.KLine)
            {
                IStrategyDrawer drawer = drawOperator.GetDrawer_KLine(compData.KlinePeriod);
                drawer.Refresh();
            }
            else if (chartType == ChartType.TimeLine)
                drawOperator.GetDrawer_TimeLine().Refresh();
            else if (chartType == ChartType.Tick)
                drawOperator.GetDrawer_Tick().Refresh();
        }

        public IStrategy Strategy
        {
            get
            {
                return strategy;
            }

            set
            {
                strategy = value;
            }
        }
    }
}
