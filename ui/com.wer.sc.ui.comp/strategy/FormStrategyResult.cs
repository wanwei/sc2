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

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyResult : Form
    {
        private ChartComponent compChart;

        private IStrategyQueryResultManager strategyResultManager;

        public FormStrategyResult(ChartComponent compChart, IStrategyQueryResultManager strategyResult)
        {
            InitializeComponent();
            this.ShowIcon = false;
            this.compChart = compChart;
            this.strategyResultManager = strategyResult;
            this.Init();
        }

        private void Init()
        {
            if (strategyResultManager == null || strategyResultManager.GetQueryResults().Count == 0)
                return;

            this.lbResults.SelectedIndexChanged += LbResults_SelectedIndexChanged;
            for (int i = 0; i < strategyResultManager.GetQueryResults().Count; i++)
            {
                this.lbResults.Items.Add(strategyResultManager.GetQueryResults()[i].Name);
            }
            this.lbResults.SelectedIndex = 0;
        }

        private void LbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            object item = lbResults.SelectedItem;
            if (item == null)
                return;
            ShowStrategyQueryResult(item.ToString());
        }

        private string currentQueryResultName;

        private void ShowStrategyQueryResult(string name)
        {
            if (name.Equals(this.currentQueryResultName))
                return;
            this.currentQueryResultName = name;
            IStrategyQueryResult strategyResult = strategyResultManager.GetQueryResultByName(name);
            this.InitGridView(strategyResult);
            IList<IStrategyQueryResultRow> results = strategyResult.Rows;
            for (int i = 0; i < results.Count; i++)
            {
                IStrategyQueryResultRow result = results[i];
                SetGridValue(result);
            }
        }

        private void SetGridValue(IStrategyQueryResultRow result)
        {
            int index = dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = result.Code;
            this.dataGridView1.Rows[index].Cells[1].Value = result.Time;
            for (int i = 0; i < result.Data.Count; i++)
            {
                this.dataGridView1.Rows[index].Cells[i + 2].Value = result.Data[i];
            }
        }

        private void InitGridView(IStrategyQueryResult strategyResult)
        {
            this.dataGridView1.Rows.Clear();
            while (this.dataGridView1.Columns.Count > 2)
                this.dataGridView1.Columns.RemoveAt(2);
            string[] titles = strategyResult.Titles;
            for (int i = 0; i < titles.Length; i++)
            {
                string title = titles[i];
                this.dataGridView1.Columns.Add(title, title);
            }
        }

        //private void SetGridValue

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
                return;
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string code = row.Cells[0].Value.ToString();
            double time = (double)row.Cells[1].Value;
            compChart.Controller.Change(time);
        }
    }
}
