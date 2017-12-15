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

namespace com.wer.sc.ui.comp.test
{
    public partial class FormMainComponent : Form
    {
        public FormMainComponent()
        {
            InitializeComponent();
            this.mainComponent1.Init(DataCenter.Default, "rb1710", 20170503.0930);
            this.menuComponent1.BindChartComponent(this.mainComponent1.ChartComponent);
        }
    }
}
