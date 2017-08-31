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

namespace com.wer.sc.ui
{
    public partial class FormStrategy : Form
    {
        private CompChartStrategyBinder binder;

        private StrategyInfo strategyInfo;

        private IStrategy strategy;

        public FormStrategy(CompChartStrategyBinder binder, StrategyInfo strategyInfo, IStrategy strategy, IDataPackage dataPackage)
        {
            InitializeComponent();
            this.binder = binder;
            this.strategy = strategy;

            this.lbStrategyId.Text = strategyInfo.StrategyID;
            this.lbStrategyName.Text = strategyInfo.StrategyName;
            this.lbStrategyDesc.Text = strategyInfo.StrategyDesc;

            this.compParameters1.Parameters = strategy.Parameters;

            this.lbCode.Text = dataPackage.Code;
            this.lbStart.Text = dataPackage.StartDate.ToString();
            this.lbEnd.Text = dataPackage.EndDate.ToString();
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
    }
}