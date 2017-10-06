using com.wer.sc.comp.graphic;
using com.wer.sc.comp.param;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using com.wer.sc.ui.strategy;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public partial class FormChart2 : Form
    {
        private CompChart compChart1;

        private UILogger uiLogger = new UILogger();

        public FormChart2()
        {
            InitializeComponent();
            this.compChart1 = compMain1.CompChart1;
            this.compChart1.Code = "RB1801";
            //this.compChart1.Time = 20170929.145900;
            this.compChart1.Time = 20170929.150000;
            //this.compChart1.Code = "RB1801";
            //this.compChart1.Time = 20170930.150000;
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Minute;
            this.compChart1.OnChartRefresh += CompChart1_OnDataRefresh;
            this.SetLbTime(this.compChart1.Time);
            this.compChart1.PaintChart();

            this.compStrategyTree1.TreeStrategy.MouseClick += TreeStrategy_MouseClick;
            this.compStrategyTree1.TreeStrategy.MouseDoubleClick += TreeStrategy_MouseDoubleClick;

            this.BindDefaultStrategy();
        }

        private StrategyInfo strategyInfo;
        private IStrategy strategy;
        private CompChartStrategyBinder binder;

        private void BindDefaultStrategy()
        {
            try
            {
                IStrategyAssembly strategyAssembly = StrategyMgrFactory.DefaultPluginMgr.GetStrategyAssembly("com.wer.sc.strategy.common");
                if (strategyAssembly == null)
                    return;
                strategyInfo = strategyAssembly.GetStrategy("STRATEGY.DEFAULT");
                //strategyInfo = strategyAssembly.GetStrategy("STRATEGY.ZIGZAG");            
                if (strategyInfo == null)
                    return;
                strategy = strategyInfo.CreateStrategy();
                binder = new CompChartStrategyBinder(this.compChart1);
                binder.BindStrategy(strategy);
            }
            catch (Exception e)
            {

            }
        }

        private void TreeStrategy_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode tempNode = this.compStrategyTree1.TreeStrategy.GetNodeAt(e.X, e.Y);
            compStrategyTree1.TreeStrategy.SelectedNode = tempNode;
        }

        private void CompChart1_OnDataRefresh(object sender, ChartRefreshArguments arg)
        {
            uiLogger.Log(arg.CurrentChartState.CandleEndIndex.ToString() + "|" + arg.CurrentChartState.CandleStartIndex.ToString());
            if (!arg.DataRefreshed)
                return;
            //this.compChart1.PaintChart();
            //等待异步    
            SetLbTime(this.compChart1.Time);
        }

        private delegate void SetLabelText(object txt);//代理

        private void SetLbTime(object text)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new SetLabelText(SetLbTime), text);//通过代理调用刷新方法
            }
            else
            {
                this.lbTime.Text = text.ToString();
            }
        }

        private void tb_SwitchTimeLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.TimeLine;
        }

        private void tb_SwitchKLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_SwitchTick_Click(object sender, EventArgs e)
        {
            //this.compChart1.ChartType = ChartType.Tick;
        }

        private void tb_Refresh_Click(object sender, EventArgs e)
        {
            if (this.compChart1.ChartType == ChartType.KLine)
                this.compChart1.PaintChart();
        }

        private void tb_KLine1_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Minute;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_5Minute;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_15Minute;
            this.compChart1.ChartType = ChartType.KLine;
            //this.compChart1.PaintChart();
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Hour;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Day;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine5S_Click(object sender, EventArgs e)
        {
            //this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_5Second;
            //this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine15S_Click(object sender, EventArgs e)
        {
            //this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_15Second;
            //this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            this.compChart1.Backward(50);
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            this.compChart1.Forward(50);
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            FormTime ft = new FormTime(this.compChart1.Time);
            ft.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = ft.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.compChart1.Time = ft.Time;
            }
        }

        private void tb_ForwardSetting_Click(object sender, EventArgs e)
        {
            FormForwardSetting frm = new FormForwardSetting();
            frm.ForwardPeriod = this.compChart1.ForwardPeriod;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.compChart1.ForwardPeriod = frm.ForwardPeriod;
            }
        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {
            this.compChart1.ForwardTime();
        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {
            this.compChart1.BackwardTime();
        }

        private bool isPlaying;

        private void tb_Play_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                this.compChart1.Pause();
                isPlaying = false;
                tb_ForwardSetting.Enabled = true;
                tb_ForwordTime.Enabled = true;
                tb_BackwordTime.Enabled = true;
                tb_ChangeTime.Enabled = true;
            }
            else
            {
                this.compChart1.Play();
                isPlaying = true;
                tb_ForwardSetting.Enabled = false;
                tb_ForwordTime.Enabled = false;
                tb_BackwordTime.Enabled = false;
                tb_ChangeTime.Enabled = false;
            }
        }

        private void tb_CodeList_Click(object sender, EventArgs e)
        {
            FormCodes form = new FormCodes(this.compChart1);
            form.ShowDialog();
        }

        private void TreeStrategy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StrategyInfo strategyInfo = GetSelectedStrategy();
            if (strategyInfo == null)
                return;
            RunStrategy(strategyInfo);
        }

        private StrategyInfo GetSelectedStrategy()
        {
            TreeNode selectedNode = compStrategyTree1.TreeStrategy.SelectedNode;
            Object obj = selectedNode.Tag;
            if (obj == null)
                return null;
            if (!(obj is StrategyInfo))
                return null;
            return (StrategyInfo)obj;
        }

        private void RunStrategy(StrategyInfo strategyInfo)
        {
            IStrategy strategy = strategyInfo.CreateStrategy();
            RunStrategy(strategy);
        }

        private void RunStrategy(IStrategy strategy)
        {
            IDataPackage dataPackage = this.compChart1.CompChartData.DataPackage;
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            //compChart1.KlinePeriod
            KLinePeriod period = compChart1.GetKLinePeriod();
            referedPeriods.UsedKLinePeriods.Add(period);
            //referedPeriods.UsedKLinePeriods.Add(this.n)
            ForwardPeriod forwardPeriod = new ForwardPeriod(false, period);
            IStrategyExecutor strategyRunner = StrategyExecutorFactory.CreateHistoryExecutor(dataPackage, referedPeriods, forwardPeriod, compChart1.StrategyHelper);
            compChart1.StrategyHelper.DrawHelper.GetDrawer_KLine(period).ClearShapes();
            //compChart1.CurrentPriceRectDrawer.cl
            //compChart1.CurrentPriceRectDrawer.ClearPriceShapes();
            if (strategy is StrategyAbstract)
            {
                ((StrategyAbstract)strategy).DefaultMainPeriod = period;
            }
            strategyRunner.SetStrategy(strategy);
            strategyRunner.ExecuteFinished += StrategyRunner_ExecuteFinished;
            strategyRunner.Execute();
        }

        private void StrategyRunner_ExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments args)
        {
            compChart1.Refresh();
        }

        private FormLog frmLog;

        private void bt_Log_Click(object sender, EventArgs e)
        {
            frmLog = new FormLog(this.uiLogger);
            frmLog.Show(this);
        }

        private void menuItemParameters_Click(object sender, EventArgs e)
        {
            StrategyInfo strategyInfo = GetSelectedStrategy();
            IStrategy strategy = strategyInfo.CreateStrategy();
            IParameters parameters = strategy.Parameters;

            //IStrategy strategy
        }

        private void menuItemExecute_Click(object sender, EventArgs e)
        {
            StrategyInfo strategyInfo = GetSelectedStrategy();
            IStrategy strategy = strategyInfo.CreateStrategy();
            IParameters parameters = strategy.Parameters;

            FormParameters frmParamSetting = new FormParameters(parameters);
            DialogResult result = frmParamSetting.ShowDialog();
            if (result == DialogResult.OK)
            {
                RunStrategy(strategy);
            }
        }

        private void btStrategy_Click(object sender, EventArgs e)
        {
            FormStrategy frm = new FormStrategy(binder, this.strategyInfo, this.strategy, this.compChart1.CompChartData.DataPackage);
            frm.Show(this);
        }
    }
}