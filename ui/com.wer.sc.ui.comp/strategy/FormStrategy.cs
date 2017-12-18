using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using com.wer.sc.utils.ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategy : Form
    {
        private ChartComponent chartComponent;

        private IStrategyData strategyData;

        //private IStrategy strategy;

        private IDataPackage_Code dataPackage;

        public IStrategyData StrategyData
        {
            get
            {
                return strategyData;
            }

            set
            {
                strategyData = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategyInfo"></param>
        /// <param name="strategy"></param>
        /// <param name="dataPackage"></param>
        public FormStrategy(ChartComponent chartComponent)
        {
            InitializeComponent();
            this.chartComponent = chartComponent;
            this.dataPackage = chartComponent.Controller.CurrentNavigater.DataPackage;
            this.Init(chartComponent.StrategyData);
        }

        private void Init(IStrategyData strategyData)
        {
            if (strategyData == null)
                return;
            this.StrategyData = strategyData;
            //this.strategy = this.StrategyData.Strategy;
            this.compParameters1.Parameters = this.strategyData.Strategy.Parameters;
        }

        private void btExecutor_Click(object sender, EventArgs e)
        {
            this.compParameters1.Parameters.GetParameterValues();
            IStrategy strategy = this.strategyData.StrategyInfo.CreateStrategy();
            strategy.Parameters.SetParameterValue(this.compParameters1.Parameters.GetParameterValues());
            this.strategyData.Strategy = strategy;
            //try
            //{
                this.chartComponent.ChartComponentStrategy.Run();
                this.Close();
            //}
            //catch (Exception ex)
            //{
            //    FormException form = new FormException(ex);
            //    form.ShowDialog();
            //    //MessageBox.Show("执行策略出错：" + e.Message);
            //}
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btStrategyDescription_Click(object sender, EventArgs e)
        {
            FormStrategyDescription form = new FormStrategyDescription(StrategyData);
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
                IStrategyData strategyData = strategyInfo.CreateStrategyData();
                this.chartComponent.StrategyData = strategyData;
                Init(strategyData);
            }
        }

        private void btStrategyResult_Click(object sender, EventArgs e)
        {
            //IStrategyExecutor executor = this.binder.StrategyExecutor;
            //if (executor == null)
            //{
            //    MessageBox.Show("");
            //    return;
            //}
            //IStrategyReport report = executor.StrategyReport;
            //if (report == null)
            //{
            //    MessageBox.Show("");
            //    return;
            //}
            //FormStrategyResult form = new FormStrategyResult(binder.CompChart, report.StrategyResult);
            //form.ShowDialog();
        }

        private void btStrategyTradeResult_Click(object sender, EventArgs e)
        {
            //IStrategyExecutor executor = this.binder.StrategyExecutor;
            //if (executor == null)
            //{
            //    MessageBox.Show("");
            //    return;
            //}
            //IStrategyReport report = executor.StrategyReport;
            //if (report == null)
            //{
            //    MessageBox.Show("");
            //    return;
            //}
            //FormStrategyTrader form = new FormStrategyTrader(binder.CompChart, report.StrategyTrader);
            //form.ShowDialog();
        }
    }
}