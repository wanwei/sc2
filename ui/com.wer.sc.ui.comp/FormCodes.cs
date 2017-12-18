using com.wer.sc.data;
using com.wer.sc.data.reader;
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
    public partial class FormCodes : Form
    {
        private ChartComponent compChart;

        private double currentTime;

        private List<CodeInfo> codes;

        private ShowCodesFilter showCodesFilter = new ShowCodesFilter();
        
        public FormCodes(ChartComponent compChart)
        {
            InitializeComponent();
            this.compChart = compChart;
            this.currentTime = compChart.Controller.ChartComponentData.Time;
            codes = compChart.DataCenter.DataReader.CodeReader.GetAllCodes();
            ShowCodes();
        }

        private void ShowCodes()
        {
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                if (code.Start > currentTime || code.End < currentTime)
                    continue;
                int index = dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = code.Code;
                this.dataGridView1.Rows[index].Cells[1].Value = code.Name;
                this.dataGridView1.Rows[index].Cells[2].Value = code.Start;
                this.dataGridView1.Rows[index].Cells[3].Value = code.End;
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
                return;
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string code = row.Cells[0].Value.ToString();
            compChart.Controller.Change(code);
        }
    }

    internal class ShowCodesFilter
    {
        private bool showOpenCode = true;

        private IList<string> showVarieties = new List<string>();

        public bool ShowOpenCode
        {
            get
            {
                return showOpenCode;
            }

            set
            {
                showOpenCode = value;
            }
        }

        public IList<string> ShowVarieties
        {
            get
            {
                return showVarieties;
            }
        }
    }
}
