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
    public partial class FormStrategyResult : Form
    {
        private CompChart compChart;

        private IStrategyQueryResult strategyResult;

        public FormStrategyResult(CompChart compChart, IStrategyQueryResult strategyResult)
        {
            InitializeComponent();
            this.compChart = compChart;
            this.strategyResult = strategyResult;
            this.Init();
        }

        private void Init()
        {
            IList<IStrategyQueryResult_Single> results = strategyResult.StrategyResults;
            for (int i = 0; i < results.Count; i++)
            {
                IStrategyQueryResult_Single result = results[i];
                int index = dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = result.Code;
                this.dataGridView1.Rows[index].Cells[1].Value = result.Time;
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
                return;
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string code = row.Cells[0].Value.ToString();
            double time = (double)row.Cells[1].Value;
            compChart.Time = time;
        }
    }
}
