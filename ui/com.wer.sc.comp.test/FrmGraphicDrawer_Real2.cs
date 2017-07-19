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
    public partial class FrmGraphicDrawer_Real2 : Form
    {
        private GraphicDrawer_TimeLine drawer;

        public FrmGraphicDrawer_Real2()
        {
            InitializeComponent();

            drawer = new GraphicDrawer_TimeLine();
            drawer.MarginInfo.MarginTop = 20;
            drawer.MarginInfo.MarginLeft = 60;
            drawer.MarginInfo.MarginRight = 50;
            drawer.MarginInfo.MarginBottom = 20;
            drawer.Padding = new GraphicPaddingInfo(0, 0, 0, 0);

            MockGraphicData_Real dataProvider = new MockGraphicData_Real();
            //dataProvider.Code = "m05";
            //dataProvider.Period = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            //dataProvider.EndIndex = 210;

            drawer.DataProvider = dataProvider;

            drawer.BindControl(this);
        }
    }
}
