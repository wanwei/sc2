using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.generator
{
    public partial class FormPrepareHistoryData : Form
    {
        private IPlugin_DataUpdater dataPreparer;

        public FormPrepareHistoryData(IPlugin_DataUpdater dataPreparer)
        {
            this.dataPreparer = dataPreparer;
            InitializeComponent();

            List<PreparerArgument> arguments = dataPreparer.GetAllArguments();
            tableLayoutPanel1.RowCount = arguments.Count;

            for (int i = 0; i < arguments.Count; i++)
            {
                PreparerArgument arg = arguments[i];
                Label lb = new Label();
                lb.Text = arg.Name;
                TextBox tb = new TextBox();
                tb.Text = arg.Value;
                tb.ReadOnly = true;

                tableLayoutPanel1.Controls.Add(lb);
                tableLayoutPanel1.SetRow(lb, i);
                tableLayoutPanel1.SetColumn(lb, 0);
                tableLayoutPanel1.Controls.Add(tb);
                tableLayoutPanel1.SetRow(tb, i);
                tableLayoutPanel1.SetColumn(tb, 1);
            }

            this.Height = arguments.Count * 40 + 70;
        }
    }
}
