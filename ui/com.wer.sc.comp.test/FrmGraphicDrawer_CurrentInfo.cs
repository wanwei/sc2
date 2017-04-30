using com.wer.sc.comp.graphic.info;
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

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_CurrentInfo : Form
    {
        public FrmGraphicDrawer_CurrentInfo()
        {
            InitializeComponent();

            GraphicDrawer_CurrentInfo drawer = new GraphicDrawer_CurrentInfo();

            DataReaderFactory fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
            GraphicDataProvider_CurrentInfo_Nav dataProvider = new GraphicDataProvider_CurrentInfo_Nav(fac);
            dataProvider.GetOperator().Change("m13", 20150106.094510);
            drawer.DataProvider = dataProvider;
            drawer.BindControl(this);
        }
    }
}
