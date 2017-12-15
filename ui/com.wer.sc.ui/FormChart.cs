using com.wer.sc.data;
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
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();

            string code = "RB1805";
            double time = double.Parse(DateTime.Now.ToString("yyyyMMdd.HHmmss"));
            this.mainComponent1.Init(DataCenter.Default, code, time);
            this.menuComponent1.BindChartComponent(this.mainComponent1.ChartComponent);
        }
    }
}
