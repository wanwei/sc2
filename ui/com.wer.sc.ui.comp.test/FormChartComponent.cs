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

            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code("rb1710", 20170101, 20170501);
            //ChartComponentInfo data = new ChartComponentInfo(dataPackage);
            //ChartComponentView view = new ChartComponentView(this.panel1, data);
        }
    }
}
