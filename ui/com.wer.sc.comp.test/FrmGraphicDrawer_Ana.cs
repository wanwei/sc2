using com.wer.sc.ana;
using com.wer.sc.ana.test.model;
using com.wer.sc.comp.graphic;
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
    public partial class FrmGraphicDrawer_Ana : Form
    {
        public FrmGraphicDrawer_Ana()
        {
            InitializeComponent();            

            KLineModelRunner runner = new KLineModelRunner(@"D:\SCDATA\CNFUTURES");
            runner.Code = "m13";
            runner.StartDate = 20100725;
            runner.EndDate = 20111125;
            runner.Period = new data.KLinePeriod(KLineTimeType.TYPE_DAY, 1);

            KLineModel_Simple2 model = new KLineModel_Simple2();            
            runner.Model = model;
            runner.run();

            IKLineData data = runner.Data;
            MockGraphicDataProvider dataProvider = new MockGraphicDataProvider();
            dataProvider.ChangeData(data);
            dataProvider.EndIndex = 200;

            GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();
            drawer.DataProvider = dataProvider;
            drawer.drawer_chart.AddPolyLines(model.polyLines);
            drawer.drawer_chart.AddPolyLines(model.polyLineList);
            drawer.drawer_chart.AddPoints(model.points);
            drawer.drawer_chart.AddPoints(model.pointLists);
            drawer.BindControl(this);
        }
    }
}