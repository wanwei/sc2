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

        private IStrategyDrawer drawer;

        private IStrategyData strategyData;

        //private IDataPackage_Code dataPackage;

        private CodePackageInfo codePackageInfo;

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

        public FormStrategy(CodePackageInfo codePackageInfo, IStrategyData strategyData, IStrategyDrawer drawer)
        {
            this.codePackageInfo = codePackageInfo;
            this.strategyData = strategyData;
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

            this.InitCodePackage(chartComponent);
            this.Init(chartComponent.StrategyData);
        }

        private void InitCodePackage(ChartComponent chartComponent)
        {
            IDataPackage_Code dataPackage = chartComponent.Controller.CurrentNavigater.DataPackage;
            CodePackageInfo codePackageInfo = new CodePackageInfo();
            codePackageInfo.Codes.Add(dataPackage.Code);
            codePackageInfo.Start = dataPackage.StartDate;
            codePackageInfo.End = dataPackage.EndDate;
            this.compCodePackage1.Init(codePackageInfo);
        }

        private void Init(IStrategyData strategyData)
        {
            if (strategyData == null)
                return;
            this.StrategyData = strategyData;
            this.compParameters1.Parameters = this.strategyData.Strategy.Parameters;
            this.chartComponent.ChartComponentStrategy.ExecuteFinished += ChartComponentStrategy_ExecuteFinished;
            this.Text = "策略：" + this.strategyData.StrategyInfo.Name;
        }

        private void btExecutor_Click(object sender, EventArgs e)
        {
            //设置参数
            if (this.compParameters1.Parameters != null)
            {
                this.compParameters1.Parameters.GetParameterValues();
                IStrategy strategy = this.strategyData.Strategy;
                strategy.Parameters.SetParameterValue(this.compParameters1.Parameters.GetParameterValues());
            }
            //
            //this.chartComponent.ChartComponentStrategy.Run();
            try
            {
                this.chartComponent.ChartComponentStrategy.Run();
                //this.Close();
            }
            catch (Exception ex)
            {
                FormException form = new FormException(ex);
                form.ShowDialog();
                //MessageBox.Show("执行策略出错：" + e.Message);
            }
        }

        private void ChartComponentStrategy_ExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments arg)
        {
            MessageBox.Show("执行完毕");
            //this.progressBar1.Maximum = 100;
            //this.progressBar1.Value = 100;                             
        }

        //private void UpdateMaxProgress(int max)
        //{
        //    if (progressBar1.InvokeRequired)
        //    {
        //        UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateMaxProgress);
        //        this.Invoke(pi, max);
        //    }
        //    else
        //    {
        //        progressBar1.Maximum = max;//设置最大长度值
        //        progressBar1.Value = 0;//设置当前值
        //        progressBar1.Step = 1;//设置没次增长多少                
        //    }
        //}

        private void ExecuteStrategy()
        {

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
            //FormStrategyDataPackage form = new FormStrategyDataPackage(this.dataPackage);
            //form.ShowDialog();
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

    /// <summary>
    /// 策略用来执行的数据
    /// </summary>
    class PreparedDataForStrategy
    {
        private bool choosedByMainContract;

        private IList<string> codes;

        private int start;

        private int end;

        public bool ChoosedByMainContract
        {
            get
            {
                return choosedByMainContract;
            }

            set
            {
                choosedByMainContract = value;
            }
        }

        public IList<string> Codes
        {
            get
            {
                return codes;
            }

            set
            {
                codes = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }
    }
}