using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.main;
using com.wer.sc.comp.test;
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
    public partial class FrmGraphicDrawer_Main : Form
    {
        public FrmGraphicDrawer_Main()
        {
            InitializeComponent();

            GraphicDrawer_Main drawer = new GraphicDrawer_Main();
            DataReaderFactory fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
            GraphicDataProvider_Main dataProvider = new GraphicDataProvider_Main(fac);
            dataProvider.GetOperator().ChangeData("m13", 20150106.094510, new KLinePeriod(KLineTimeType.MINUTE, 1));
            drawer.DataProvider = dataProvider;

            drawer.BindControl(this);
            //drawer.DrawGraph(this.CreateGraphics());
        }
    }
}
