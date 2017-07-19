using com.wer.sc.comp.graphic.timeline;
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
        private GraphicDrawer_TimeLineChart drawer;

        private GraphicDrawer_TimeLineMount drawer_mount;

        public FrmGraphicDrawer_Real()
        {
            InitializeComponent();

            drawer = new GraphicDrawer_TimeLineChart();
            drawer.MarginInfo = new GraphicMarginInfo(60, 20, 50, 20);
            drawer.Padding = new GraphicPaddingInfo(0, 0, 0, 0);
            MockGraphicData_Real dataProvider = new MockGraphicData_Real();
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
