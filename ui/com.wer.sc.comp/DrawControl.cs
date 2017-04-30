using System;
using System.Windows.Forms;
using com.wer.sc.data;
using com.wer.sc.comp.graphic;
using com.wer.sc.comp.ana;

namespace com.wer.sc.comp
{
    public partial class DrawControl : UserControl
    {
        private String dataPath;

        private DataReaderFactory fac;

        private IGraphicDataProvider_Candle dataProvider;

        private AnaDrawer_KLine drawer;

        public DrawControl()
        {
            InitializeComponent();
        }

        public string DataPath
        {
            get
            {
                return dataPath;
            }

            set
            {
                if (dataPath != value)
                {
                    dataPath = value;

                    this.fac = new DataReaderFactory(DataPath);
                    //this.dataProvider = new GraphicDataProvider_Default(fac);
                    this.dataProvider = new GraphicDataProvider_CandleNav(fac);
                    if (this.drawer != null)
                        this.drawer.UnBind();
                    this.drawer = new AnaDrawer_KLine(fac, dataProvider);
                    this.drawer.Bind(this);
                }
            }
        }

        public AnaDrawer_KLine Drawer
        {
            get
            {
                return drawer;
            }
        }
    }
}
