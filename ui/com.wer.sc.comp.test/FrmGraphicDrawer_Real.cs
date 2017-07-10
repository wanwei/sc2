using com.wer.sc.comp.graphic.real;
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
    public partial class FrmGraphicDrawer_Real : Form
    {
        private GraphicDrawer_RealChart drawer;

        private GraphicDrawer_RealMount drawer_mount;

        public FrmGraphicDrawer_Real()
        {
            InitializeComponent();

            drawer = new GraphicDrawer_RealChart();
            drawer.MarginInfo.MarginTop = 20;
            drawer.MarginInfo.MarginLeft = 60;
            drawer.MarginInfo.MarginRight = 50;
            drawer.MarginInfo.MarginBottom = 20;
            drawer.Padding = new GraphicPaddingInfo(0, 0, 0, 0);

            MockGraphicDataProvider_Real dataProvider = new MockGraphicDataProvider_Real();
            //dataProvider.Code = "m05";
            //dataProvider.Period = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            //dataProvider.EndIndex = 210;

            drawer.DataProvider = dataProvider;

            drawer.BindControl(this);

            //drawer_mount = new GraphicDrawer_RealMount();
            //drawer_mount.DataProvider = dataProvider;
            //drawer_mount.MarginInfo.MarginTop = 0;
            //drawer_mount.MarginInfo.MarginLeft = 60;
            //drawer_mount.MarginInfo.MarginRight = 20;
            //drawer_mount.MarginInfo.MarginBottom = 20;
            //drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 50, 0);
            //drawer_mount.BindControl(panel2);
        }
    }
}
