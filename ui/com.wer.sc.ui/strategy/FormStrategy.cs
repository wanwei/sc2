using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.strategy
{
    public partial class FormStrategy : Form
    {
        private CompChartStrategyBinder binder;

        private StrategyInfo strategyInfo;

        private IStrategy strategy;

        private IDataPackage dataPackage;

        public FormStrategy(CompChartStrategyBinder binder, StrategyInfo strategyInfo, IStrategy strategy, IDataPackage dataPackage)
        {
            InitializeComponent();
            this.binder = binder;
            this.dataPackage = dataPackage;

            InitStrategy(strategyInfo, strategy);
        }

        private void InitStrategy(StrategyInfo strategyInfo, IStrategy strategy)
        {
            this.strategy = strategy;
            this.strategyInfo = strategyInfo;
            this.compParameters1.Parameters = strategy.Parameters;
        }

        private void btExecutor_Click(object sender, EventArgs e)
        {
            this.compParameters1.Parameters.GetParameterValues();
            //this.strategy.Parameters.SetParameterValue(this.compParameters1.Parameters.GetParameterValues());
            this.binder.BindStrategy(this.strategy);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btStrategyDescription_Click(object sender, EventArgs e)
        {
            FormStrategyDescription form = new FormStrategyDescription(strategyInfo);
            form.ShowDialog();
        }

        private void btStrategyDataPackage_Click(object sender, EventArgs e)
        {
            FormStrategyDataPackage form = new FormStrategyDataPackage(this.dataPackage);
            form.ShowDialog();
        }

        private void btStrategyReport_Click(object sender, EventArgs e)
        {
            FormStrategyReport form = new FormStrategyReport();
            form.ShowDialog();
        }

        private void btChangeStrategy_Click(object sender, EventArgs e)
        {
            FormStrategyLoader form = new FormStrategyLoader();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                StrategyInfo strategyInfo = form.SelectedStrategy;
                IStrategy strategy = strategyInfo.CreateStrategy();
                InitStrategy(strategyInfo, strategy);
            }
        }

        private void btStrategyResult_Click(object sender, EventArgs e)
        {
            IStrategyExecutor executor = this.binder.StrategyExecutor;
            if (executor == null)
            {
                MessageBox.Show("");
                return;
            }
            IStrategyReport report = executor.StrategyReport;
            if (report == null)
            {
                MessageBox.Show("");
                return;
            }
            FormStrategyResult form = new FormStrategyResult(binder.CompChart, report.StrategyResult);
            form.ShowDialog();
        }

        private void btStrategyTradeResult_Click(object sender, EventArgs e)
        {
            IStrategyExecutor executor = this.binder.StrategyExecutor;
            if (executor == null)
            {
                MessageBox.Show("");
                return;
            }
            IStrategyReport report = executor.StrategyReport;
            if (report == null)
            {
                MessageBox.Show("");
                return;
            }
            FormStrategyTrader form = new FormStrategyTrader(binder.CompChart, report.StrategyTrader);
            form.ShowDialog();
        }
    }
}