using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.graphic;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件运行接口
    /// </summary>
    public class ChartComponentStrategy
    {
        private IStrategyData strategyData;

        private IStrategyExecutor_Single strategyExecutor;
        //画图器
        private IStrategyDrawer drawOperator;

        private ChartComponent compChart;

        public IStrategyData StrategyData
        {
            get
            {
                return strategyData;
            }

            set
            {
                this.strategyData = value;
            }
        }

        public IStrategyExecutor_Single StrategyExecutor
        {
            get { return strategyExecutor; }
        }

        public ChartComponentStrategy(ChartComponent compChart, IStrategyData strategyData)
        {
            this.StrategyData = strategyData;
            this.compChart = compChart;
            this.compChart.OnChartRefresh += CompChart_OnChartRefresh;
        }

        private void CompChart_OnChartRefresh(object sender, ChartComponentRefreshArguments arg)
        {
            if (this.strategyData == null || this.strategyData.Strategy == null)
                return;
            IStrategy strategy = this.strategyData.Strategy;
            //if (!arg.DataRefreshed)
            //    return;
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            //当K线图上数据变化，需要修改策略画的图形
            //如果画的是K线，且K线周期发生了变化，那么适应全周期的策略需要重新计算
            if (!arg.PrevCompData.KlinePeriod.Equals(arg.CurrentCompData.KlinePeriod) && referedPeriods == null)
            {
                //重置当前策略
                this.strategyData.RefreshStrategy();
                strategy = strategyData.Strategy;
                strategy.Parameters.SetParameterValue(strategy.Parameters.GetParameterValues());
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
            if (this.strategyData == null || this.strategyData.Strategy == null)
                return;
            IStrategy strategy = this.strategyData.Strategy;
            StrategyReferedPeriods referedPeriods = strategy.GetReferedPeriods();
            if (referedPeriods == null)
            {
                referedPeriods = new StrategyReferedPeriods();
                KLinePeriod currentPeriod = this.compChart.Controller.ChartComponentData.KlinePeriod;
                referedPeriods.UsedKLinePeriods.Add(currentPeriod);
                if (strategy is StrategyAbstract)
                {
                    ((StrategyAbstract)strategy).MainKLinePeriod = currentPeriod;
                }
            }
            KLinePeriod mainPeriod = referedPeriods.MainPeriod;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(referedPeriods.UseTickData, mainPeriod);

            IStrategyExecutorFactory executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory();
            IDataPackage_Code dataPackage = this.compChart.Controller.CurrentNavigater.DataPackage;

            Dictionary<KLinePeriod, int> dic_KLinePeriod_StartPos = new Dictionary<KLinePeriod, int>();
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
                dic_KLinePeriod_StartPos.Add(period, dataPackage.GetKLineData(period).BarPos);
            }

            drawOperator = new ChartComponentStrategyDrawer(this.compChart.Drawer, dic_KLinePeriod_StartPos, 0, 0);
            StrategyArguments_DataPackage strategyDataPackage = new StrategyArguments_DataPackage(dataPackage, referedPeriods, forwardPeriod); ;
            StrategyHelper strategyOperator = new StrategyHelper();
            strategyOperator.Drawer = drawOperator;
            strategyExecutor = executorFactory.CreateExecutor_History(strategyDataPackage);

            strategyExecutor.Strategy = strategy;
            //strategyExecutor.Run();
            //strategyExecutor.ExecuteFinished += StrategyExecutor_ExecuteFinished;
            strategyExecutor.Execute();
        }

        public event StrategyFinished ExecuteFinished;

        private void StrategyExecutor_ExecuteFinished(IStrategy strategy, StrategyFinishedArguments arg)
        {
            //IStrategyTrader trader = strategyExecutor.StrategyReport.StrategyTrader;
            //if (trader != null)
            //{
            //    if (trader is StrategyTrader_History)
            //    {
            //        compChart.Account = ((StrategyTrader_History)trader).Account;
            //    }
            //}
            //if (ExecuteFinished != null)
            //    ExecuteFinished(strategy, arg);
        }

        public void Refresh()
        {
            ChartComponentData compData = this.compChart.Controller.ChartComponentData;
            ChartType chartType = compData.ChartType;
            if (chartType == ChartType.KLine)
            {
                IStrategyDrawer_PriceRect drawer = drawOperator.GetDrawer_KLine(compData.KlinePeriod);
                drawer.Refresh();
            }
            //else if (chartType == ChartType.TimeLine)
            //    drawOperator.GetDrawer_TimeLine().Refresh();
            //else if (chartType == ChartType.Tick)
            //    drawOperator.GetDrawer_Tick().Refresh();
        }
    }
}
