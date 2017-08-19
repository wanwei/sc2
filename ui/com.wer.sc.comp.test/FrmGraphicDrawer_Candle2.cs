using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.shape;
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
    public partial class FrmGraphicDrawer_Candle2 : Form
    {
        public FrmGraphicDrawer_Candle2()
        {
            InitializeComponent();
            GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();

            MockGraphicData_Candle dataProvider = new MockGraphicData_Candle("m1505", 20140601, 20150401, KLinePeriod.KLinePeriod_1Day);
            dataProvider.BlockMount = 100;
            dataProvider.EndIndex = 200;
            //dataProvider.Code = "m05";
            //dataProvider.Period = new KLinePeriod(KLineTimeType.DAY, 1);
            //dataProvider.EndIndex = 710;

            Shape_Label label = new Shape_Label();
            label.Text = "test";
            label.Color = Color.White;
            label.X = 1;
            label.Y = -20;
            drawer.Drawer_Chart.DrawShape(label);
            drawer.DataProvider = dataProvider;
            drawer.BindControl(this);

            CrossHairDrawer cdrawer = new CrossHairDrawer();
            cdrawer.Bind(drawer);
            //drawer.DrawGraph();
        }
    }
}
