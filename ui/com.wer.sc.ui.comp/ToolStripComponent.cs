using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.ui.comp.strategy;
using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp.trade;
using com.wer.sc.plugin;
using com.wer.sc.data.account;
using com.wer.sc.data.market;

namespace com.wer.sc.ui.comp
{
    public partial class ToolStripComponent : UserControl
    {
        private ChartComponent chartComponent;

        private ForwardPeriod forwardPeriod = new ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

        //交易插件
        private IPlugin_MarketTrader marketTrader;

        public ToolStripComponent()
        {
            InitializeComponent();
            this.tb_KLine1.Tag = KLinePeriod.KLinePeriod_1Minute;
            this.tb_KLine5.Tag = KLinePeriod.KLinePeriod_5Minute;
            this.tb_KLine15.Tag = KLinePeriod.KLinePeriod_15Minute;
            this.tb_KLine1H.Tag = KLinePeriod.KLinePeriod_1Hour;
            this.tb_KLine1Day.Tag = KLinePeriod.KLinePeriod_1Day;
            this.tb_KLine5S.Tag = KLinePeriod.KLinePeriod_5Second;
            this.tb_KLine15S.Tag = KLinePeriod.KLinePeriod_15Second;
        }

        public void BindChartComponent(ChartComponent chartComponent)
        {
            this.chartComponent = chartComponent;
        }

        private void tb_SwitchChartType_Click(object sender, EventArgs e)
        {
            if (chartComponent == null)
                return;
            ToolStripButton bt = (ToolStripButton)sender;
            if (bt.Tag == null)
                return;
            if (!(bt.Tag is ChartType))
                return;
            ChartType chartType = (ChartType)bt.Tag;
            this.chartComponent.Controller.ChangeChartType(chartType);
        }

        private void tb_Refresh_Click(object sender, EventArgs e)
        {
            if (chartComponent == null)
                return;
            this.chartComponent.Controller.Refresh();
        }

        private void tb_KLine_Click(object sender, EventArgs e)
        {
            if (chartComponent == null)
                return;
            ToolStripButton bt = (ToolStripButton)sender;
            if (bt.Tag == null)
                return;
            if (!(bt.Tag is KLinePeriod))
                return;
            KLinePeriod period = (KLinePeriod)bt.Tag;
            this.chartComponent.Controller.ChangeKLinePeriod(period);
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            this.chartComponent.Controller.ForwardView(-20);
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            this.chartComponent.Controller.ForwardView(20);
        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {
            //this.chartComponent.Controller.BackwardTime(forwardPeriod.KlineForwardPeriod);
            this.chartComponent.Controller.BackwardTime(forwardPeriod.KlineForwardPeriod);
        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {
            this.chartComponent.Controller.ForwardTime(forwardPeriod);
        }

        private void tb_ForwardSetting_Click(object sender, EventArgs e)
        {
            FormForwardSetting formForwardSetting = new FormForwardSetting();
            formForwardSetting.ForwardPeriod = forwardPeriod;
            DialogResult dialogResult = formForwardSetting.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.forwardPeriod = formForwardSetting.ForwardPeriod;
                if (this.forwardPeriod.IsTickForward)
                {
                    this.tb_BackwordTime.Enabled = false;
                    this.tb_BackwordTime.Text = "不支持tick后退";
                    this.tb_ForwordTime.Text = "前进一个tick";
                }
                else
                {
                    this.tb_BackwordTime.Enabled = true;
                    this.tb_BackwordTime.Text = "后退" + forwardPeriod.KlineForwardPeriod;
                    this.tb_ForwordTime.Text = "前进" + forwardPeriod.KlineForwardPeriod;
                }
            }
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            //FormTime ft = new FormTime(chartComponent.Controller.ChartComponentData.Time);
            //DialogResult result = ft.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    this.chartComponent.Controller.Change(ft.Time);
            //}
            FormCodeTime ft = new FormCodeTime(chartComponent.Controller.ChartComponentData.Code, chartComponent.Controller.ChartComponentData.Time);
            DialogResult result = ft.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.chartComponent.Controller.Change(ft.Code, ft.Time);
            }
        }

        private void btStrategy_Click(object sender, EventArgs e)
        {
            FormStrategy form = new FormStrategy(chartComponent);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                IStrategyExecutorPool pool = StrategyCenter.Default.GetStrategyExecutorPool();                
                FormStrategyExecutorPool formRunState = new FormStrategyExecutorPool(pool);
                formRunState.TopMost = true;
                formRunState.Show();
            }
        }

        private void btStrategyDataPackage_Click(object sender, EventArgs e)
        {
            IDataPackage_Code dataPackage = this.chartComponent.Controller.CurrentNavigater.DataPackage;
            FormStrategyDataPackage form = new FormStrategyDataPackage(dataPackage);
            form.ShowDialog();
        }

        private void btStrategyResult_Click(object sender, EventArgs e)
        {
            ChartComponentStrategy componentStrategy = this.chartComponent.ChartComponentStrategy;
            if (componentStrategy == null || componentStrategy.StrategyExecutor == null)
            {
                MessageBox.Show("没有执行的策略");
                return;
            }
            IStrategyQueryResultManager strategyResult = componentStrategy.StrategyExecutor.StrategyResult.StrategyQueryResultManager;
            if (strategyResult == null || strategyResult.GetQueryResults().Count == 0)
            {
                MessageBox.Show("没有查询结果");
                return;
            }
            FormStrategyResult form = new FormStrategyResult(chartComponent, strategyResult);
            form.ShowDialog();
        }

        private void tb_ChangeCode_Click(object sender, EventArgs e)
        {
            FormCodes form = new FormCodes(chartComponent);
            form.ShowDialog();
        }

        private void tb_Trade_Click(object sender, EventArgs e)
        {
            if (!chartComponent.ConnectedServer)
            {
                if (this.marketTrader == null)
                {
                    //弹出交易账号选择界面
                    FormAccount formAccount = new FormAccount(DataCenter.Default.AccountManager, FormAccount.PATH_MOCK);
                    DialogResult result = formAccount.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        chartComponent.Account = formAccount.SelectedAccount;
                        IAccount account = chartComponent.Account;
                        if (account.Time > 0)
                        {
                            string code = null;
                            if (account.CurrentTradeInfo.Count > 0)
                                code = account.CurrentTradeInfo[account.CurrentTradeInfo.Count - 1].InstrumentID;
                            if (code != null)
                                chartComponent.Controller.Change(code, account.Time);
                            else
                                chartComponent.Controller.Change(account.Time);
                        }
                        account.BindRealTimeReader(this.chartComponent.Controller.CurrentRealTimeDataReader);
                        this.marketTrader = new Plugin_MarketTrader_Simu(account, this.chartComponent.DataCenter.AccountManager, formAccount.SelectedAccountPath, formAccount.SelectedAccountName);
                    }
                    else
                        return;
                }
            }
            FormTrade formTrade = new FormTrade(marketTrader, chartComponent.Controller.CurrentNavigater);
            formTrade.TopMost = true;
            formTrade.Show();
        }

        public IAccount Account
        {
            get { return chartComponent.Account; }
            set { chartComponent.Account = value; }
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            if (this.marketTrader != null || this.Account != null)
            {
                if (this.marketTrader != null)
                {
                    this.marketTrader.DisConnect();
                    this.marketTrader = null;
                }
                this.Account = null;
                MessageBox.Show("交易已登出");
            }
            else
                MessageBox.Show("没有登陆交易");
        }

        private void tb_AccountAna_Click(object sender, EventArgs e)
        {
            if (Account == null)
            {
                MessageBox.Show("当前没有登陆任何账号，请选择一个账号进行分析");
                FormAccount formAccount = new FormAccount(DataCenter.Default.AccountManager, "");
                DialogResult result = formAccount.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Account = formAccount.SelectedAccount;
                    string code = null;
                    if (Account.CurrentTradeInfo.Count > 0)
                        code = Account.CurrentTradeInfo[Account.CurrentTradeInfo.Count - 1].InstrumentID;
                    if (code != null)
                        chartComponent.Controller.Change(code, Account.Time);
                    else
                        chartComponent.Controller.Change(Account.Time);
                }
                else
                    return;
            }
            FormAccountAna formAccountAna = new FormAccountAna(Account, this.chartComponent);
            formAccountAna.TopMost = true;
            formAccountAna.Show();
        }

        private void tb_Play_Click(object sender, EventArgs e)
        {
            chartComponent.Controller.Play();
        }
    }
}