using com.wer.sc.ana.test.model;
using com.wer.sc.comp.ana;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_Ana2 : Form
    {
        public FrmGraphicDrawer_Ana2()
        {
            InitializeComponent();

            DataReaderFactory fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
            GraphicDataProvider_CandleDefault dataProvider = new GraphicDataProvider_CandleDefault(fac);
            AnaDrawer_KLine drawer = new AnaDrawer_KLine(fac, dataProvider);
            drawer.Bind(this);
            drawer.Show("m13", 20100101, 20150101, new KLinePeriod(KLineTimeType.DAY, 1));

            KLineModel_Simple2 model = new KLineModel_Simple2();
            drawer.Run(model);
            //drawer.Run("m13", 20100101, 20150101, new KLinePeriod(KLineTimeType.DAY, 1), model);            

            //Thread.Sleep(100000);
            KLineModel_Simple model2 = new KLineModel_Simple();
            drawer.Run(model2);
            //drawer.Run("m13", 20100101, 20150101, new KLinePeriod(KLineTimeType.DAY, 1), model2);
        }
    }
}