using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.test
{
    public partial class FormChartComponent : Form
    {
        public FormChartComponent()
        {
            InitializeComponent();

            this.chartComponent1.Init(DataCenter.Default, "rb1710", 20170502.0930);
            this.menuComponent1.BindChartComponent(this.chartComponent1);
        }
    }
}
