using com.wer.sc.comp.graphic;
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

namespace com.wer.sc.comp
{
    public partial class FrmGraphicDrawer_PriceRect : Form
    {
        public FrmGraphicDrawer_PriceRect()
        {
            InitializeComponent();

            GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();

            MockGraphicData_Candle dataProvider = new MockGraphicData_Candle("m1505", 20140601, 20150401, KLinePeriod.KLinePeriod_1Day);
            dataProvider.BlockMount = 100;
            dataProvider.EndIndex = 200;
            //dataProvider.Code = "m05";
            //dataProvider.Period = new KLinePeriod(KLineTimeType.DAY, 1);
            //dataProvider.EndIndex = 710;

            drawer.DataProvider = dataProvider;
            drawer.BindControl(this);

            IKLineData klineData = dataProvider.GetKLineData();

            for (int x = 50; x <= 110; x++)
            {
                //int x = 150;
                float y = klineData.Arr_High[x];
                PriceShape_Point point = new PriceShape_Point(x, y, 5, Color.Red);
                drawer.Drawer_Chart.DrawShape(point);
            }
            CrossHairDrawer cdrawer = new CrossHairDrawer();
            cdrawer.Bind(drawer);
        }
    }
}
