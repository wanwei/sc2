using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.main;
using com.wer.sc.comp.graphic.real;
using com.wer.sc.comp.graphic.utils;
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
    public partial class FrmGraphicDrawer_Switch2 : Form
    {
        public FrmGraphicDrawer_Switch2()
        {
            InitializeComponent();

            GraphicDrawer_Switch_CandleReal drawer = new GraphicDrawer_Switch_CandleReal();
            DataReaderFactory fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
            GraphicDataProvider_Main dataProvider = new GraphicDataProvider_Main(fac);

            dataProvider.DataNavigate.Change("m13", 20150106.094510, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));

            drawer.DataProvider = dataProvider;
            drawer.BindControl(this);
        }
    }
}
