using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.info;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            IDataReader fac = DataReaderFactory.CreateDataReader(@"E:\SCDATA\CNFUTURES");
            IGraphicData_CurrentInfo dataProvider = new GraphicDataProvider_CurrentInfo();
            //dataProvider.GetOperator().Change("m13", 20150106.094510);
            //drawer.DataProvider = dataProvider;
            //drawer.BindControl(this);
            //IGraphicChartRight 
        }
    }
}
