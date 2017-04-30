using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_Candle : Form
    {
        private GraphicDrawer_CandleChart drawer;

        private GraphicDrawer_CandleMount drawer_mount;
        public FrmGraphicDrawer_Candle()
        {
            InitializeComponent();
            drawer = new GraphicDrawer_CandleChart();
            drawer.MarginInfo.MarginTop = 20;
            drawer.MarginInfo.MarginLeft = 60;
            drawer.MarginInfo.MarginRight = 20;
            drawer.MarginInfo.MarginBottom = 1;
            drawer.Padding = new GraphicPaddingInfo(0, 20, 50, 0);

            MockGraphicDataProvider dataProvider = new MockGraphicDataProvider();
            dataProvider.Code = "m05";
            dataProvider.Period = new KLinePeriod(KLineTimeType.DAY, 1);
            dataProvider.EndIndex = 210;

            drawer.DataProvider = dataProvider;

            drawer.BindControl(panel1);

            DrawOthers(dataProvider);

            drawer_mount = new GraphicDrawer_CandleMount();
            drawer_mount.DataProvider = dataProvider;
            drawer_mount.MarginInfo.MarginTop = 0;
            drawer_mount.MarginInfo.MarginLeft = 60;
            drawer_mount.MarginInfo.MarginRight = 20;
            drawer_mount.MarginInfo.MarginBottom = 20;
            drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 50, 0);
            drawer_mount.BindControl(panel2);
        }

        private void DrawOthers(MockGraphicDataProvider dataProvider)
        {
            IKLineData data = dataProvider.GetKLineData();
            float[] ma = new float[data.Length];
            for (int i = 0; i < ma.Length; i++)
            {
                ma[i] = this.ma(data.Arr_End, i, 5);
            }
            drawer.AddPolyLine(new PolyLineArray(ma, Color.Green));

            float[] points = new float[data.Length];
            for (int i = 0; i < ma.Length; i++)
            {
                float value = ma[i];
                points[i] = (value > data.Arr_High[i]) ? value : -1;
            }
            drawer.AddPoint(new PointArray(points, Color.Blue, 5));
        }

        public float ma(IList<float> values, int index, int len)
        {
            float ma = 0;
            int startindex = index - len + 1;
            int endIndex = index;
            startindex = startindex < 0 ? 0 : startindex;
            for (int i = startindex; i < endIndex; i++)
                ma += values[i];
            return (float)Math.Round(ma / (endIndex - startindex), 3);
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            drawer.DataProvider.EndIndex -= 20;
            drawer.DrawGraph();
            drawer_mount.DrawGraph();
        }

        private void btForward_Click(object sender, EventArgs e)
        {
            drawer.DataProvider.EndIndex += 20;
            drawer.DrawGraph();
            drawer_mount.DrawGraph();
        }
    }
}