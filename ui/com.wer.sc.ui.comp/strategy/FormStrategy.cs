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
            this.ShowIcon = false;
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
            //if (this.strategyData.Strategy != null && this.strategyData.Strategy.Parameters)
            this.compParameters1.Parameters = this.strategyData.Strategy.Parameters;
        }

        private void btExecutor_Click(object sender, EventArgs e)
        {
            if (this.compParameters1.Parameters != null)
            {
                this.compParameters1.Parameters.GetParameterValues();
                IStrategy strategy = this.strategyData.Strategy;
                strategy.Parameters.SetParameterValue(this.compParameters1.Parameters.GetParameterValues());
            }
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
                IStrategyInfo strategyInfo = form.SelectedStrategy;
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

        private void btRefresh_Click(object sender, EventArgs e)
        {
            IStrategyAssemblyMgr mgr = StrategyCenter.Default.GetStrategyMgr();
            if (this.strategyData == null)
            {
                mgr.Refresh();
                return;
            }
            string assemblyId = this.strategyData.StrategyInfo.StrategyAssembly.AssemblyName;
            string className = this.strategyData.StrategyInfo.ClassName;
            mgr.Refresh();

            IStrategyInfo strategyInfo = mgr.GetStrategyAssembly(assemblyId).GetStrategyInfo(className);
            IStrategyData strategyData = strategyInfo.CreateStrategyData();
            this.chartComponent.StrategyData = strategyData;
            this.Init(strategyData);
            MessageBox.Show("策略刷新成功");
        }
    }
}